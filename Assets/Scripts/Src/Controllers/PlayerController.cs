using Entities;
using Game.Input;
using Observers;

namespace Controllers
{
	public class PlayerController : InputObserver
	{
		private Player player;
		private InputDevice inputHandler;

		public void Awake () {}
		public void Start () {}
		public void Update () {}
		public void OnEnable () {}
		public void OnDisable () {}

		public void OnCollisionEnter2D () {}
		public void OnCollisionExit2D () {}
		public void OnCollisionStay2D () {}

		public void inputMoved(bool left)
		{

		}

		public void inputJumped()
		{

		}

		public void inputFired()
		{

		}
	}
}
