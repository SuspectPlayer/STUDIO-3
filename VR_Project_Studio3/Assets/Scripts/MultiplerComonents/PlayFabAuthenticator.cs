//Written by Jack
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
    public GameObject loginGRP;

    public GameObject[] disableOnFail;
    public GameObject[] enableOnAuthentication;

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
    }

    private void Update()
    {
        displayClues.text = "Clues: " + clues;
        displayRunes.text = "Runes: " + runes;
    }

    //Requests to authenticate the player using their login details
    public void AuthenticateWithPlayFabLogin()
    {
        Debug.Log("PlayFab authenticating using Custom ID...");
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = login_User.text;
        request.Password = login_Pass.text;
        username = login_User.text;

        request.InfoRequestParameters = infoRequest;

        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnError);
    }
    
    //Requests to create a new account with the inputed details
    public void AuthenticateWithPlayFabRegister()
    {
        
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Username = register_User.text;
        request.Password = register_Pass.text;
        request.Email = register_Email.text;

        PlayFabClientAPI.RegisterPlayFabUser(request, result => { Debug.Log("Account Made"); AuthenticateWithPlayFabAfterReg(); }, OnError);
    }

    //Authenticate the player with their newly registered account
    void AuthenticateWithPlayFabAfterReg()
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = register_User.text;
        request.Password = register_Pass.text;
        username = register_User.text;

        request.InfoRequestParameters = infoRequest;

        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnError);
    }
    //Requests data from the players account
    void RequestToken(LoginResult result)
    {
        Debug.Log("PlayFab authenticated. Requesting photon token...");

        //Requests to see what items are in the player inventory and how much curreny they have
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

        //Requests to use the photon server settings
        _playFabPlayerIdCache = result.PlayFabId;
        GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest();
        request.PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;

        PlayFabClientAPI.GetPhotonAuthenticationToken(request, AuthenticatWithPhoton, OnError);
    }

    //Connects to the photon servers if it passed through the authentiction process
    void AuthenticatWithPhoton(GetPhotonAuthenticationTokenResult result)
    {
        Debug.Log("Photon token acquired: " + result.PhotonCustomAuthenticationToken + "  Authentication complete.");
        var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);
        customAuth.AddAuthParameter("token", result.PhotonCustomAuthenticationToken);
        PhotonNetwork.AuthValues = customAuth;

        LoginManager.ConnectToPhotonServer(username);
        foreach (GameObject g in enableOnAuthentication)
        {
            g.SetActive(true);
        }
    }

    //Produces a error message if the authentication process find an error
    void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        foreach (GameObject g in disableOnFail)
        {
            g.SetActive(false);
        }

        errorPopup.SetActive(true);
        string message = error.GenerateErrorReport();
        if (message.Contains("Password: The Password field is required."))
        {
            errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = "Password is Missing";
        }
        else if (message.Contains("Username: The Username field is required."))
        {
            errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = "Username is Missing";
        }
        else if (message.Contains("Username: Username must be between 3 and 20 characters.") && loginGRP.activeSelf == true)
        {
            errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = "Invalid username or password";
        }
        else if (message.Contains("Password: Password must be between 6 and 100 characters.") && loginGRP.activeSelf == true)
        {
            errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = "Invalid username or password";
        }
        else if (message.Contains("Username: Username must be between 3 and 20 characters.") && loginGRP.activeSelf == false)
        {
            errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = "Username must be between 3 and 20 characters";
        }
        else if (message.Contains("Password: Password must be between 6 and 100 characters.") && loginGRP.activeSelf == false)
        {
            errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = "Password must be between 6 and 100 characters";
        }
        else if (message.Contains("Email: Email address is not valid."))
        {
            errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = "Invalid Email address";
        }
        else
        {
            errorPopup.GetComponentInChildren<TextMeshProUGUI>().text = error.ErrorMessage;
        }
    }
    //Method for the player to by a specific item if they have enough currency
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
    //Method for the player to add more curreny to their account 
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
