using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorganiseChiddlers : MonoBehaviour
{
    public Chiddler[] chiddlers;

    public void ChiddlerOrganise(int child, int index)
    {
        int tick = 0;
        foreach (Chiddler chiddler in chiddlers)
        {
            if (chiddler == chiddlers[child])
            {
                chiddler.OrganiseChildIndex(index);
            }
            else
            {
                chiddler.OrganiseChildIndex(tick);
                tick++;
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
}
