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
    [Header("Wrist UI 'Send' Objects")]
    public GameObject sendTitle;
    public GameObject sendContent;
    //Send Emotes
    [Space(5)]
    [Header("Wrist UI 'Receive' Objects")]
    public GameObject receiveTitle;
    public GameObject receiveContent;
    //Emotes For Get Emotes
    [Space(5)]
    [Header("Wrist UI specific emote Objects")]
    public GameObject[] uiEmoteReceive;

    //Tabs
    [Space(5)]
    [Header("Wrist UI Tab Objects")]
    public GameObject switchToSend;
    public GameObject switchToReceive;

    //EmoteStates
    [Space(5)]
    [Header("Wrist UI Receive Emote booleans")]
    [SerializeField]
    bool[] m_EmoteList = new bool[8];

    public bool[] emoteList { get { return m_EmoteList; } set { m_EmoteList = value; } }


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
        
    }

    public void SwitchTab(bool openSendWindow)
    {
        switchToSend.SetActive(!openSendWindow);
        switchToReceive.SetActive(openSendWindow);

        sendTitle.SetActive(openSendWindow);
        sendContent.SetActive(openSendWindow);
        receiveTitle.SetActive(!openSendWindow);
        receiveContent.SetActive(!openSendWindow);
    }

    public void SendEmoteToDiver(int emoteIndex)
    {
        
    }

    public void SendEmoteToIntelligence(int emoteIndex)
    {

    }

}
