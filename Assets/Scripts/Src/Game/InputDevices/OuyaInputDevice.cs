#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;
#endif

using System.Collections.Generic;

namespace Game.InputDevices
{
	public class OuyaInputDevice : InputDevice
	{
		override public float getAxisMovement()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			if (OuyaSDK.OuyaInput.GetButton(this.Id, OuyaController.BUTTON_DPAD_RIGHT))
			{
				return 1.0f;
			}
			else if (OuyaSDK.OuyaInput.GetButton(this.Id, OuyaController.BUTTON_DPAD_LEFT))
			{
				return -1.0f;
			}
			else
			{
				float x_axis = OuyaSDK.OuyaInput.GetAxis(this.Id, OuyaController.AXIS_LS_X);

				if (x_axis > 0.7f)
				{
					return 1.0f;
				}
				else if (x_axis < -0.7f)
				{
					return -1.0f;
				}
				else
				{
					return 0.0f;
				}
			}
			#else
			return 0.0f;
			#endif
		}

		override public bool isJumping()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return OuyaSDK.OuyaInput.GetButtonDown (this.Id, OuyaController.BUTTON_O);
			#else
			return false;
			#endif
		}

		override public bool isShooting()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return OuyaSDK.OuyaInput.GetButtonDown (this.Id, OuyaController.BUTTON_A);
			#else
			return false;
			#endif
		}

		override public bool isPausing()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return OuyaSDK.OuyaInput.GetButtonDown (this.Id, OuyaController.BUTTON_MENU);
			#else
			return false;
			#endif
		}

		override public bool isStillConnected ()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			return OuyaSDK.OuyaInput.IsControllerConnected(this.Id);
			#else
			return false;
			#endif
		}

		public static int[] GetConnectedControllers ()
		{
			List<int> controllersID = new List<int> (4);
			
			#if UNITY_ANDROID && !UNITY_EDITOR
			
			for (int i=0; i < 4; i++)
			{
				if (OuyaSDK.OuyaInput.IsControllerConnected(i))
				{
					controllersID.Add(i);
				}
			}
			
			#endif
			
			return controllersID.ToArray();
		}
	}
}