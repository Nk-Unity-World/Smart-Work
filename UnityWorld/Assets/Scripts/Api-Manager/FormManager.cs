using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormManager : MonoBehaviour
{
    public class LoginDetails
    {
        public string username;
        public string password;

        public LoginDetails(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
