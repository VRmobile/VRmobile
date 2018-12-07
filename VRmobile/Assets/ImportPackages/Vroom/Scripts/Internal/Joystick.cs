using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Vroom Controller for Unity
// Copyright 2017 WonderLeague Corporation.
// version 2.32

namespace Vvr.Internal
{
	public class Joystick
	{
		const float scale = (16 / 512);

		public Joystick()
		{
			Position = Vector2.zero;
			Action = VvrJoystickAction.Neutral;
		}

		public Vector2 Position { get; private set; }
		public Vector2 PositionRaw { get; private set; }
		public VvrJoystickAction Action { get; private set; }

		public void SetPosition(float new_x, float new_y)
		{
			this.SetPosition(new Vector2(new_x, new_y));
		}

		public void SetPosition(Vector2 newPos)
		{
			Vector2 oldPos = Position;

			Vector2 pos = newPos;
			const double scale = 1.0f / 512.0f;

			pos.x = (float)scale * (pos.x - 512);
			pos.y = (float)scale * (pos.y - 512);

			if (pos.x <= -0.7f)
			{
				if (pos.y <= -0.7f)
					Action = VvrJoystickAction.UpLeft;
				else if (pos.y >= 0.7f)
					Action = VvrJoystickAction.UpRight;
				else
					Action = VvrJoystickAction.Up;
			}
			else if (pos.x >= 0.7f)
			{
				if (pos.y <= -0.7f)
					Action = VvrJoystickAction.DownLeft;
				else if (pos.y >= 0.7f)
					Action = VvrJoystickAction.DownRight;
				else
					Action = VvrJoystickAction.Down;
			}
			else
			{
				if (pos.y <= -0.7f)
					Action = VvrJoystickAction.Left;
				else if (pos.y >= 0.7f)
					Action = VvrJoystickAction.Right;
				else
					Action = VvrJoystickAction.Neutral;

			}

			Position = pos;
			PositionRaw = newPos;
		}
	}
}