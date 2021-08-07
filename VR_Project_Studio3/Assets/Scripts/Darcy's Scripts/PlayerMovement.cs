using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    PhotonView photonView;

    [SerializeField]
    GameObject intelCamSwap;
    
    //the controller
    public CharacterController controller;

    //Transforms
    public Transform player;
    public Transform pov;

    //ground check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //moving speeds
    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //moving variables
    public Vector3 move;
    Vector3 velocity;

    //landing variables
    public AudioClip landingSound;

    //footstep variables
    public Vector3 originalCamPos;
    public float timer;
    public float stepTimer;
    public AudioClip[] footstepSounds;

    //booleans
    public bool firstStep = true;
    bool isGrounded;
    public bool sprinting;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        originalCamPos = pov.localPosition;
    }

    void Update()
    {
        if (photonView.IsMine && !intelCamSwap.GetComponent<IntelCameraSwap>().zoomedIn)
        {

            #region Grounding
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //checking to see if the player is on the ground

            if (isGrounded && velocity.y < 0) //stops gravityDownForce from applying when on the ground, makes it so the character doesn't teleport to the ground when                                        
            {                                 //the gravityDownForce starts applying again.
                velocity.y = -2f;
            }
            #endregion

            #region Moving
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            move = transform.right * x + transform.forward * z;

            if (player.localScale.y <= 0.5f) //checking to see if the character is crouching or not
            {
                speed = 6f;
            }
            if (player.localScale.y >= 1.0f && !sprinting) //if the player is crouching, speed is halved
            {
                speed = 12f;
            }
            if (sprinting) //sprinting increases speed by 50%
            {
                speed = 18f;
            }

            controller.Move(move * speed * Time.deltaTime);

            #endregion

            #region Jumping
            if (Input.GetButtonDown("Jump") && isGrounded) //jumping
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }



            #endregion

            #region Gravity
            velocity.y += gravity * Time.deltaTime; //gravityDownForce
                                                    //velocity equation is (v = g * t^2). this means that time.deltatime has to be multiplied twice.
            controller.Move(velocity * Time.deltaTime);
            #endregion

            #region Crouching
            //if (Input.GetKey(KeyCode.LeftControl))
            //{
            //    Vector3 newScale = new Vector3(1, 0.5f, 1);
            //    player.localScale = newScale;
            //}

            //if (!Input.GetKey(KeyCode.LeftControl))
            //{
            //    Vector3 newScale = new Vector3(1, 1f, 1);
            //    player.localScale = newScale;
            //}
            #endregion

            #region Sprinting

            if (Input.GetKey(KeyCode.LeftShift) && player.localScale.y >= 1.0f) //sprinting cant happen if the player is crouching
            {
                sprinting = true;
            }

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                sprinting = false;
            }

            #endregion

            #region Footsteps
            if (pov.localPosition.y != originalCamPos.y)
            {
                timer += Time.deltaTime;
                if (timer >= 0.5f && !firstStep)
                {
                    //Footsteps();
                    timer = 0f;
                    stepTimer = 0f;
                    pov.localPosition = originalCamPos;
                }
                if (timer >= 0.1f && firstStep)
                {
                    //Footsteps();
                    firstStep = false;
                    timer = 0f;
                    stepTimer = 0f;
                    pov.localPosition = originalCamPos;
                }
            }
            if (pov.localPosition.y == originalCamPos.y)
            {
                stepTimer += Time.deltaTime;
                if (stepTimer >= 2f)
                {
                    firstStep = true;
                }
            }
            #endregion
        }

    }

    //public void Footsteps()
    //{
    //    if(!controller.isGrounded)
    //    {
    //        return;
    //    }
    //    //pick & play a random footstep sound from the array
    //    int n = Random.Range(0, footstepSounds.Length);
    //}

    //public void Landing()
    //{
    //    if(!controller.isGrounded)
    //    {
    //        return;
    //    }
    //}
}
