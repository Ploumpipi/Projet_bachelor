using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpspeed = 8f;
    public float gravity = 20f;
    public bool isGrounded = false;

    private Vector3 moveDirection = Vector3.zero;
    CharacterController Cc;
    Rigidbody rb;

    void Start()
    {
        Cc = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Cc.isGrounded)
        {
            isGrounded = true;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            //Debug.Log(Input.GetAxis("Horizontal"));
            //Debug.Log(Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //transform.Translate(moveDirection);
            //Debug.Log("Test");
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpspeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * speed * 100);
        Cc.Move(moveDirection * Time.deltaTime);
    }
}
