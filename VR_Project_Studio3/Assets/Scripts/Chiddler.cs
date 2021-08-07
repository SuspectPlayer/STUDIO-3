using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chiddler : MonoBehaviour
{
    int originalIndex;

    public void OrganiseChildIndex(int index)
    {
        originalIndex = gameObject.transform.GetSiblingIndex();
        gameObject.transform.SetSiblingIndex(index);
    }

    public void ResetChildIndex()
    {
        gameObject.transform.SetSiblingIndex(originalIndex);
    }
}
