using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniBoolSet : MonoBehaviour
{
    Animator ani;
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    public void AniSetBool(bool booly)
    {
        ani.SetBool("IsOpen", booly);
    }
}
