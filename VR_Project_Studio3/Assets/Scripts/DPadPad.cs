using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPadPad : MonoBehaviour
{
    public EmoteSending emoteSender;

    public Image uiEmoteReceive;
    public Image uiEmoteOverride;

    public Animator animator;

    public Image[] emoteButtonSprites;

    public MeshRenderer[] renderers;


    public Material buttonReady;
    public Material buttonPressed;

    bool pressOkay;

    // Start is called before the first frame update
    void Awake()
    {
        emoteSender.dPad = this;

        for (int i = 0; i < 4; i++)
        {
            emoteButtonSprites[i].sprite = emoteSender.emoteSprites[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DPadSend(int emote)
    {
        if (pressOkay) StartCoroutine(DPadSendTimer(emote));
        else { }
    }

    public void DPadReceive(int emote)
    {
        StartCoroutine(DPadReceiveSequence(emote));
    }

    IEnumerator DPadReceiveSequence(int emote) //Lays out timing to receive emote
    {
        while(!animator.GetCurrentAnimatorStateInfo(2).IsName("EmoteBase") || !animator.GetCurrentAnimatorStateInfo(2).IsName("HasReceived") || animator.IsInTransition(2))
        {
            yield return null;
        } //Holds Coroutine if Transitioning or in transition anim
        Debug.Log("Sequence Can Proceed");

        if (animator.GetCurrentAnimatorStateInfo(2).IsName("EmoteBase"))
        {
            animator.SetBool("ReceivedFirst", false);
            DPadFromOpen(emote);
        }
        else
        {
            animator.SetBool("ReceivedFirst", true);
            DPadFromOpen(emote);
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
        emoteSender.ToDiver(emote);
        animator.SetTrigger("SendToDiver");
        pressOkay = false;
        renderers[emote].material = buttonPressed;

        yield return new WaitForSeconds(3f);

        pressOkay = true;
        renderers[emote].material = buttonReady;
    }
}
