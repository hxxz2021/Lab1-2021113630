using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace TutorialInfo.Scripts
{
	public class Delay : MonoBehaviour
	{
		public double delayTime = 5;
		private double _timer;

		private void Update()
		{
			_timer -= Time.deltaTime;
			if (_timer > 0) return;
			gameObject.GetComponent<Button>().interactable = true;
			gameObject.GetComponent<EventTrigger>().enabled = true;
		}
		public void Click()
		{
			_timer = delayTime;
			gameObject.GetComponent<Button>().interactable = false;
			gameObject.GetComponent<EventTrigger>().enabled = false;
		}
	}
}