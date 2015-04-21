using UnityEngine;
using System.Collections;
using Entities;
using Game.InputDevices;
using System.Collections.Generic;

namespace Game
{
	public class GameManager
	{
		// Singleton
		private GameManager () {}
		private static GameManager instance;

		public static GameManager Instance
		{
			get {
				if (instance == null) {
					instance = new GameManager ();
				}
				
				return instance;
			}
		}

		// Camera Tools
		private readonly float targetAspectRatio = 1.776699f;
		private readonly float targetCameraSize = 5.0f;

		public void UpdateCameraSize (Camera camera)
		{	
			float newCameraSize = (targetAspectRatio * targetCameraSize) / camera.aspect;
			camera.orthographicSize = newCameraSize;
		}
		
		// Game Elements
		private Arena arena;
		private List<InputInfo> inputDevices = new List<InputInfo> (4);

		// Input Handlers
		public void FindAvailableControllers ()
		{
			// Keyboard
			for (int pos=0; pos < 4; pos++)
			{
				this.inputDevices.Add (new InputInfo ( pos, typeof(KeyboardInputDevice), true ));
			}
		}

		public InputInfo FindNextController ()
		{
			for (int pos=0; pos<inputDevices.Count; pos++)
			{
				if (inputDevices[pos].Available)
				{
					inputDevices[pos].Available = false;
					return inputDevices[pos];
				}
			}

			return null;
		}

		public InputInfo GetInputInfo (int pos)
		{
			return inputDevices[pos];
		}

	}
}
