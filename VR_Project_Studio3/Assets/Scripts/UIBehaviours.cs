using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviours : MonoBehaviour
{
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

    void Awake()
    {
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

    public void OpenToCorrect()
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
        }
        else
        {
            //Do some error stuff
        }
        
    }

    public void ReceiveFromIntelligence(int emoteIndex)
    {
        StartCoroutine(Receive(emoteIndex));
        receiveAlert = true; // Set some alerter active
    }

    IEnumerator Receive(int emote)
    {
        while (!inBox.activeInHierarchy || !animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpen"))
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

    IEnumerator EmoteTimer()
    {
        canNextEmote = false;
        yield return new WaitForSeconds(timeTilNextEmote);
        canNextEmote = true;
    }

    IEnumerator QueueTillAtOpenSwitch()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpen")) yield return null;

        animator.SetBool("IsSend", outBox.activeSelf);
        animator.SetTrigger("SelfSwitch");
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("IsSend", outBox.activeSelf);
    }

    IEnumerator QueueTillSendReady(int emote)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SelfUIIsOpen")) yield return null;

        animator.SetInteger("SelfWhichEmote", emote+1);
        animator.SetTrigger("SelfSendReceive");
        emoteSender.ToIntelligence(emote);
    }
}
