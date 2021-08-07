using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FPSUIOpener : MonoBehaviour
{
    public GameObject wristUIObject;
    public UIBehaviours uiBehave;
    PhotonView photonView;

    private GameSetup gameSetup;
    [SerializeField] private Animator openAnimator;
    public MouseLook looker;

    float moussel;

    bool isTrue = false;
    bool queueWaiting = false;

    private void Awake()
    {
        gameSetup = GameObject.Find("GameSetup").GetComponent<GameSetup>();

        photonView = GetComponentInParent<PhotonView>();

        openAnimator.SetBool("IsVRPlayer", !gameSetup.isFlatScreen);
        openAnimator.SetTrigger("Branch");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!queueWaiting)
            {
                StartCoroutine(QueueAnimator());
            }
        }
    }

    public void WristlessBoi() // To Call From Animator; Checks if UI should be open
    {
        if (!isTrue)
        {
            uiBehave.Close();
        }
        else
        {
            uiBehave.OpenToCorrect();
        }
    }

    void EmoteUIActive()
    {
        if (isTrue)
        {
            moussel = looker.mouseSensitivity;
            openAnimator.SetTrigger("PlayerOpen");
            
            if (photonView.IsMine)
            {
                looker.mouseSensitivity = 0f;
                Cursor.lockState = CursorLockMode.Confined;
            }
            
        }
        else 
        { 
            openAnimator.SetTrigger("PlayerClose");
            if (photonView.IsMine)
            {
                looker.mouseSensitivity = moussel;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    bool IsAnimatorMoveState()
    {
        bool isMoveState;
        AnimatorStateInfo stateInfo = openAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("VRUIOpening") || stateInfo.IsName("VRUIClosing") || stateInfo.IsName("FPSUIOpening") || stateInfo.IsName("FPSUIClosing"))
            isMoveState = true;
        else isMoveState = false;


        if (openAnimator.IsInTransition(0) || isMoveState)
            return true;
        else
            return false;
    }

    IEnumerator QueueAnimator() // Waits for animations to get to a rest state
    {
        Debug.Log("Queued");
        queueWaiting = true;
        while (IsAnimatorMoveState())
        {
            yield return null;
        }

        isTrue = !isTrue;
        EmoteUIActive();
        queueWaiting = false;
    }
}
