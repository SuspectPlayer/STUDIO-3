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
        setup = FindObjectOfType<GameSetup>();

        emoteSender = GameObject.Find("EmoteManager").GetComponent<EmoteSending>();

        emoteSender.uiObject = this;

        animator = wristCanvas.GetComponent<Animator>();

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

    public void OpenToCorrect() //For Animator. Opens UI to last opened panel.
    {
        animator.SetBool("IsSend", outBox.activeSelf);
        animator.SetTrigger("SelfOpen");
    }

    public void Close()
    {
        animator.SetTrigger("SelfClose");
    }

    public void SwitchTab(bool openSendWindow) //Attached to SwitchTab Button
    {
        switchToSend.SetActive(!openSendWindow);
        switchToReceive.SetActive(openSendWindow);

        StartCoroutine(QueueTillAtOpenSwitch());

        //sendTitle.SetActive(openSendWindow);
        //sendContent.SetActive(openSendWindow);
        //receiveTitle.SetActive(!openSendWindow);
        //receiveContent.SetActive(!openSendWindow);

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

    public void ReceiveFromIntelligence(int emoteIndex)
    {
        StartCoroutine(Receive(emoteIndex));
        receiveAlert = true; // Set some alerter active
    }

    IEnumerator Receive(int emote)
    {
        while (!inBox.activeInHierarchy || !animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenS") || !animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenR"))
        {
            yield return null;
        }

        if (firstReceived)
        {
            uiEmoteReceiveRecent.sprite = emoteSender.emoteSprites[emote];
            animator.SetBool("SelfReceivedFirst", firstReceived);
            animator.SetTrigger("SelfSendReceive");
        }
        else
        {
            uiEmoteReceivePrevious.sprite = uiEmoteReceiveRecent.sprite;
            animator.SetBool("SelfReceivedFirst", firstReceived);
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
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenS") || !animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenR")) yield return null;

        animator.SetBool("IsSend", outBox.activeSelf);
        animator.SetTrigger("SelfSwitch");
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("IsSend", outBox.activeSelf);
    }

    IEnumerator QueueTillSendReady(int emote)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenS") || !animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpenR")) yield return null;

        animator.SetInteger("SelfWhichEmote", emote+1);
        animator.SetTrigger("SelfSendReceive");
        emoteSender.ToIntelligence(emote);
    }

    private void OnApplicationQuit()
    {
        if (setup.isVRPlayer)
        {
            AnalyticsResult resultDiverSent = Analytics.CustomEvent(
                "DiverEmoteSent",
                new Dictionary<string, object>
                {
                { "Emote_Happy", uiEmoteSendSuccess[0]},
                { "Emote_Sad", uiEmoteSendSuccess[1]},
                { "Emote_Exclaim", uiEmoteSendSuccess[2]},
                { "Emote_Query", uiEmoteSendSuccess[3]},
                { "Emote_Up", uiEmoteSendSuccess[4]},
                { "Emote_Down", uiEmoteSendSuccess[5]},
                { "Emote_Left", uiEmoteSendSuccess[6]},
                { "Emote_Right", uiEmoteSendSuccess[7]},
                { "Emote_Total", uiEmoteTotal}
                }
            );
            Debug.Log("Diver Emote Result: " + resultDiverSent);
        }
    }
}
