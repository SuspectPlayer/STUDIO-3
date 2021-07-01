//Adapation of Script given in Udemy course

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[DefaultExecutionOrder(kControllerManagerUpdateOrder)]
public class CyclingControllerManager : MonoBehaviour
{
    // Slightly after the default, so that any actions such as release or grab can be processed *before* we switch controllers.
    public const int kControllerManagerUpdateOrder = 10;

    InputDevice m_RightController;
    InputDevice m_LeftController;

    [SerializeField]
    [Tooltip("The buttons on the controller that will trigger a transition to the next Controller State.")]
    List<InputHelpers.Button> m_StateCycleButtons = new List<InputHelpers.Button>();
    /// <summary>
    /// The buttons on the controller that will trigger a transition to the next Controller State.
    /// </summary>
    public List<InputHelpers.Button> stateCycleButtons { get { return m_StateCycleButtons; } set { m_StateCycleButtons = value; } }

    /*
    [SerializeField]
    [Tooltip("The buttons on the controller that will force a deactivation of the teleport option.")]
    List<InputHelpers.Button> m_DeactivationButtons = new List<InputHelpers.Button>();
    /// <summary>
    /// The buttons on the controller that will trigger a transition to the Teleport Controller.
    /// </summary>
    public List<InputHelpers.Button> deactivationButtons { get { return m_DeactivationButtons; } set { m_DeactivationButtons = value; } }
    */


    [SerializeField]
    [Tooltip("The Game Object which represents the left hand for normal hand-like purposes.")]
    GameObject m_LeftDirectController;
    /// <summary>
    /// The Game Object which represents the left hand for grab interaction purposes.
    /// </summary>
    public GameObject leftDirectController { get { return m_LeftDirectController; } set { m_LeftDirectController = value; } }

    [SerializeField]
    [Tooltip("The Game Object which represents the left hand for normal laser-pointer purposes.")]
    GameObject m_LeftRayController;
    /// <summary>
    /// The Game Object which represents the left hand for UI interaction purposes.
    /// </summary>
    public GameObject leftRayController { get { return m_LeftRayController; } set { m_LeftRayController = value; } }

    [SerializeField]
    [Tooltip("The Game Object which represents the left hand when teleporting.")]
    GameObject m_LeftTeleportController;
    /// <summary>
    /// The Game Object which represents the left hand when teleporting.
    /// </summary>
    public GameObject leftTeleportController { get { return m_LeftTeleportController; } set { m_LeftTeleportController = value; } }

    [SerializeField]
    [Tooltip("The Game Object which represents the right hand for normal hand-like purposes.")]
    GameObject m_RightDirectController;
    /// <summary>
    /// The Game Object which represents the right hand for grab interaction purposes.
    /// </summary>
    public GameObject rightDirectController { get { return m_RightDirectController; } set { m_RightDirectController = value; } }

    [SerializeField]
    [Tooltip("The Game Object which represents the right hand for normal laser-pointer purposes.")]
    GameObject m_RightRayController;
    /// <summary>
    /// The Game Object which represents the right hand for UI interaction purposes.
    /// </summary>
    public GameObject rightRayController { get { return m_RightRayController; } set { m_RightRayController = value; } }

    [SerializeField]
    [Tooltip("The Game Object which represents the right hand when teleporting.")]
    GameObject m_RightTeleportController;
    /// <summary>
    /// The Game Object which represents the right hand when teleporting.
    /// </summary>
    public GameObject rightTeleportController { get { return m_RightTeleportController; } set { m_RightTeleportController = value; } }

    bool m_LeftCycleDown = false;
    bool m_RightCycleDown = false;

    /// <summary>
    /// A simple state machine which manages the three pieces of content that are used to represent
    /// A controller state within the XR Interaction Toolkit
    /// </summary>
    struct InteractorController
    {
        /// <summary>
        /// The game object that this state controls
        /// </summary>
        public GameObject m_GO;
        /// <summary>
        /// The XR Controller instance that is associated with this state
        /// </summary>
        public XRController m_XRController;
        /// <summary>
        /// The Line renderer that is associated with this state
        /// </summary>
        public XRInteractorLineVisual m_LineRenderer;
        /// <summary>
        /// The interactor instance that is associated with this state
        /// </summary>
        public XRBaseInteractor m_Interactor;

