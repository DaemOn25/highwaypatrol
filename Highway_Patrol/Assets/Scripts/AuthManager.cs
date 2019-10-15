using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using FullSerializer;

public class AuthManager : MonoBehaviour
{
    public static string localId;
    public static string playerName;
    public static string idToken;
    private string getLocalId;


    private static string dbUrl = "https://highway-patrol-48c8d.firebaseio.com/users";
    private string authKey = "AIzaSyDM6Bty_vdpL2uVT-QYx_6B-7zBHHDNH9g";

    public InputField unameText;
    public InputField emailText;
    public InputField passwordText;

    public Button signupButton;
    public Button loginButton;
    public Button signchkButton;
    public Button loginchkButton;

    public Text loginText;
    public Text signupText;
    public Text chkText;
    public Text signchkText;
    public Text loginchkText;
    public Text tb;                      //text box instance

    public static fsSerializer serializer = new fsSerializer();   //object for full serializer 

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void SignUpButton()
    {   
        if (unameText.text != "")      //checking if username field is empty
        {
            SignUpUser(emailText.text, unameText.text, passwordText.text);
        }
        else
        {
            Debug.Log("Username field empty");
            tb.text = "Console: " + "Username field empty.";
        }

    }

    public void SignInButton()
    {
        
        SignInUser(emailText.text, passwordText.text);
    }

    public static void PostToDatabase(bool emptyScore = false , string idTokentemp ="")
    {
        User user = new User();

        if(idTokentemp =="")
        {
            idTokentemp = idToken;
        }

        if (emptyScore)
        {
            UiManager.score = 0;
        }
                                        //RestClient.Post("https://highway-patrol-48c8d.firebaseio.com/.json" , user );
        RestClient.Put(dbUrl + "/" + localId + ".json?auth=" + idToken, user);
    }

    private void RetrieveFromDatabase()
    {
        User user = new User();
        RestClient.Get<User>(dbUrl + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            user = response;
        });

    }

    private void SignUpUser(string email, string username, string password)
    {
       
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<AuthResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + authKey, userData).Then(response =>
        {

            string emailVerification = "{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + response.idToken + "\"}";
            RestClient.Post("https://www.googleapis.com/identitytoolkit/v3/relyingparty/getOobConfirmationCode?key=" + authKey,emailVerification);
            //^this post request sent confirmation email
            
            localId = response.localId;
            playerName = username;
            //idToken = response.idToken;                 //not storing id token while signup coz need to verify while sigin

            Debug.Log(localId);
            Debug.Log(playerName);

            PostToDatabase(true , response.idToken);
            tb.text = "Console: " + "SignUp successful, verify email before login";

        }).Catch(error =>
        {
            Debug.Log(error);
            tb.text = "Console: " + "Signup failed, try again";
        });
    }

    private void SignInUser(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<AuthResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + authKey, userData).Then(response =>
        {
            string emailVerification = "{\"idToken\":\"" + response.idToken + "\"}";
            RestClient.Post(
                   "https://www.googleapis.com/identitytoolkit/v3/relyingparty/getAccountInfo?key=" + authKey,
                   emailVerification).Then(
                   emailResponse =>
                   {

                       fsData emailVerificationData = fsJsonParser.Parse(emailResponse.Text);
                       EmailConfirm emailConfirmationInfo = new EmailConfirm();
                       serializer.TryDeserialize(emailVerificationData, ref emailConfirmationInfo).AssertSuccessWithoutWarnings();

                       if (emailConfirmationInfo.users[0].emailVerified)
                       {
                           idToken = response.idToken;
                           localId = response.localId;
                           GetUsername();

                           Debug.Log(localId);
                           Debug.Log(idToken);
                           Debug.Log("SigIn successful");
                           tb.text = "Console: " + "Login successful";

                           SceneManager.LoadScene("Home_Menu");
                       }
                       else
                       {
                           Debug.Log("You are stupid, you need to verify your email dumb");
                           tb.text = "Console: " + "Login failed, email not verified";
                       }
                   });
           
        }).Catch(error =>
        {
            Debug.Log(error);
            tb.text = "Console: " + "Login failed, email/password incorrect";
        });
    }

    private void GetUsername()
    {
        RestClient.Get<User>(dbUrl + "/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            playerName = response.userName;
            Debug.Log(playerName);
        });
    }

    //private void GetLocalId()                              //method to return the localId of the user using his username.
    //{
    //    RestClient.Get(dbUrl + ".json?auth=" + idToken).Then(response =>
    //    {
    //        var username = playerName;

    //        fsData userData = fsJsonParser.Parse(response.Text);
    //        Dictionary<string, User> users = null;
    //        serializer.TryDeserialize(userData, ref users);

    //        foreach (var user in users.Values)
    //        {
    //            if (user.userName == username)
    //            {
    //                getLocalId = user.localId;
    //                RetrieveFromDatabase();
    //                break;
    //            }
    //        }
    //    }).Catch(error =>
    //    {
    //        Debug.Log(error);
    //    });
    //}

    //private int UnameAvail(string tempUname)                              //method to check if username exists in the db
    //{
    //    int flag = 1;

    //    RestClient.Get(dbUrl + ".json?auth=" + idToken).Then(response =>
    //    {
    //        var checkUname = tempUname;

    //        fsData userData = fsJsonParser.Parse(response.Text);
    //        Dictionary<string, User> users = null;
    //        serializer.TryDeserialize(userData, ref users);

    //        foreach (var user in users.Values)
    //        {
    //            if (user.userName == checkUname)
    //            {
    //                Debug.Log(user.userName + "f");
    //                Debug.Log(checkUname);
    //                flag = 0;
    //                //getLocalId = user.localId;
    //                //RetrieveFromDatabase();
    //            }
    //        }
    //    }).Catch(error =>
    //    {
    //        Debug.Log(error);
    //    });

    //    return flag;
    
    //}


    public void Exit()
    {
        Application.Quit();
    }

    public void SignUpChk()
    {
        chkText.gameObject.SetActive(true);
        signchkText.gameObject.SetActive(false);
        loginchkText.gameObject.SetActive(true);
        signchkButton.gameObject.SetActive(false);
        loginchkButton.gameObject.SetActive(true);

        unameText.gameObject.SetActive(true);
        emailText.gameObject.SetActive(true);
        passwordText.gameObject.SetActive(true);
        signupButton.gameObject.SetActive(true);
        signupText.gameObject.SetActive(true);

        loginText.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(false);
    }

    public void SignInChk()
    {
        chkText.gameObject.SetActive(true);
        signchkText.gameObject.SetActive(true);
        loginchkText.gameObject.SetActive(false);
        signchkButton.gameObject.SetActive(true);
        loginchkButton.gameObject.SetActive(false);
       
        unameText.gameObject.SetActive(false);
        emailText.gameObject.SetActive(true);
        passwordText.gameObject.SetActive(true);
        loginButton.gameObject.SetActive(true);
        loginText.gameObject.SetActive(true);

        signupText.gameObject.SetActive(false);
        signupButton.gameObject.SetActive(false);
    }
}
