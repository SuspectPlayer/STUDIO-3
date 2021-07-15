using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayFabAuthenticator : MonoBehaviour
{
    public TMP_InputField login_User;
    public TMP_InputField login_Pass;
    
    public TMP_InputField register_User;
    public TMP_InputField register_Pass;
    public TMP_InputField register_Email;

    public GameObject[] disableOnFail;
    public GameObject[] enableOnAuthentication;
    //public GameObject[] disableOnAuthentication;

    private string _playFabPlayerIdCache;

    string username;

    public void Awake()
    {
        PlayFabSettings.TitleId = "D61DD";
        //AuthenticateWithPlayFab();
    }

    public void AuthenticateWithPlayFabLogin()
    {
        Debug.Log("PlayFab authenticating using Custom ID...");
        //LoginWithCustomIDRequest request = new LoginWithCustomIDRequest();
        //request.CreateAccount = true;
        //request.CustomId = PlayFabSettings.DeviceUniqueIdentifier;
        //PlayFabClientAPI.LoginWithCustomID(request, RequestToken, OnError);
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = login_User.text;
        request.Password = login_Pass.text;
        username = login_User.text;

        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnError);
        foreach (GameObject g in enableOnAuthentication)
        {
            g.SetActive(true);
        }
    }
    
    public void AuthenticateWithPlayFabRegister()
    {
        
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Username = register_User.text;
        request.Password = register_Pass.text;
        request.Email = register_Email.text;
        foreach (GameObject g in enableOnAuthentication)
        {
            g.SetActive(true);
        }

        PlayFabClientAPI.RegisterPlayFabUser(request, result => { Debug.Log("Account Made"); AuthenticateWithPlayFabAfterReg(); }, OnError);
    }

    void AuthenticateWithPlayFabAfterReg()
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = register_User.text;
        request.Password = register_Pass.text;
        username = register_User.text;

        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnError);
    }

    void RequestToken(LoginResult result)
    {
        Debug.Log("PlayFab authenticated. Requesting photon token...");
        _playFabPlayerIdCache = result.PlayFabId;
        GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest();
        request.PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;

        PlayFabClientAPI.GetPhotonAuthenticationToken(request, AuthenticatWithPhoton, OnError);
    }

    void AuthenticatWithPhoton(GetPhotonAuthenticationTokenResult result)
    {
        Debug.Log("Photon token acquired: " + result.PhotonCustomAuthenticationToken + "  Authentication complete.");
        var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);
        customAuth.AddAuthParameter("token", result.PhotonCustomAuthenticationToken);
        PhotonNetwork.AuthValues = customAuth;

        LoginManager.ConnecteToPhotonServer(username);
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        foreach (GameObject g in disableOnFail)
        {
            g.SetActive(false);
        }
    }
}
