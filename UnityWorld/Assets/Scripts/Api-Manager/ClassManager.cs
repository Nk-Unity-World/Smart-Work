using System;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    [Serializable]
    public class LoginStatus
    {
        public int code;
        public string message;
    }
}
