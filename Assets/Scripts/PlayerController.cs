using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    private float BaseSpeed = 3;
    [SerializeField] private float CameraSpeed;
    [SerializeField] private Transform Camera;
    private Animator anim;
    private float speed = 3;


    public bool InControl;
    Rigidbody RB;
    PlayerActions playerActions;

    void Start()
    {
        playerActions = PlayerActions.CreateWithAllBindings();
        RB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        movementState = MovementState.Still;
        anim.SetBool("Moving", false);
        anim.SetBool("Running", false);
        anim.SetBool("Sprinting", false);
    }


    void Update()
    {
        if (InControl)
        {
            Move(playerActions.MovePlayer.Value);
            RotateCamera(playerActions.RotateCamera);
        }
    }


    public bool Moving;
    public bool Running;
    public bool Sprinting;


    //USE AN ENUM NEXT TIME YOU OPEN THIS DOCUMENT
    MovementState movementState;
    public enum MovementState
    {
        Still,
        Walking,
        Running
    }




    public void Move(Vector2 axis)
    {
        float x = axis.x;
        float y = axis.y;


        float xPos = System.Math.Abs(x);
        float yPos = System.Math.Abs(y);
        if (xPos == 0 && yPos == 0)
        {// not walking at all
            if (movementState != MovementState.Still)
            {
                movementState = MovementState.Still;
                anim.SetBool("Moving", false);
                anim.SetBool("Running", false);
                anim.SetBool("Sprinting", false);
            }
        }
        else if (!playerActions.Run || playerActions.LDown)
        {//walking
            if (movementState != MovementState.Walking)
            {
                movementState = MovementState.Walking;
                anim.SetBool("Moving", true);
                anim.SetBool("Running", false);
                anim.SetBool("Sprinting", false);
            }
        }
        else if (playerActions.Run)
        {//running
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
    private float SpeedPenalty;
    private float SpeedBonus = 1;

    public float GetSpeed(bool Forward = true)
    {

        switch (movementState)
        {
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

