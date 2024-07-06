using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TutorialInfo.Scripts
{
	public class SwitchRoom : MonoBehaviour
	{
		private ResourceManager _resourceManager;
		public GameObject roomFire, roomForest, roomOutdoor, popup, account, resourceList, date, top, topBox, wakeUp, makeFire, nextDay;
		public GameObject optionFire, optionForest, optionOutdoor, line1On3, line2On3;

		public GameObject log, gainTrap;

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		private void Start()
		{
			_resourceManager = FindObjectOfType<ResourceManager>();
		}

		public void RoomInit()
		{
			top.SetActive(true);
			wakeUp.SetActive(true);
			
			optionForest.SetActive(false);
			optionOutdoor.SetActive(false);
			line1On3.SetActive(false);
			line2On3.SetActive(false);
			
			roomFire.SetActive(true);
			
			account.SetActive(false);
			date.SetActive(true);
		}

		public void RoomFirstDay()
		{
			wakeUp.SetActive(false);
			
			resourceList.SetActive(true);
			makeFire.SetActive(true);
			nextDay.SetActive(true);
			
			optionForest.SetActive(true);
			line1On3.SetActive(true);
			
			FindObjectOfType<ResourceList>().Refresh();
		}

		public void RoomSecondDay()
		{
			FindObjectOfType<SwitchRoom>().SelectRoomFire();
		}
		
		public void SelectRoomFire()
		{
			topBox.GetComponent<CanvasGroup>().interactable = true;
			roomFire.SetActive(true);
			roomForest.SetActive(false);
			roomOutdoor.SetActive(false);
			popup.SetActive(false);
			ResourceManager.SwitchRoom(1);
		}
		
		public void SelectRoomForest()
		{
			roomFire.SetActive(false);
			roomForest.SetActive(true);
			roomOutdoor.SetActive(false);
			popup.SetActive(false);
			ResourceManager.SwitchRoom(2);
		}

		public void Refresh()
		{
			log.transform.GetComponent<Button>().interactable = true;
			log.transform.GetComponent<EventTrigger>().enabled = true;
			gainTrap.transform.GetComponent<Button>().interactable = true;
			gainTrap.transform.GetComponent<EventTrigger>().enabled = true;
		}
		
		public void SelectRoomOutdoor()
		{
			roomFire.SetActive(false);
			roomForest.SetActive(false);
			roomOutdoor.SetActive(true);
			popup.SetActive(false);
			ResourceManager.SwitchRoom(3);
		}

		public void SelectPopUp()
		{
			topBox.GetComponent<CanvasGroup>().interactable = false;
			roomFire.SetActive(false);
			roomForest.SetActive(false);
			roomOutdoor.SetActive(false);
			popup.SetActive(true);
		}
	}
}