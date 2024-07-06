using UnityEngine;

namespace TutorialInfo.Scripts
{
	public class DropBoxController : MonoBehaviour
	{
		private Transform _dropbox;

		private void Start()
		{
			_dropbox = gameObject.transform.Find("DropBox");
		}

		public void OnPointerEnter()
		{
			_dropbox.gameObject.SetActive(true);
		}
		
		public void OnPointerExit()
		{
			_dropbox.gameObject.SetActive(false);
		}
	}
}