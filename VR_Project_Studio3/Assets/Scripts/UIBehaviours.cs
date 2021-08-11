using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class UIBehaviours : MonoBehaviour
{
    GameSetup setup;
    public EmoteSending emoteSender;
    public Animator animator;
    ///UI Elements
    //UI Root
    [Header("Wrist UI Main GameObject")]
    public GameObject wristCanvas;
    [Space(5)]
    public GameObject outBox;
    public GameObject inBox;
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

    bool firstReceived = false;

    [Space(5)]
    public float timeTilNextEmote = 3f;
    bool canNextEmote = true;
    bool receiveAlert = false;

    //Variables for analytics
    [HideInInspector]public static int[] uiEmoteSendSuccess = new int[4];
    [HideInInspector] public static int uiEmoteTotal;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        setup = FindObjectOfType<GameSetup>();

        emoteSender = GameObject.Find("EmoteManager").GetComponent<EmoteSending>();

        emoteSender.uiObject = this;

        animator = wristCanvas.GetComponent<Animator>();

        for (int i = 0; i < 4; i++)
        {
            emoteButtons[i].sprite = emoteSender.emoteSprites[i];
        }

        uiEmoteJustSent.enabled = false;
        uiEmoteReceivePrevious.enabled = false;
        uiEmoteReceiveRecent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(emoteSender == null)
        {
            emoteSender = GameObject.Find("EmoteManager").GetComponent<EmoteSending>();

            emoteSender.uiObject = this;

            for (int i = 0; i < 4; i++)
            {
                emoteButtons[i].sprite = emoteSender.emoteSprites[i];
            }
        }
    }

    public void OpenToCorrect() //Called from Player's UIOpener. Opens UI to last opened panel.
    {
        animator.SetBool("IsSend", outBox.activeSelf);
        animator.SetTrigger("SelfOpen");
    }

    public void Close() //Called from Player's UIOpener. 
    {
        animator.SetTrigger("SelfClose");
    }

    public void SwitchTab(bool openSendWindow) //Attached to SwitchTab Button (true = open send/close receive, false = close send/open receive)
    {
        switchToSend.SetActive(!openSendWindow);
        switchToReceive.SetActive(openSendWindow);

        StartCoroutine(QueueTillAtOpenSwitch());

    }

    public void SendEmoteToIntelligence(int emoteIndex) //Attach to Diver's Emote Buttons
    {
        if (canNextEmote)
        {
            StartCoroutine(EmoteTimer());
            StartCoroutine(QueueTillSendReady(emoteIndex));
            uiEmoteSendSuccess[emoteIndex]++;

        }
        else
        {
            Debug.Log("Impatience.");
        }
        
    }

    public void ReceiveFromIntelligence(int emoteIndex) // Called from Emote Sender
    {
        StartCoroutine(Receive(emoteIndex));
        receiveAlert = true; // Set some alerter active
    }

    IEnumerator Receive(int emote) //Sequences Receiving Emotes
    {
        Debug.Log("JASPER'S SHOOP-DE-DOOP: DiverUI is waiting to receive");
        while (!inBox.activeInHierarchy || (!animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenS") && !animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenR")))
        {
            yield return null;
        }

        if (!firstReceived)
        {
            Debug.Log("JASPER'S SHOOP-DE-DOOP: DiverUI is receiving first emote");
            uiEmoteReceiveRecent.enabled = true;
            uiEmoteReceiveRecent.sprite = emoteSender.emoteSprites[emote];
            animator.SetBool("DoTheGet", true);
            animator.SetBool("SelfReceivedFirst", false);
            animator.SetTrigger("SelfSendReceive");
            firstReceived = true;
        }
        else
        {

            uiEmoteReceivePrevious.enabled = true;
            uiEmoteReceivePrevious.sprite = uiEmoteReceiveRecent.sprite;
            animator.SetBool("DoTheGet", true);
            animator.SetBool("SelfReceivedFirst", true);
            uiEmoteReceiveRecent.sprite = emoteSender.emoteSprites[emote];
            animator.SetTrigger("SelfSendReceive");
        }
        receiveAlert = false; // Set some alerter inactive
    }

    IEnumerator EmoteTimer() //Sets timer to prevent emote spam
    {
        canNextEmote = false;
        yield return new WaitForSeconds(timeTilNextEmote);
        canNextEmote = true;
    }

    IEnumerator QueueTillAtOpenSwitch()
    {
        Debug.Log("JASPER'S SHOOP-DE-DOOP: DiverUI is waiting to switch");
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenS") && !animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenR")) yield return null;

        animator.SetBool("IsSend", outBox.activeSelf);
        animator.SetTrigger("SelfSwitch");
        yield return new WaitForSeconds(3f);
        animator.SetBool("IsSend", outBox.activeSelf);
        Debug.Log("JASPER'S SHOOP-DE-DOOP: DiverUI has switched");
    }

    IEnumerator QueueTillSendReady(int emote) //Sequences Sending Emotes
    {
        Debug.Log("JASPER'S SHOOP-DE-DOOP: DiverUI is waiting to send");
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenS")) yield return null;

        animator.SetBool("DoTheSend", true);
        animator.SetInteger("SelfWhichEmote", emote+1);

        uiEmoteJustSent.enabled = true;
        uiEmoteJustSent.sprite = emoteSender.emoteSprites[emote];

        animator.SetTrigger("SelfSendReceive");
        emoteSender.ToIntelligence(emote);
        Debug.Log("JASPER'S SHOOP-DE-DOOP: DiverUI has sent signal to Emote Sender");
    }

    private void OnApplicationQuit()
    {
        if (setup.isVRPlayer)
        {
            AnalyticsResult resultDiverSent = Analytics.CustomEvent(
                "DiverEmoteSent",
                new Dictionary<string, object>
                {
                { "Emote_HappyYay", uiEmoteSendSuccess[0]},
                { "Emote_SadYay", uiEmoteSendSuccess[1]},
                { "Emote_ExclaimYay", uiEmoteSendSuccess[2]},
                { "Emote_QueryYay", uiEmoteSendSuccess[3]},
                { "Emote_UpYay", uiEmoteSendSuccess[4]},
                { "Emote_DownYay", uiEmoteSendSuccess[5]},
                { "Emote_LeftYay", uiEmoteSendSuccess[6]},
                { "Emote_RightYay", uiEmoteSendSuccess[7]},
                { "Emote_TotalYay", uiEmoteTotal}
                }
            );
            Debug.Log("Diver Emote Result: " + resultDiverSent);
        }
    }
}
