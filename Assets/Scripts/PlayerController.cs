using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{

    //controller related
    private float BaseSpeed = 3;
    [SerializeField] private float CameraSpeed;
    [SerializeField] private Transform Camera;
    private Animator anim;
    private float speed = 3;


    public bool InControl;
    Rigidbody RB;
    public PlayerActions playerActions;

    void Awake()
    {
        playerActions = PlayerActions.CreateWithAllBindings();
        RB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        movementState = MovementState.Still;
        anim.SetBool("Moving", false);
        anim.SetBool("Running", false);
        anim.SetBool("Sprinting", false);

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (!InControl) return;

        if (playerActions.Inventory.WasPressed)
        {
            InControl = false;
            anim.SetBool("Moving", false);
            anim.SetBool("Backwards", false);
            anim.SetBool("Running", false);

            Inventory.instance.OpenInventory();
        }
        if (playerActions.Run.WasPressed)
        {

            if (Running && movementState == MovementState.Running) //toggleabilty
            {
                Running = false;
            }
            else
            {

                Running = true;
            }
        }
        CrouchMovement();
        Move(playerActions.MovePlayer.Value);
        RotateCamera(playerActions.RotateCamera);
    }


    public bool Moving;
    [HideInInspector] public bool Running;


    //USE AN ENUM NEXT TIME YOU OPEN THIS DOCUMENT
    MovementState movementState;
    public enum MovementState
    {
        Crouched,
        Still,
        Walking,
        Running
    }

    public bool Crouched;
    private void CrouchMovement()
    {
        if (playerActions.Crouch.WasPressed && movementState != MovementState.Running)
        {
            if (Crouched)
            {
                Crouched = false;
                anim.SetBool("Crouching", false);
            }
            else
            {
                Crouched = true;
                anim.SetBool("Crouching", true);
            }
        }
    }

    public void Move(Vector2 axis)
    {
        float x = axis.x;
        float y = axis.y;


        float xPos = System.Math.Abs(x);
        float yPos = System.Math.Abs(y);



        //Idle
        if (xPos == 0 && yPos == 0)
        {
            Running = false;
            if (movementState != MovementState.Still)
            {
                movementState = MovementState.Still;
                anim.SetBool("Moving", false);
                anim.SetBool("Backwards", false);
                anim.SetBool("Running", false);
                anim.SetBool("Sprinting", false);
            }
        }

        //Walking
        else if (!playerActions.Run && !Running || playerActions.LDown || playerActions.Crouch)
        {
            Running = false;
            if (movementState != MovementState.Walking)
            {
                movementState = MovementState.Walking;
                anim.SetBool("Moving", true);
                anim.SetBool("Running", false);
                anim.SetBool("Sprinting", false);
            }
            if (playerActions.LDown)
            {
                anim.SetBool("Backwards", true);
            }
            else anim.SetBool("Backwards", false);
            if (Crouched)
            {
                movementState = MovementState.Crouched;
            }
        }

        //Running
        else if (playerActions.Run.WasPressed || Running)
        {

            if (movementState != MovementState.Running)
            {
                movementState = MovementState.Running;
                anim.SetBool("Moving", true);
                anim.SetBool("Running", true);
                anim.SetBool("Sprinting", false);
            }
        }
        transform.Translate(Vector3.forward * axis.y * GetSpeed() * Time.deltaTime);
        transform.Translate(Vector3.right * axis.x * GetSpeed(false) * Time.deltaTime);
    }
    public void StopMovement()
    {
        anim.SetBool("Moving", false);
        anim.SetBool("Backwards", false);
        anim.SetBool("Running", false);
    }
    private float SpeedPenalty;
    private float SpeedBonus = 1;

    public float GetSpeed(bool Forward = true)
    {

        switch (movementState)
        {
            case (MovementState.Crouched):
                BaseSpeed = 2;
                break;

            case (MovementState.Still):
                BaseSpeed = 0;
                break;

            case (MovementState.Walking):
                BaseSpeed = 3;
                break;

            case (MovementState.Running):
                BaseSpeed = 7;
                break;
        }

        float spd = BaseSpeed * SpeedBonus;
        if (movementState == MovementState.Running && !Forward || movementState == MovementState.Running && playerActions.LDown)
        {
            spd -= 4;
        }
        return spd;

    }


    float rotX, rotY;
    private float rotation = 0, minX = -75, maxX = 75;
    public void RotateCamera(Vector2 axis)
    {
        rotX = axis.x * -1 * CameraSpeed;
        rotY += axis.y * CameraSpeed;
        transform.Rotate(0.0f, axis.x * CameraSpeed * Time.deltaTime, 0.0f);

        rotation -= axis.y * Time.deltaTime * CameraSpeed;
        rotation = Mathf.Clamp(rotation, minX, maxX);
        Vector3 localRotation = Camera.localRotation.eulerAngles;
        Camera.localRotation = Quaternion.Euler(rotation, localRotation.y, localRotation.z);
    }

}

