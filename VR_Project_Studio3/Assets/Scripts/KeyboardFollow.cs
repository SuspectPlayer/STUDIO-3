using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardFollow : MonoBehaviour
{
    public Transform playerHips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerHips.position;
        gameObject.transform.rotation = playerHips.rotation;
    }
}
