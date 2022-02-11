using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SessionModel
{
    public string access_token;
    public string token_type;
    public string expires_at;
    public UserModel user;
}
