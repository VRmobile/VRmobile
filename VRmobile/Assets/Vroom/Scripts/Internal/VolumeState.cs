using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Vroom Controller for Unity
// Copyright 2017 WonderLeague Corporation.
// version 2.32

namespace Vvr.Internal
{
	public class VolumeState 
	{
		public Button volumeDown = new Button ();
		public Button volumeUp   = new Button ();
		public VvrVolumeState volumeState = VvrVolumeState.None;

		private int buttonState = 0;
		private VvrVolumeState invalidState = VvrVolumeState.None;

		public void SetState (int newState) {
			/*
			 * 0x00 : [Vol+ false, Vol- false]
			 * 0x01 : [Vol+ false, Vol- true ]
			 * 0x02 : [Vol+ true,  Vol- false]
			 * 0x03 : [Vol+ true,  Vol- true ] 
			 */

			volumeDown.SetState((newState & 0x1) == 1 ? true : false);
			volumeUp.SetState(((newState >> 1) & 0x1) == 1 ? true : false);

			int oldState = buttonState;

			if (newState == 0) {
				volumeState = VvrVolumeState.None;
				invalidState = VvrVolumeState.None;
			} else if (newState == 1) {
				if (invalidState == VvrVolumeState.TurnDown) {
					volumeState = VvrVolumeState.None;
				} else {
					volumeState = VvrVolumeState.TurnDown;
				}

				if (invalidState == VvrVolumeState.TurnUp)
					invalidState = VvrVolumeState.TurnDown;
				
			/*
				if (oldState == 3) {
					if (invalidState == VvrVolumeState.TurnUp) {
						invalidState = VvrVolumeState.TurnDown;
					}
					volumeState = VvrVolumeState.TurnDown;
				} else {
					if (invalidState != VvrVolumeState.TurnDown) {
						volumeState = VvrVolumeState.TurnDown;
					} else {
						volumeState = VvrVolumeState.None;
					}
				}
			 */
			} else if (newState == 2) {
				if (invalidState == VvrVolumeState.TurnUp) {
					volumeState = VvrVolumeState.None;
				} else {
					volumeState = VvrVolumeState.TurnUp;
				}

				if (invalidState == VvrVolumeState.TurnDown)
					invalidState = VvrVolumeState.TurnUp;
			/*
				if (oldState == 3) {
					if (invalidState == VvrVolumeState.TurnDown) {
						invalidState = VvrVolumeState.TurnUp;
					}
					volumeState = VvrVolumeState.TurnUp;
				} else {
					if (invalidState != VvrVolumeState.TurnUp) {
						volumeState = VvrVolumeState.TurnUp;
					} else {
						volumeState = VvrVolumeState.None;
					}
				}			
			 */
			} else if (newState == 3) {
				if (oldState == 0) {
					/* 無押下状態から同時押し状態を検出した場合、強制でVol-とする */
					volumeState = VvrVolumeState.TurnDown;
				} else if (oldState == 1) {
					/* Vol-押下状態から、Vol+押下 */
					invalidState = VvrVolumeState.TurnDown;
					volumeState = VvrVolumeState.TurnUp;
				} else if (oldState == 2) {
					/* Vol+押下状態から、Vol-押下 */
					invalidState = VvrVolumeState.TurnUp;
					volumeState = VvrVolumeState.TurnDown;
				} else {
					/* 同時押し状態から同時押し状態 */
					if (invalidState == VvrVolumeState.TurnDown) {
						volumeState = VvrVolumeState.TurnUp;
					} else if (invalidState == VvrVolumeState.TurnUp) {
						volumeState = VvrVolumeState.TurnDown;
					}
				}
			}
			buttonState = newState;

		}
	}
}