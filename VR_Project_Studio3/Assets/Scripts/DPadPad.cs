using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class DPadPad : MonoBehaviour
{
    GameSetup setup;
    public EmoteSending emoteSender;

    public Image uiEmoteReceive;
    public Image uiEmoteOverride;
    public Image uiEmoteSending;

    public Animator animator;

    public Image[] emoteButtonSprites;

    public MeshRenderer[] renderers;


    public Material buttonReady;
    public Material buttonPressed;

    bool pressOkay;
    bool assignAttempt;


    //Variables for analytics
    [HideInInspector] public static int[] dpadEmoteSendSuccess = new int[8];
    [HideInInspector] public static int dpadEmoteTotal;


    // Start is called before the first frame update
    void Awake()
    {
        setup = FindObjectOfType<GameSetup>();
        assignAttempt = false;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        pressOkay = true;

        if (emoteSender == null)
        {
            GameObject.Find("EmoteManager").GetComponent<EmoteSending>();
            assignAttempt = true;

        }
        if (emoteSender == null && assignAttempt == true) { Debug.LogError("Failed to aquire Emote Manager."); }

        if (emoteSender != null)
        {
            emoteSender.dPad = this;

            for (int i = 0; i < 4; i++)
            {
                emoteButtonSprites[i].sprite = emoteSender.emoteSprites[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DPadSend(int emote)
    {
        if (pressOkay && emoteSender != null) StartCoroutine(DPadSendTimer(emote));
        else if (pressOkay && emoteSender == null) Debug.LogError("The Emote Manager could not be found, but I swear I already told you that.");
        else { Debug.Log("Hold your horses, you can't just fling emotes willy-nilly. Be patient, thanks."); }
    }

    public void DPadReceive(int emote)
    {
        Debug.Log("JASPER'S SHOOP-DE-DOOP: Dpad has received RPC call successfully");
        StartCoroutine(DPadReceiveSequence(emote));
    }

    IEnumerator DPadReceiveSequence(int emote) //Lays out timing to receive emote
    {
        Debug.Log("JASPER'S SHOOP-DE-DOOP: Dpad is waiting to receive");
        while ((!animator.GetCurrentAnimatorStateInfo(1).IsName("Chumby") && !animator.GetCurrentAnimatorStateInfo(1).IsName("HasReceived")))
        {
            yield return null;
        } //Holds Coroutine if Transitioning or in transition anim
        Debug.Log("Sequence Can Proceed");

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Chumby"))
        {
            animator.SetBool("ReceivedFirst", false);
            DPadFromOpen(emote);
            Debug.Log("JASPER'S SHOOP-DE-DOOP: Dpad has received first emote");
        }
        else
        {
            animator.SetBool("ReceivedFirst", true);
            DPadFromOpen(emote);
            Debug.Log("JASPER'S SHOOP-DE-DOOP: Dpad has received subsequent emote");
        }
    }

    void DPadFromOpen(int emote)
    {
        if(animator.GetBool("ReceivedFirst")) uiEmoteOverride.sprite = uiEmoteReceive.sprite;

        uiEmoteReceive.sprite = emoteSender.emoteSprites[emote];
        animator.SetTrigger("ReceiveFromDiver");
    }

    IEnumerator DPadSendTimer(int emote)
    {
        Debug.Log("JASPER'S SHOOP-DE-DOOP: Dpad has started send timer.");
        dpadEmoteSendSuccess[emote]++;
        dpadEmoteTotal++;
        emoteSender.ToDiver(emote);
        uiEmoteSending.sprite = emoteSender.emoteSprites[emote];
        animator.SetTrigger("SendToDiver");
        pressOkay = false;
        renderers[emote].material = buttonPressed;

        yield return new WaitForSeconds(3f);

        pressOkay = true;
        renderers[emote].material = buttonReady;
        Debug.Log("JASPER'S SHOOP-DE-DOOP: Dpad has finished send timer");
    }

    private void OnApplicationQuit()
    {
        if (!setup.isVRPlayer)
        {
            AnalyticsResult resultDpadSent = Analytics.CustomEvent(
                "IntelligenceEmoteSent",
                new Dictionary<string, object>
                {
                { "Emote_Happy", dpadEmoteSendSuccess[0]},
                { "Emote_Sad", dpadEmoteSendSuccess[1]},
                { "Emote_Exclaim", dpadEmoteSendSuccess[2]},
                { "Emote_Query", dpadEmoteSendSuccess[3]},
                { "Emote_Up", dpadEmoteSendSuccess[4]},
                { "Emote_Down", dpadEmoteSendSuccess[5]},
                { "Emote_Left", dpadEmoteSendSuccess[6]},
                { "Emote_Right", dpadEmoteSendSuccess[7]},
                { "Emote_Total", dpadEmoteTotal}
                }
                );
            Debug.Log("Intel Emote Result: " + resultDpadSent);
        }
    }
}
