using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviours : MonoBehaviour
{
    public EmoteSending emoteSender;
    public Animator animator;
    ReorganiseChiddlers organ;
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

    bool seenEmote;

    [Space(5)]
    public float timeTilNextEmote = 3f;
    bool canNextEmote = true;

    void Awake()
    {
        emoteSender = GameObject.Find("EmoteManager").GetComponent<EmoteSending>();

        emoteSender.uiObject = this;

        animator = wristCanvas.GetComponent<Animator>();
        organ = wristCanvas.GetComponent<ReorganiseChiddlers>();

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
        if (wristCanvas.activeInHierarchy == true && receiveContent.activeInHierarchy == true)
        {
            seenEmote = true;
        }
        else
        {
            seenEmote = false;
        }
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
        //StartCoroutine(SiblingIndex(emoteIndex, 3));
        if (canNextEmote)
        {
            StartCoroutine(EmoteTimer());
            emoteSender.ToIntelligence(emoteIndex);
        }
        else
        {
            //Do some error stuff
        }
        
    }

    public void ReceiveFromIntelligence(int emoteIndex)
    {
        StartCoroutine(Receive(emoteIndex));
    }

    /*IEnumerator SiblingIndex(int child, int index)
    {
        organ.chiddlers[child].OrganiseChildIndex(index);
        yield return new WaitForSeconds(1f);
        organ.chiddlers[child].ResetChildIndex();
    }*/

    IEnumerator Receive(int emote)
    {
        uiEmoteReceiveRecent.sprite = emoteSender.emoteSprites[emote];

        while (!seenEmote)
        {
            yield return null;
        }

    }

    IEnumerator EmoteTimer()
    {
        canNextEmote = false;
        yield return new WaitForSeconds(timeTilNextEmote);
        canNextEmote = true;
    }
}
