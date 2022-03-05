using UnityEngine;

public class CheckConnection : MonoBehaviour
{
    public static CheckConnection checkConnection=null;

    private void Awake()
    {
        if (checkConnection == null)
            checkConnection = this;
    }

    public bool CheckInternetConnection()
    {
        bool isConnectedToInternet = false;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            isConnectedToInternet = false;

            //===== Show No Internet Screen
        }
        else
        {
            isConnectedToInternet = true;
        }
        return isConnectedToInternet;
    }
}
