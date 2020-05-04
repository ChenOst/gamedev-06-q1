using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component moves its object when the player clicks the arrow keys.
 */
public class KeyboardMover: MonoBehaviour {
    [Tooltip("Speed of movement, in meters per second")]
    [SerializeField] float speed = 1f;

    void Update() {
        float horizontal = Input.GetAxis("Horizontal"); // +1 if right arrow is pushed, -1 if left arrow is pushed, 0 otherwise
        float vertical = Input.GetAxis("Vertical");     // +1 if up arrow is pushed, -1 if down arrow is pushed, 0 otherwise
        Vector3 movementVector = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        transform.position += movementVector;

        // Flat World - Invisible Borders

        if (transform.position.y >= 3.0f)
        {
            transform.position = new Vector3(transform.position.x, 3.0f, 0);
        }
        else if (transform.position.y <= -3.0f)
        {
            transform.position = new Vector3(transform.position.x, -3.0f, 0);
        }

        // A round world - the player comes to one side of the world and appears on the other side

        if (transform.position.x >= 10.0f)
        {
            transform.position = new Vector3(-10.0f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.0f)
        {
            transform.position = new Vector3(10.0f, transform.position.y, 0);
        }
    }
}