        /// <summary>
        /// When passed a gameObject, this function will scrape the game object for all valid components that we will
        /// interact with by enabling/disabling as the state changes
        /// </summary>
        /// <param name="gameObject">The game object to scrape the various components from</param>
        public void Attach(GameObject gameObject)
        {
            m_GO = gameObject;
            if (m_GO != null)
            {
                m_XRController = m_GO.GetComponent<XRController>();
                m_LineRenderer = m_GO.GetComponent<XRInteractorLineVisual>();
                m_Interactor = m_GO.GetComponent<XRBaseInteractor>();

                Leave();
            }
        }

        /// <summary>
        /// Enter this state, performs a set of changes to the associated components to enable things
        /// </summary>
        public void Enter()
        {
            if (m_LineRenderer)
            {
                m_LineRenderer.enabled = true;
            }
            if (m_XRController)
            {
                m_XRController.enableInputActions = true;
            }
            if (m_Interactor)
            {
                m_Interactor.enabled = true;
            }
        }

        /// <summary>
        /// Leaves this state, performs a set of changes to the associate components to disable things.
        /// </summary>
        public void Leave()
        {
            if (m_LineRenderer)
            {
                m_LineRenderer.enabled = false;
            }
            if (m_XRController)
            {
                m_XRController.enableInputActions = false;
            }
            if (m_Interactor)
            {
                m_Interactor.enabled = false;
            }
        }
    }

    /// <summary>
    /// The states that we are currently modeling. 
    /// If you want to add more states, add them here!
    /// </summary>
    public enum ControllerStates
    {
        /// <summary>
        /// the Grab state is the interaction state for directly grabbing and interacting with objects
        /// </summary>
        Grab = 0,
        /// <summary>
        /// the Ray state is the interaction state for selecting and interacting with UI objects
        /// </summary>
        Ray = 1,
        /// <summary>
        /// the Teleport state is used to interact with teleport interactors and queue teleportations.
        /// </summary>
        Teleport = 2,
        /// <summary>
        /// Maximum sentinel
        /// </summary>
        MAX = 3,
    }

    /// <summary>
    /// Current status of a controller. there will be two instances of this (for left/right). and this allows
    /// the system to change between different states on each controller independently.
    /// </summary>
    struct ControllerState
    {
        ControllerStates m_State;
        InteractorController[] m_Interactors;

        /// <summary>
        /// Sets up the controller
        /// </summary>
        public void Initialize()
        {
            m_State = ControllerStates.MAX;
            m_Interactors = new InteractorController[(int)ControllerStates.MAX];
        }

        /// <summary>
        /// Exits from all states that are in the list, basically a reset.
        /// </summary>
        public void ClearAll()
        {
            if (m_Interactors == null)
                return;

            for (int i = 0; i < (int)ControllerStates.MAX; ++i)
            {
                m_Interactors[i].Leave();
            }
        }

        /// <summary>
        /// Attaches a game object that represents an interactor for a state, to a state.
        /// </summary>
        /// <param name="state">The state that we're attaching the game object to</param>
        /// <param name="parentGameObject">The game object that represents the interactor for that state.</param>
        public void SetGameObject(ControllerStates state, GameObject parentGameObject)
        {
            if ((state == ControllerStates.MAX) || (m_Interactors == null))
                return;

            m_Interactors[(int)state].Attach(parentGameObject);
        }

        /// <summary>
        /// Attempts to set the current state of a controller.
        /// </summary>
        /// <param name="nextState">The state that we wish to transition to</param>
        public void SetState(ControllerStates nextState)
        {
            if (nextState == m_State || nextState == ControllerStates.MAX)
            {
                return;
            }
            else
            {
                if (m_State != ControllerStates.MAX)
                {
                    m_Interactors[(int)m_State].Leave();
                }

                m_State = nextState;
                m_Interactors[(int)m_State].Enter();
            }
        }

        public void CycleState(ControllerStates state)
        {
            int oldState = (int)state;
            if (oldState >= ((int)ControllerStates.MAX) - 1)
            {
                oldState = 0;
            }
            else
            {
                oldState += 1;
            }
            
            ControllerStates newState = (ControllerStates) oldState;

            SetState(newState);
        }

        public ControllerStates GetState()
        {
            return m_State;
        }
    }

    ControllerState m_RightControllerState;
    ControllerState m_LeftControllerState;

    ControllerStates tempLeft;
    ControllerStates tempRight;

