using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float acceleration;
    public float maxSpeed;
    private Rigidbody rigidBody;
    private KeyCode[] inputKeys;
    private Vector3[] directionsForKeys;

    // currently, Start() is checking input keys and acknowledging the rigidbody of "player".
    void Start() {
        inputKeys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
        directionsForKeys = new Vector3[] { Vector3.forward, Vector3.left, Vector3.back, Vector3.right };
        rigidBody = GetComponent<Rigidbody>();
    }

    // FixedUpdate is for a non-basic physics allocation
    // loop checks continuously for input keys
    void FixedUpdate () {
        for (int i = 0; i < inputKeys.Length; i++){
            var key = inputKeys[i];

    // if they were pressed,
            if(Input.GetKey(key)) {
        // gets the direction, multiply by acceleration and number of seconds, and makes 'player' move.
                Vector3 movement = directionsForKeys[i] * acceleration * Time.deltaTime;
                movePlayer(movement);
            }
        }
    }
    /* movePlayer() processes speed of "player" and impedes it from surpassing a maximum speed.
    this is fundamental to keep physics straight, or else game becomes unbalanced as the player might never be hit.
    */
    void movePlayer(Vector3 movement) {
        if(rigidBody.velocity.magnitude * acceleration > maxSpeed) {
            rigidBody.AddForce(movement * -1);
        } else {
            rigidBody.AddForce(movement);
        }
    }
}
