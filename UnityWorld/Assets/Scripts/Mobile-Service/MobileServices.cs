using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

//============= Nk
[CreateAssetMenu(fileName = "MobileService", menuName = "NkObjects/MobileService", order = 2)]
public class MobileServices : ScriptableObject
{
	public enum ADDRESSFAM
	{
		IPv4, IPv6
	}

	
	public void BtnRateUsClick()
	{
		Application.OpenURL ("https://play.google.com/store/apps/details?id=" + Application.identifier);
	}


	public void BtnMoreGamesClick()
	{
		Application.OpenURL ("https://play.google.com/store/apps/developer?id=" + Application.companyName);
	}

	public void BtnShareGplayURL()
	{
		BtnShare("Download " + Application.productName + " from https://play.google.com/store/apps/details?id=" + Application.identifier + "&hl=en");
	}

	public void BtnShare(string strMsg)
	{
		#if UNITY_ANDROID
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), strMsg);
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("startActivity", intentObject);
#elif UNITY_IOS
		GeneralSharingiOSBridge.ShareSimpleText (strMsg);
#endif
	}

	public void BtnQuitClick()
	{
		Application.Quit();
	}


	public void ShowAndroidToastMessage(string message)
	{
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		if (unityActivity != null)
		{
			AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
			unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			{
				AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
				toastObject.Call("show");
			}));
		}
	}


	public void CopyTextOnClipboard(string msg)
    {
		TextEditor editor = new TextEditor
		{
			text = msg
		};
		editor.SelectAll();
		editor.Copy();

		ShowAndroidToastMessage("Text Copied");
	}


	public void ShowIPAdd()
	{
		string ipAddress = GetIP(ADDRESSFAM.IPv4);
		Debug.Log("ipAddress:" + ipAddress);
	}

	public static string GetIP(ADDRESSFAM Addfam)
	{
		if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
		{
			return null;
		}

		string output = "";

		foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
		{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
			NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
			NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

			if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif
			{
				foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
				{
					//IPv4
					if (Addfam == ADDRESSFAM.IPv4)
					{
						if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							output = ip.Address.ToString();
						}
					}

					//IPv6
					else if (Addfam == ADDRESSFAM.IPv6)
					{
						if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
						{
							output = ip.Address.ToString();
						}
					}
				}
			}
		}
		return output;
	}
}