    public void UIControllerActivation(bool isLeft)
    {
        tempLeft = m_LeftControllerState.GetState();
        tempRight = m_RightControllerState.GetState();

        if (isLeft)
        {
            m_LeftControllerState.SetState(ControllerStates.Grab);
            m_RightControllerState.SetState(ControllerStates.Ray);
        }
        else if (!isLeft)
        {
            m_RightControllerState.SetState(ControllerStates.Grab);
            m_LeftControllerState.SetState(ControllerStates.Ray);
        }
    }

    public void UIControllerDeactivation()
    {
        m_LeftControllerState.SetState(tempLeft);
        m_RightControllerState.SetState(tempRight);
    }

    void OnEnable()
    {
        m_LeftCycleDown = false;
        m_RightCycleDown = false;

        m_RightControllerState.Initialize();
        m_LeftControllerState.Initialize();

        m_RightControllerState.SetGameObject(ControllerStates.Grab, m_RightDirectController);
        m_RightControllerState.SetGameObject(ControllerStates.Ray, m_RightRayController);
        m_RightControllerState.SetGameObject(ControllerStates.Teleport, m_RightTeleportController);

        m_LeftControllerState.SetGameObject(ControllerStates.Grab, m_LeftDirectController);
        m_LeftControllerState.SetGameObject(ControllerStates.Ray, m_LeftRayController);
        m_LeftControllerState.SetGameObject(ControllerStates.Teleport, m_LeftTeleportController);

        m_LeftControllerState.ClearAll();
        m_RightControllerState.ClearAll();

        InputDevices.deviceConnected += RegisterDevices;
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        for (int i = 0; i < devices.Count; i++)
            RegisterDevices(devices[i]);
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= RegisterDevices;
    }

    void RegisterDevices(InputDevice connectedDevice)
    {
        if (connectedDevice.isValid)
        {
#if UNITY_2019_3_OR_NEWER
            if ((connectedDevice.characteristics & InputDeviceCharacteristics.Left) != 0)
#else
            if (connectedDevice.role == InputDeviceRole.LeftHanded)
#endif
            {
                m_LeftController = connectedDevice;
                m_LeftControllerState.ClearAll();
                m_LeftControllerState.SetState(ControllerStates.Grab);
            }
#if UNITY_2019_3_OR_NEWER
            else if ((connectedDevice.characteristics & InputDeviceCharacteristics.Right) != 0)
#else
            else if (connectedDevice.role == InputDeviceRole.RightHanded)
#endif
            {
                m_RightController = connectedDevice;
                m_RightControllerState.ClearAll();
                m_RightControllerState.SetState(ControllerStates.Grab);
            }
        }
    }

    void Update()
    {
        if (m_LeftController.isValid)
        {
            bool cycling = false;
            for (int i = 0; i < m_StateCycleButtons.Count; i++)
            {
                m_LeftCycleDown = m_LeftController.IsPressed(m_StateCycleButtons[i], out bool value);
                cycling |= value;
            }
            /*
            bool deactivated = false;
            for (int i = 0; i < m_DeactivationButtons.Count; i++)
            {
                m_LeftController.IsPressed(m_DeactivationButtons[i], out bool value);
                m_LeftTeleportDeactivated |= value;
            }

            if (deactivated)
                m_LeftTeleportDeactivated = true;
            */
            // if we're pressing the activation buttons, we transition to Teleport
            if (cycling && !m_LeftCycleDown)
            {
                m_LeftControllerState.CycleState(m_LeftControllerState.GetState());
            }
            /*// otherwise we're in normal state. 
            else
            {
                m_LeftControllerState.SetState(ControllerStates.Select);

                if (!cycling)
                    m_LeftCycleDown = false;
            }*/
        }

        if (m_RightController.isValid)
        {
            bool cycling = false;
            for (int i = 0; i < m_StateCycleButtons.Count; i++)
            {
                m_RightCycleDown = m_RightController.IsPressed(m_StateCycleButtons[i], out bool value);
                cycling |= value;
            }
            /*
            bool deactivated = false;
            for (int i = 0; i < m_DeactivationButtons.Count; i++)
            {
                m_RightController.IsPressed(m_DeactivationButtons[i], out bool value);
                deactivated |= value;
            }

            if (deactivated)
                m_RightTeleportDeactivated = true;
            */
            if (cycling && !m_RightCycleDown)
            {
                m_RightControllerState.CycleState(m_RightControllerState.GetState());
            }
            /*
            else
            {
                m_RightControllerState.SetState(ControllerStates.Select);

                if (!cycling)
                    m_RightCycleDown = false;
            }*/
        }
    }
}
