using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SebastianController : MonoBehaviour
{
    public float movespeed = 6;
    Rigidbody rb;
    Camera viewCam;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        viewCam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = viewCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCam.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * movespeed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
