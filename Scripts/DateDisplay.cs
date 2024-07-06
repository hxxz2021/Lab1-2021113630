using TMPro;
using UnityEngine;

namespace TutorialInfo.Scripts
{
    public class DateDisplay : MonoBehaviour
    {
        public GameObject date;
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Refresh(int tmp)
        {
            date.transform.GetComponent<TextMeshProUGUI>().text = tmp.ToString();
        }
    }
}
