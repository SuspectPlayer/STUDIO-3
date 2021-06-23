using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviours : MonoBehaviour
{
    CyclingControllerManager controllerManager;
    //UI (De)Activation Variables
    public GameObject wristUIPrefab;

    GameObject leftUIInstance;
    GameObject rightUIInstance;

    public Transform leftWrist;
    public Transform rightWrist;

    [HideInInspector] public bool leftUIActive;
    [HideInInspector] public bool rightUIActive;

    //EmoteStates
    bool m_EmoteHappy;
    bool m_EmoteSad;
    bool m_EmoteQuery;
    bool m_EmoteExclaim;

    public bool emoteHappy { get { return m_EmoteHappy; } set { m_EmoteHappy = value; } }
    public bool emoteSad { get { return m_EmoteSad; } set { m_EmoteSad = value; } }
    public bool emoteQuery { get { return m_EmoteQuery; } set { m_EmoteQuery = value; } }
    public bool emoteExclaim { get { return m_EmoteExclaim; } set { m_EmoteExclaim = value; } }

    bool m_EmoteLeft;
    bool m_EmoteRight;
    bool m_EmoteUp;
    bool m_EmoteDown;

    public bool emoteLeft { get { return m_EmoteLeft; } set { m_EmoteLeft = value; } }
    public bool emoteRight { get { return m_EmoteRight; } set { m_EmoteRight = value; } }
    public bool emoteUp { get { return m_EmoteUp; } set { m_EmoteUp = value; } }
    public bool emoteDown { get { return m_EmoteDown; } set { m_EmoteDown = value; } }

    void Awake()
    {
        controllerManager = GetComponent<CyclingControllerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftUIActivate()
    {
        if (rightUIActive || leftUIActive)
        {
            Debug.LogWarning("Wrist UI is already active!");
            return;
        }
        else
        {
            leftUIInstance = Instantiate(wristUIPrefab, leftWrist);
            controllerManager.UIControllerActivation(true);
            leftUIActive = true;
        }
    }

    public void RightUIActivate()
    {
        if (leftUIActive || rightUIActive)
        {
            Debug.LogWarning("Wrist UI is already active!");
            return;
        }
        else
        {
            rightUIInstance = Instantiate(wristUIPrefab, rightWrist);
            controllerManager.UIControllerActivation(false);
            rightUIActive = true;
        }
    }

    public void UIDeactivate()
    {
        if (!leftUIActive && !rightUIActive)
        {
            Debug.LogWarning("Wrist UI is not active!");
            return;
        }
        else if (leftUIActive)
        {
            Destroy(leftUIInstance);
            controllerManager.UIControllerDeactivation();
            leftUIActive = false;
        }
        else if (rightUIActive)
        {
            Destroy(rightUIInstance);
            controllerManager.UIControllerDeactivation();
            rightUIActive = false;
        }
    }
}
