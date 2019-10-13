using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User
{
    public int userScore;
    public string localId;
    public string userName;

    public User()
    {
        userScore = UiManager.score;
        localId = AuthManager.localId;
        userName = AuthManager.playerName;
    }
}
