using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Christian Pedersen
//Run and Jump Assignment 2
public class PlayerController : MonoBehaviour
{
    private float speed = 20.0f;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;

    public float jumpSpeed = 0.05f;
    public float speedMultiplier = 2.0f;

    private Vector3 startPosition;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody>();
        Debug.Log("Hello World");
    }

    void HandleMovement()
    {
        // Store the current speed in a variable
        float currentSpeed = speed;

        // Check if Left Shift key is pressed then speed up car
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= speedMultiplier;
            Debug.Log("Maybe we should make some numbers bigger now");
        }

        // Move the vehicle forward
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * forwardInput);

        horizontalInput *= Mathf.Sign(forwardInput);
        if (forwardInput == 0)
        {
            horizontalInput = 0;
        }
        // Rotate the car based on horizontal input
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);

        //Check if space is clicked, then jump
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * jumpSpeed);
        }
    }

    void HandleResetPosition()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ResetPosition(startPosition);
            }
            else
            {
                ResetPosition();
            }
        }
    }


    void ResetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        transform.rotation = Quaternion.identity;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

    }

    void ResetPosition()
    {
        Vector3 newPos = transform.position;
        newPos.y = 10;
        ResetPosition(newPos);
    }

    // Update is called once per frame
    void Update()
    {
        HandleResetPosition();
        HandleMovement();

    }

}


