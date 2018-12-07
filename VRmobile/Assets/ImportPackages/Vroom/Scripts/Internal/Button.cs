using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Vroom Controller for Unity
// Copyright 2017 WonderLeague Corporation.
// version 2.32

namespace Vvr.Internal
{
	public class Button
	{
		public Button()
		{
			ButtonUp = false;
			ButtonDown = false;
			ButtonState = false;
		}

		public bool ButtonUp { get; private set; }
		public bool ButtonDown { get; private set; }
		public bool ButtonState { get; private set; }

		public void SetState(bool newState)
		{
			bool oldState = ButtonState;
			if (oldState)
			{
				ButtonDown = false;
				if (newState == false)
					ButtonUp = true;
			}
			else
			{
				ButtonUp = false;
				if (newState == true)
					ButtonDown = true;
			}
			ButtonState = newState;
		}
	}
}