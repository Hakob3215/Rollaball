using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public GameObject winTextObject;

    // Rigidbody of the player.
    private Rigidbody rb;
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;
    public TextMeshProUGUI countText;
    public float jumpForce = 100;
    public float speedLimit = 50;


    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.


    void OnJump()
    {
        
        if (rb.position.y == 0.5)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    void OnMove(InputValue movementValue)
    {
        
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }


    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        //caps velocity with magnitude
        
        rb.AddForce(movement * speed);
        
        if(rb.velocity.x >= speedLimit)
        {
            rb.velocity = new Vector3(speedLimit,rb.velocity.y,rb.velocity.z);
        }
        else if(rb.velocity.x <= -speedLimit)
        {
            rb.velocity = new Vector3(-speedLimit,rb.velocity.y,rb.velocity.z);
        }

        if (rb.velocity.z >= speedLimit)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedLimit);
        }
        else if (rb.velocity.z <= -speedLimit)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speedLimit);
        }

        // Apply force to the Rigidbody to move the player.

        if (rb.position.y < -5)
        {
            rb.position = new Vector3(0, 0.5f, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

}