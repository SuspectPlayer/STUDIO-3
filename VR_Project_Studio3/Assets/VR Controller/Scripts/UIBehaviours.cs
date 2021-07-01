using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviours : MonoBehaviour
{
    ///UI Elements
    //UI Root
    [Header("Wrist UI Main GameObject")]
    public GameObject wristCanvas;
    //Main Options
    [Space(5)]
    [Header("Wrist UI 'Main Menu' Objects")]
    public GameObject mainTitle;
    public GameObject mainContent;
    //Send Emotes
    [Space(5)]
    [Header("Wrist UI 'Send Emote' Objects")]
    public GameObject sendEmotesTitle;
    public GameObject sendEmotesContent;
    //Get Emotes
    [Space(5)]
    [Header("Wrist UI 'Receive Emotes' Objects")]
    public GameObject getEmotesTitle;
    public GameObject getEmotesContent;
    //Emotes For Get Emotes
    [Space(5)]
    [Header("Wrist UI specific emote Objects")]
    public GameObject gotEmoteHappy;
    public GameObject gotEmoteSad;
    public GameObject gotEmoteQuery;
    public GameObject gotEmoteExclaim;
    public GameObject gotEmoteLeft;
    public GameObject gotEmoteRight;
    public GameObject gotEmoteUp;
    public GameObject gotEmoteDown;

    //Tabs
    [Space(5)]
    [Header("Wrist UI Tab Objects")]
    [Tooltip("This is Active when Main Wrist Menu is shown. Not interactable.")]
    public GameObject mainTabWhenActive;
    [Tooltip("This is Active when Main Wrist Menu is not shown. Interactable.")]
    public GameObject mainTabWhenInactive;
    [Tooltip("This is Active when Send Emote Menu is shown. Not interactable.")]
    public GameObject sendTabWhenActive;
    [Tooltip("This is Active when Send Emote Menu is not shown. Interactable.")]
    public GameObject sendTabWhenInactive;

    //EmoteStates
    [Space(5)]
    [Header("Wrist UI Receive Emote booleans")]
    [SerializeField]
    [Tooltip("True when 'Happy Emote' is received.")]
    bool m_EmoteHappy = false;
    [SerializeField]
    [Tooltip("True when 'Sad Emote' is received.")]
    bool m_EmoteSad = false;
    [SerializeField]
    [Tooltip("True when 'Query Emote' is received.")]
    bool m_EmoteQuery = false;
    [SerializeField]
    [Tooltip("True when 'Exclaim Emote' is received.")]
    bool m_EmoteExclaim = false;
    [SerializeField]
    [Tooltip("True when 'LeftArrow Emote' is received.")]
    bool m_EmoteLeft = false;
    [SerializeField]
    [Tooltip("True when 'RightArrow Emote' is received.")]
    bool m_EmoteRight = false;
    [SerializeField]
    [Tooltip("True when 'UpArrow Emote' is received.")]
    bool m_EmoteUp = false;
    [SerializeField]
    [Tooltip("True when 'DownArrow Emote' is received.")]
    bool m_EmoteDown = false;

    public bool emoteHappy { get { return m_EmoteHappy; } set { m_EmoteHappy = value; } }
    public bool emoteSad { get { return m_EmoteSad; } set { m_EmoteSad = value; } }
    public bool emoteQuery { get { return m_EmoteQuery; } set { m_EmoteQuery = value; } }
    public bool emoteExclaim { get { return m_EmoteExclaim; } set { m_EmoteExclaim = value; } }

    public bool emoteLeft { get { return m_EmoteLeft; } set { m_EmoteLeft = value; } }
    public bool emoteRight { get { return m_EmoteRight; } set { m_EmoteRight = value; } }
    public bool emoteUp { get { return m_EmoteUp; } set { m_EmoteUp = value; } }
    public bool emoteDown { get { return m_EmoteDown; } set { m_EmoteDown = value; } }


    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Set ReceiveEmote UI Active State based on ReceiveEmote booleans.
        gotEmoteHappy.SetActive(m_EmoteHappy);
        gotEmoteSad.SetActive(m_EmoteSad);
        gotEmoteQuery.SetActive(m_EmoteQuery);
        gotEmoteExclaim.SetActive(m_EmoteExclaim);
        gotEmoteLeft.SetActive(m_EmoteLeft);
        gotEmoteRight.SetActive(m_EmoteRight);
        gotEmoteUp.SetActive(m_EmoteUp);
        gotEmoteDown.SetActive(m_EmoteDown);
    }

    public void SwitchToMain()
    {
        mainTabWhenActive.SetActive(true);
        mainTabWhenInactive.SetActive(false);
        sendTabWhenActive.SetActive(true);
        sendTabWhenInactive.SetActive(false);

        mainTitle.SetActive(true);
        mainContent.SetActive(true);

        sendEmotesTitle.SetActive(false);
        sendEmotesContent.SetActive(false);
    }

    public void SwitchToSend()
    {
        mainTabWhenActive.SetActive(false);
        mainTabWhenInactive.SetActive(true);
        sendTabWhenActive.SetActive(false);
        sendTabWhenInactive.SetActive(true);

        mainTitle.SetActive(false);
        mainContent.SetActive(false);

        sendEmotesTitle.SetActive(true);
        sendEmotesContent.SetActive(true);
    }

    public void SwitchToReceive()
    {
        //I don't know what to put here yet; But this is for when any emote is received. 
    }

    public void SendEmoteHappy()
    {
        Debug.Log("Happy Emote Sent");
    }

    public void SendEmoteSad()
    {
        Debug.Log("Sad Emote Sent");
    }

    public void SendEmoteQuery()
    {
        Debug.Log("Query Emote Sent");
    }

    public void SendEmoteExclaim()
    {
        Debug.Log("Exclaim Emote Sent");
    }

}
