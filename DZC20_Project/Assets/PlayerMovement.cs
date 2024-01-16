using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    float horizontalMove = 0f;
    float verticalMove = 0f; // Variable for vertical movement

    public float runSpeed = 40f;

    private BoxCollider2D boundaryCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Boundary BoxCollider2D in the scene
        // If the boundary collider is attached to another object, you will need to find that object specifically
        // For example, you could use GameObject.FindWithTag if you've tagged your boundary object
        boundaryCollider = GameObject.FindWithTag("Boundary").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal and vertical input
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
    }

    private void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, false, false);

        // Clamp the player's position within the Box Collider's bounds
        // This assumes that the boundary collider is axis-aligned and not rotated
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundaryCollider.bounds.min.x, boundaryCollider.bounds.max.x),
            Mathf.Clamp(transform.position.y, boundaryCollider.bounds.min.y, boundaryCollider.bounds.max.y),
            transform.position.z
        );
    }
}
