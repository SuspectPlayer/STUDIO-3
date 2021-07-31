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

    public int runes = 0;
    public int clues = 0;
    public GetPlayerCombinedInfoRequestParams infoRequest;
    public TextMeshProUGUI displayRunes;
    public TextMeshProUGUI displayClues;

    public GameObject errorPopup;

    public void Awake()
    {
        PlayFabSettings.TitleId = "D61DD";
        //AuthenticateWithPlayFab();
    }

    private void Update()
    {
        displayClues.text = "Clues: " + clues;
        displayRunes.text = "Runes: " + runes;
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

        request.InfoRequestParameters = infoRequest;

        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnError);
    }
    
    public void AuthenticateWithPlayFabRegister()
    {
        
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Username = register_User.text;
        request.Password = register_Pass.text;
        request.Email = register_Email.text;

        PlayFabClientAPI.RegisterPlayFabUser(request, result => { Debug.Log("Account Made"); AuthenticateWithPlayFabAfterReg(); }, OnError);
    }

    void AuthenticateWithPlayFabAfterReg()
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = register_User.text;
        request.Password = register_Pass.text;
        username = register_User.text;

        request.InfoRequestParameters = infoRequest;

        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnError);
    }

    void RequestToken(LoginResult result)
    {
        Debug.Log("PlayFab authenticated. Requesting photon token...");


        GetUserInventoryRequest inv = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(inv, invResult => 
        { 
            List<ItemInstance> items = invResult.Inventory;
            if(items.Count > 0)
            {
                clues = result.InfoResultPayload.UserInventory[0].RemainingUses.Value;
            }
            else
            {
                clues = 0;
            }
        }, OnError);
        
        runes = result.InfoResultPayload.UserVirtualCurrency["RU"];

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
        foreach (GameObject g in enableOnAuthentication)
        {
            g.SetActive(true);
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        foreach (GameObject g in disableOnFail)
        {
            g.SetActive(false);
        }

        errorPopup.SetActive(true);
        errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = error.ErrorMessage;
    }

    public void BuyItem(string itemID)
    {
        PurchaseItemRequest pr = new PurchaseItemRequest();
        pr.CatalogVersion = "Player Clues";
        pr.ItemId = itemID;
        pr.VirtualCurrency = "RU";
        pr.Price = 2;

        PlayFabClientAPI.PurchaseItem(pr, result => 
        {
            runes -= 2;
            clues += 1;
            Debug.Log("Item Purchased: " + result.Items[0].DisplayName); 
        }, OnError);
    }

    public void AddCurrency(int amount)
    {
        AddUserVirtualCurrencyRequest addRunes = new AddUserVirtualCurrencyRequest();

        addRunes.VirtualCurrency = "RU";
        addRunes.Amount = amount;

        PlayFabClientAPI.AddUserVirtualCurrency(addRunes, result => 
        {
            runes += amount;
        }, OnError);

    }
}
