using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AuthManager : MonoBehaviour
{
    public static string localId;
    public static string playerName;
    private string idToken;
    private static string dbUrl = "https://highway-patrol-48c8d.firebaseio.com/users";
    private string authKey = "AIzaSyDM6Bty_vdpL2uVT-QYx_6B-7zBHHDNH9g";

    public InputField unameText;
    public InputField emailText;
    public InputField passwordText;

    public GameObject toastObj;
    
    //UiManager ui = new UiManager();
    //ShowToast toast = new ShowToast();
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SignUpButton()
    {
        
        SignUpUser(emailText.text , unameText.text , passwordText.text);
     
    }

    public void SignInButton()
    {
        SignInUser(emailText.text , passwordText.text);
    }

    public static void PostToDatabase(bool emptyScore = false)
    {
        User user = new User();
        if (emptyScore)
        {
            UiManager.score = 0;
        }
        //RestClient.Post("https://highway-patrol-48c8d.firebaseio.com/.json" , user );
        RestClient.Put(dbUrl + "/" + localId + ".json", user);
    }

    private void RetrieveFromDatabase()
    {
        User user = new User();
        RestClient.Get<User>(dbUrl + "/" + localId + ".json").Then(response =>
        {
            user = response;
        });

    }

    private void SignUpUser(string email, string username, string password)
    {
        ShowToast toast = toastObj.AddComponent<ShowToast>();

        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<AuthResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + authKey, userData).Then(response =>
        {
            localId = response.localId;
            idToken = response.idToken;
            playerName = username;

            Debug.Log(localId);
            Debug.Log(idToken);
            Debug.Log(playerName);

            PostToDatabase(true);

        }).Catch(error =>
        {
            toast.ToastShow("Sign Up failed, Please try again");
            Debug.Log(error);
        });
    }

    private void SignInUser(string email, string password)
    {
        ShowToast toast = toastObj.AddComponent<ShowToast>();

        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<AuthResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + authKey, userData).Then(response =>
        {
            localId = response.localId;
            idToken = response.idToken;
            GetUsername();

            Debug.Log(localId);
            Debug.Log(idToken);

            SceneManager.LoadScene("Home_Menu");
        }).Catch(error =>
        {
            toast.ToastShow("Login failed, Username or Password is incorrect");
            Debug.Log(error);
        });
    }

    public void GetUsername()
    {
        RestClient.Get<User>(dbUrl + "/" + localId + ".json").Then(response =>
        {
           playerName = response.userName;
           Debug.Log(playerName);
        });
    }

}
