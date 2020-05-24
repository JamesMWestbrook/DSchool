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

    private void SetMovement(int i)
    {
        switch (i)
        {
            case (0):
                Moving = false;
                Running = false;
                Sprinting = false;
                break;
            case (1):
                Moving = true;
                Running = false;
                Sprinting = false;
                break;
            case (2):
                Moving = false;
                Running = true;
                Sprinting = false;
                break;
            case (3):
                Moving = false;
                Running = false;
                Sprinting = true;
                break;
        }

    }
    public void Move(Vector2 axis)
    {
        float x = axis.x;
        float y = axis.y;

        if (x == 0 && y == 0)
        {// not walking at all
            SetMovement(0);
        }
        else if (x < 0.5 || y < 0.5)
        {//walking
            SetMovement(1);
        }
        else
        {//running, in here should be a check if sprint is held
            SetMovement(2);
        }


        transform.Translate(Vector3.forward * axis.y * GetSpeed() * Time.deltaTime);
        transform.Translate(Vector3.right * axis.x * speed * Time.deltaTime);
    }

    public float GetSpeed()
    {
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
