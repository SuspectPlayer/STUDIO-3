using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviours : MonoBehaviour
{
    public EmoteSending emoteSender;
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
    [Header("Wrist UI specific emote components")]
    public Image uiEmoteJustSent;
    public Image uiEmoteReceiveRecent;
    public Image uiEmoteReceivePrevious;
    public Image[] emoteButtons;

    //Tabs
    [Space(5)]
    [Header("Wrist UI Tab Objects")]
    public GameObject switchToSend;
    public GameObject switchToReceive;

    


    void Awake()
    {
        emoteSender = GameObject.Find("EmoteManager").GetComponent<EmoteSending>();

        emoteSender.uiObject = this;

        for (int i = 0; i < 4; i++)
        {
            emoteButtons[i].sprite = emoteSender.emoteSprites[i];
        }
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

    public void SendEmoteToIntelligence(int emoteIndex)
    {
        emoteSender.ToIntelligence(emoteIndex);
    }

    public void ReceiveFromIntelligence(int emoteIndex)
    {
        StartCoroutine(Receive(emoteIndex));
    }

    void MoveSprite()
    {
        //Do the Thing
    }

    IEnumerator Receive(int emote)
    {
        //Play Some doopity dooo
        uiEmoteReceiveRecent.sprite = emoteSender.emoteSprites[emote];

        while (wristCanvas.activeInHierarchy == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(10f);

        MoveSprite();
    }
}
