using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
public class EnterGameButton : MonoBehaviour
{	
	public int			gate = 4;
	public string		serverip = "";
	public GameObject 	input;


	void Start()
	{
		if (input != null)
		{
			if( Application.platform == RuntimePlatform.Android ||
			   Application.platform == RuntimePlatform.IPhonePlayer)
			{
				input.SetActive(false);
			}
			else
			{
				/*
				input.SetActive(true);
				FileStream file =null;
				try 
				{
					file = new FileStream("Assets/account.txt", FileMode.Open);
				}catch(System.Exception e)
				{
					Debug.Log(e.Message);
				}

				if(file!=null)
				{
					byte[] data = new byte[file.Length];
					file.Read (data, 0, (int)file.Length);
					input.GetComponent<UIInput>().value = Encoding.UTF8.GetString(data);
					file.Close();
				}
				*/

			}
			
		}
	}
	
	void EnterGame()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer && 
		    Application.platform != RuntimePlatform.Android && input != null)
		{
			SDGlobal.ptUserName = input.GetComponent<UIInput>().value;	

			FileStream file = new FileStream("Assets/account.txt",FileMode.Create);
			byte[] data = Encoding.UTF8.GetBytes(SDGlobal.ptUserName);
			file.Write(data,0,data.Length);
			file.Close();

			// test serverip..
			if( serverip != "" )
                SDNetGlobal.Login_IP = serverip;
		}
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            gate = SDNetGlobal.serverId;
		else
			gate = 4;//BM_add
        
        GameObject goUIRoot = GameObject.Find("UI Root (2D)");
        if (goUIRoot)
        {
            LoginUI loginUI = goUIRoot.GetComponent<LoginUI>();
            loginUI.StartGame(gate);
        }
        

        
	}
	
	void OnClick()
	{
		if( gameObject.name == "bt_StartGame" )
		{
			EnterGame();
		}
		else if( gameObject.name == "bt_PlayVideo" )
		{
			GameObject mainCamera = GameObject.Find("@MainCamera");
			if( mainCamera!=null && (Application.platform==RuntimePlatform.Android || Application.platform==RuntimePlatform.IPhonePlayer) )
			{
				sdMovieVideo movie = mainCamera.GetComponent<sdMovieVideo>();
				if(movie != null)
				{
					movie.PlayMovie("DS_intro.mp4");
				}
			}

			//sdUICharacter.Instance.ShowMsgLine("错误提示测试！",MSGCOLOR.Red);
		}
		else if( gameObject.name == "bt_Username" )
		{
			Debug.Log("bt_Username button onclick");
            sdUICharacter.Instance.HideLoginMsg();
			GameObject goUIRoot = GameObject.Find("UI Root (2D)");
			if(goUIRoot)
			{
				Debug.Log("Find ui root");
				GHome.GetInstance().Logout((code, msg, data) =>
				{
					Debug.Log("GHome logout callback code: " + code + "msg: " + msg);

					if(code == 0)
					{
						LoginUI loginUI = goUIRoot.GetComponent<LoginUI>();
						loginUI.Relogin();
					}
				});
			}

			gameObject.SetActive(false);

		}
		else if( gameObject.name == "bt_Server" )
		{
            List<JsonNode> lst = SDNetGlobal.m_lstSrvInfo;
            if (lst.Count > 0)
                sdUICharacter.Instance.ShowSelectSrvWnd(true);
        }
	}
}
