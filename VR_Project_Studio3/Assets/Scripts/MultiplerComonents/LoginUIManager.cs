using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{

    public GameObject connectOptionsPannel;
    public GameObject connectWithNamePannel;
    void Start()
    {
        connectOptionsPannel.SetActive(true);
        connectWithNamePannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
