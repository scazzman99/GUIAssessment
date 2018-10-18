using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reusing this script so that you can walk around in the space to inspect time etc.
namespace GUIAssignment {
    [RequireComponent(typeof(CharacterController))]
    public class CharControlWithCam : MonoBehaviour {

        #region Vars
        [Header("MainMovementVariables")]
        [Space(10)]
        public float speed = 5f;
        public float crouchSpeed = 2f;
        public float sprintSpeed = 8f;
        public float grav = 20f;
        public float jumpSpeed = 8f;
        public bool isSprinting;
        public CharacterController playerController;
        private Vector3 moveVector = Vector3.zero;
        [Header("CameraVariables")]
        [Space(10)]
        public Camera mainCam;
        public RotationAxis rotAxis;
        //Sensitivity vars will control the speed at which cam/player rotation will happen
        public float sensX = 10f;
        public float sensY = 10f;
        //Min and max Y will be used to clamp the angle at which the camera can tilt vertically
        public float minY = -60f;
        public float maxY = 60f;
        //Empty variable used to store input for Y movement of camera
        public float rotY;

        //Keycodes that will store inputs for certain actions
        [Header("Keybinds")]
        public KeyCode forward;
        public KeyCode backward, left, right, jump, crouch, sprint, interact;
        #endregion

        #region Start&Update
        // Use this for initialization
        void Start() {
            //connect the character controller
            playerController = GetComponent<CharacterController>();
            //get the camera and connect it to the script
            mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
            //set up the keybinds to be used
            SetUpKeys();

        }

        // Update is called once per frame
        void Update() {
            PlayerMovement();
            CameraMovement();
        }

        #endregion

        #region Player&CameraMovement

        void PlayerMovement()
        {
            if (playerController.isGrounded)
            {

                //set empty vector for movement
                moveVector = new Vector3(0f, 0f, 0f);

                //get player input
                GetMovementInput();

                //actually use this vector to transform coordinates of player
                moveVector = transform.TransformDirection(moveVector);

                //if crouch key is pressed and/or held
                if (Input.GetKey(crouch))
                {
                    //Move vector has magnitude crouch speed
                    moveVector *= crouchSpeed;
                }

                //else if sprint key is pressed and/or held
                else if (Input.GetKey(sprint))
                {
                    //Move vector has magnitude sprint speed
                    moveVector *= sprintSpeed;
                }
                else
                {
                    //increase the rate at which the player travels
                    moveVector *= speed;
                }

                //if the jump key is pressed
                if (Input.GetKeyDown(jump))
                {
                    //if jump button is pressed, add y component to moveVector to make the player jump. APPLY CONST GRAVITY
                    moveVector.y = jumpSpeed;
                }
            }

            //apply gravity every frame
            moveVector.y -= grav * Time.deltaTime;

            //ensures that if game pauses the player will stop
            playerController.Move(moveVector * Time.deltaTime);

        }

        void CameraMovement()
        {
            switch (rotAxis)
            {
                case RotationAxis.MouseXandY:
                    //TIME.DELTATIME IS A FLOAT AND WILL SEVERLY HURT ROTATION, THERFORE USE TIMESCALE WHICH IS EITHER 1 OR 0
                    //Y rotation is just y axis of mouse multiply sens in Y direction
                    rotY += Input.GetAxis("Mouse Y") * sensY * Time.timeScale;

                    //takes the Y rotation and ensures that it cannot exceed our max and min y rotations, simulating a neck.
                    rotY = Mathf.Clamp(rotY, minY, maxY);

                    //rotate the camera around the X axis using clamped values to give vertical rotation
                    mainCam.transform.localEulerAngles = new Vector3(-rotY, 0f, 0f);

                    //rotate the player around the Y axis according to their input from X axis and sensX
                    playerController.transform.Rotate(0f, Input.GetAxis("Mouse X") * sensX * Time.timeScale, 0f);




                    break;

                case RotationAxis.MouseX:
                    //rotate the player around the Y axis when turning left or right with Cam
                    playerController.transform.Rotate(0f, Input.GetAxis("Mouse X") * sensX, 0f);
                    break;
                case RotationAxis.MouseY:
                    //get rotation input and * sens to give base Y rotation
                    rotY += Input.GetAxis("Mouse Y") * sensY;
                    //same as in MouseXandY
                    rotY = Mathf.Clamp(rotY, minY, maxY);

                    //adjust angle of camera based on -rotY value, which will turn camera around the x axis.
                    mainCam.transform.localEulerAngles = new Vector3(-rotY, 0f, 0f);
                    break;
            }
        }

        #endregion

        //Set up the keybinds for the controls
        private void SetUpKeys()
        {
            //parse the result of getstring to a keycode. If no keycode is found at string key 'Forward; then set to 'W'.
            forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
            backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
            left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
            right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
            jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
            crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
            sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
            interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "F"));

        }

        //Get movement direction from keybinds
        private void GetMovementInput()
        {
            //If forward key is pressed
            if (Input.GetKey(forward))
            {
                //+1 to z axis
                moveVector.z += 1f;
            }
            //If backward key is pressed
            if (Input.GetKey(backward))
            {
                //-1 to z axis
                moveVector.z -= 1f;
            }
            //If left key is pressed
            if (Input.GetKey(left))
            {
                //+1 to x axis
                moveVector.x -= 1f;
            }
            //If right key is pressed
            if (Input.GetKey(right))
            {
                //-1 to x axis
                moveVector.x += 1f;
            }
        }

    }

    
}

#region RotationAxisEnum
public enum RotationAxis
{
    MouseXandY,
    MouseX,
    MouseY
}
#endregion