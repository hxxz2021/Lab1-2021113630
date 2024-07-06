using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TutorialInfo.Scripts
{
	public class ResourceList : MonoBehaviour
	{
		private ResourceManager _resourceManager;
		
		public GameObject resourceButtonTemplate, resourceColumnTemplate;
		public GameObject border;

		private RectTransform _rectList, _rectBorder;
		
		private void Start()
		{
			_resourceManager = FindObjectOfType<ResourceManager>();
			
			_rectList = gameObject.transform.GetComponent<RectTransform>();
			_rectBorder = border.transform.GetComponent<RectTransform>();
		}

		private void Update()
		{
			RectChange();
		}

		public void Refresh()
		{
			if (!_resourceManager)
				Start();
			
			var resourceList = _resourceManager.GetResourceList();
			var jobList = _resourceManager.GetJobList();
			
			for (var i = 0; i < gameObject.transform.childCount; i ++)
				Destroy(gameObject.transform.GetChild(i).gameObject);
			
			foreach (var i in resourceList)
			{
				if (i.Available == 0)
					continue;
				var buttonTMP = Instantiate(resourceButtonTemplate, gameObject.transform, false);
				buttonTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tools.ResourceName(resourceList.IndexOf(i));
				buttonTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = i.Cnt.ToString();

				var index = resourceList.IndexOf(i);
				var total = 0;
				
				foreach (var j in jobList.ScheduleList)
				{
					if (j.Cnt == 0 || j.Consume.Count <= index)
						continue;
					if (j.Consume[index] == 0)
						continue;

					var columnTMP = Instantiate(resourceColumnTemplate, buttonTMP.transform.GetChild(2), false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = j.Name;
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>-" + j.Cnt * j.Consume[index] + "</b>/天";
					total -= j.Cnt * j.Consume[index];
				}
				
				foreach (var j in jobList.ScheduleList)
				{
					if (j.Cnt == 0 || j.Produce.Count <= index)
						continue;
					if (j.Produce[index] == 0)
						continue;

					var columnTMP = Instantiate(resourceColumnTemplate, buttonTMP.transform.GetChild(2), false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = j.Name;
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>+" + j.Cnt * j.Produce[index] + "</b>/天";
					total += j.Cnt * j.Produce[index];
				}
				
				foreach (var j in jobList.ExtraList)
				{
					if (j.Cnt == 0 || j.Consume.Count <= index)
						continue;
					if (j.Consume[index] == 0)
						continue;

					var columnTMP = Instantiate(resourceColumnTemplate, buttonTMP.transform.GetChild(2), false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = j.Name;
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>-" + j.Cnt * j.Consume[index] + "</b>/天";
					total -= +j.Cnt * j.Consume[index];
				}
				
				foreach (var j in jobList.ExtraList)
				{
					if (j.Cnt == 0 || j.Produce.Count <= index)
						continue;
					if (j.Produce[index] == 0)
						continue;

					var columnTMP = Instantiate(resourceColumnTemplate, buttonTMP.transform.GetChild(2), false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = j.Name;
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>+" + j.Cnt * j.Produce[index] + "</b>/天";
					total += j.Cnt * j.Produce[index];
				}

				if (total != 0)
				{
					var columnTMP = Instantiate(resourceColumnTemplate, buttonTMP.transform.GetChild(2), false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "总计";
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>" + (total > 0 ? "+" : "") + total + "</b>/天";
				}
			}
		}

		private void RectChange()
		{
			_rectBorder.anchoredPosition = _rectList.anchoredPosition;
			_rectBorder.sizeDelta = _rectList.sizeDelta;
			_rectBorder.anchorMin = _rectList.anchorMin;
			_rectBorder.anchorMax = _rectList.anchorMax;
			_rectBorder.pivot = _rectList.pivot;
		}
	}
}