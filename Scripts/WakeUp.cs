using UnityEngine;

namespace TutorialInfo.Scripts
{
	public class WakeUp : MonoBehaviour
	{
		public GameObject nextDay;

		public void Click()
		{
			FindObjectOfType<SwitchRoom>().RoomFirstDay();
			nextDay.GetComponent<Delay>().Click();
			FindObjectOfType<ResourceManager>().NextDay();
		}
	}
}