using TMPro;
using System;
using UnityEngine;

namespace TutorialInfo.Scripts
{
    public class Account : MonoBehaviour
    {
        public GameObject userName, userPwd;

        private Message _message;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _message = FindObjectOfType<Message>();
            
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            _message.Add(GameMessage.InitMessage[UnityEngine.Random.Range(0, GameMessage.InitMessage.Count)]);
        }

        private Tuple<string, string> Click()
        {
            var nameTMP = userName.transform.GetComponent<TMP_InputField>().text;
            var pwdTMP = userPwd.transform.GetComponent<TMP_InputField>().text;
            return new Tuple<string, string>(nameTMP, pwdTMP);
        }

        public void SignUp()
        {
            var tmp = Click();
            
            PlayerPrefs.SetString("username", tmp.L);
            PlayerPrefs.SetString("password", tmp.R);
            
            _message.Add("注册成功");
            Init();
        }

        public void LogIn()
        {
            var tmp = Click();
            
            var storedUsername = PlayerPrefs.GetString("username");
            var storedPassword = PlayerPrefs.GetString("password");

            if (tmp.L == storedUsername && tmp.R == storedPassword)
            {
                _message.Add("登录成功");
                Init();
            }
            else
                _message.Add("用户名或密码错误");
        }

        private static void Init()
        {
            FindObjectOfType<SwitchRoom>().RoomInit();
        }
    }
}
