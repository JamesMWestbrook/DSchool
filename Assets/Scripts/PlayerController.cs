using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float BaseSpeed;
    [SerializeField] private float CameraSpeed;
    [SerializeField] private Transform Camera;
    private float speed = 5;


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


    public void Move(Vector2 axis)
    {
        transform.Translate(Vector3.forward * axis.y * speed * Time.deltaTime);
        transform.Translate(Vector3.right  * axis.x * speed * Time.deltaTime);
    }

    float rotX, rotY;
    private float rotation = 0, minX = -75, maxX = 75;
    public void RotateCamera(Vector2 axis)
    {
        rotX = axis.x * -1 * CameraSpeed;
        rotY += axis.y *  CameraSpeed;
        transform.Rotate(0.0f, axis.x * CameraSpeed * Time.deltaTime, 0.0f);

        rotation -= axis.y * Time.deltaTime * CameraSpeed;
        rotation = Mathf.Clamp(rotation, minX, maxX);
        Vector3 localRotation = Camera.localRotation.eulerAngles;
        Camera.localRotation = Quaternion.Euler(rotation, localRotation.y, localRotation.z);
    }

}
