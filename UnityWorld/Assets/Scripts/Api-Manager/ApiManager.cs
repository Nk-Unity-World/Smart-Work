using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : MonoBehaviour
{
    public static ApiManager apiManager;

    const string strMainUrl = "Your Server URL";

    const string strLoginUrl = strMainUrl + "/login";//Api URL

    public ClassManager.LoginStatus loginStatus;

    public enum ApiMethod
    {
        NONE,
        LOGIN_METHOD,
    }

    private void Awake()
    {
        apiManager = this;
    }


    ///=========== Methods to call api
    public void LoginMethod(FormManager.LoginDetails loginDetails)
    {
        string form = JsonUtility.ToJson(loginDetails);
        StartCoroutine(IeWaitForWWW(form, strLoginUrl, ApiMethod.LOGIN_METHOD, "POST","NEW"));
    }


    ////=========== API Process POST/GET ========
    public IEnumerator IeWaitForWWW(string form, string url, ApiMethod apiMethod, string methodType, string server)//methodType is either "POST" or "GET"
    {
        UnityWebRequest uwr = new UnityWebRequest(url, methodType);

        if (form != string.Empty)
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(form);
            uwr.uploadHandler = new UploadHandlerRaw(bodyRaw); 
        }

        if (server == "NEW")//Check api requires Tocken:-"token" OR BearerTocken:-"Authorization" as an argument
        {
            uwr.SetRequestHeader("token", PlayerPrefs.GetString("newToken"));
        }
        else
        {
            uwr.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        }
        
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.downloadHandler = new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();

        if (!uwr.isNetworkError)
        {
            if (!string.IsNullOrEmpty(uwr.downloadHandler.text))
            {
                ReadJsonData(uwr.downloadHandler.text, apiMethod);
            }
        }
    }



    public void ReadJsonData(string JsonString, ApiMethod apiMethod)
    {
        if (apiMethod == ApiMethod.LOGIN_METHOD)
        {
            Debug.Log("LOGIN_METHOD == json String :" + JsonString);
            loginStatus = JsonUtility.FromJson<ClassManager.LoginStatus>(JsonString);

            if (loginStatus.code == 200)//result true
            {
                Debug.Log("Store Data");
            }
            else
            {
                Debug.Log("LOGIN_METHOD IN ERROR");
                if (loginStatus.message == "Unauthenticated.")
                {
                    Debug.Log(loginStatus.message);
                }
            }
        }
    }
}
