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

    public Image[] emoteButtons;

    // Start is called before the first frame update
    void Awake()
    {
        emoteSender.dPad = this;

        for (int i = 0; i < 4; i++)
        {
            emoteButtons[i].sprite = emoteSender.emoteSprites[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DPadSend(int emote)
    {
        emoteSender.ToDiver(emote);
    }

    public void DPadReceive(int emote)
    {
        StartCoroutine(DPadReceiveSequence(emote));
    }

    IEnumerator DPadReceiveSequence(int emote) //Lays out timing to receive emote
    {
        while(animator.GetCurrentAnimatorStateInfo(0).IsName("IntelEmoteOpening") || animator.GetCurrentAnimatorStateInfo(0).IsName("IntelEmoteClosing") || animator.IsInTransition(0))
        {
            yield return null;
        } //Holds Coroutine if Transitioning or in transition anim
        Debug.Log("Sequence Can Proceed");

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IntelEmoteOpen"))
        {
            DPadFromOpen(emote);
            animator.SetBool("IntelEmoteOverride", false);
        }
        else
        {
            uiEmoteReceive.sprite = emoteSender.emoteSprites[emote];
            animator.SetTrigger("IntelOpen");
        }

        yield return new WaitForSeconds(7f);

        animator.SetTrigger("IntelCloseOrOver");
    }

    void DPadFromOpen(int emote)
    {
        uiEmoteOverride.sprite = uiEmoteReceive.sprite;
        animator.SetBool("IntelEmoteOverride", true);
        animator.SetTrigger("IntelCloseOrOver");
        uiEmoteReceive.sprite = emoteSender.emoteSprites[emote];
    }
}
