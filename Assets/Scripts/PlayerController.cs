using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    private float BaseSpeed = 3;
    [SerializeField] private float CameraSpeed;
    [SerializeField] private Transform Camera;
    private float speed = 3;


    public bool InControl;
    Rigidbody RB;
    PlayerActions playerActions;
    // Start is called before the first frame update
    void Start()
    {
        playerActions = PlayerActions.CreateWithAllBindings();
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

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
        Running,
        Sprinting
    }

    public void Move(Vector2 axis)
    {
        
        float x = axis.x;
        float y = axis.y;


        float xPos = System.Math.Abs(x);
        float yPos = System.Math.Abs(y);
        if (xPos == 0 && yPos == 0)
        {// not walking at all
            if(movementState != MovementState.Still)
            {
                movementState = MovementState.Still;
            }
        }
        else if (xPos < 0.6 && yPos < 0.6 )
        {//walking
            if (movementState != MovementState.Walking)
            {
                movementState = MovementState.Walking;

            }
        }
        else
        {//running, in here should be a check if sprint is heldif(movementState == MovementState.Still)
            if (movementState != MovementState.Running)
            {
                movementState = MovementState.Running;

            }
            
        }
        Debug.Log("x " + x + " Y " + y);
        Debug.Log(movementState);

        transform.Translate(Vector3.forward * axis.y * GetSpeed() * Time.deltaTime);
        transform.Translate(Vector3.right * axis.x * speed * Time.deltaTime);
    }
    public float SpeedReduction;
    public float SpeedBonus;
    public IEnumerator SpeedPenalty()
    {
        float test;

        yield return new WaitForSeconds(2);
    }

    public float GetSpeed()
    {

        switch (movementState)
        {
            case (MovementState.Still):
                SpeedBonus = 0;
                break;        
        
            case (MovementState.Walking):
                SpeedBonus = 3;
                break;        
        
            case (MovementState.Running):
                SpeedBonus = 4;
                break;        
        
            case (MovementState.Sprinting):
                SpeedBonus = 5;
                break;
            
        }
        float spd = BaseSpeed;

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

