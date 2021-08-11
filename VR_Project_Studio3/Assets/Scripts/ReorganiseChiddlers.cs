using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorganiseChiddlers : MonoBehaviour
{
    public Chiddler[] chiddlers;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChiddlerOrganise(int index)
    {
        foreach (Chiddler chiddler in chiddlers)
        {
            if (chiddler == chiddlers[index])
            {
                chiddler.OrganiseChildIndex(0);
            }
        }

        StartCoroutine(ChiddlerIndexReset());
    }

    IEnumerator ChiddlerIndexReset()
    {
        yield return new WaitForSeconds(1f);
        foreach (Chiddler chiddler in chiddlers)
        {
            chiddler.ResetChildIndex();
        }
    }

    public void Animu()
    {
        animator.SetBool("DoTheSend", false);
        animator.SetBool("DoTheGet", false);
    }
}
