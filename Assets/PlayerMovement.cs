using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody rb;
    public Transform tr;
    private float isGrounded = 0;

    RaycastHit hit1;
    RaycastHit hit2;
    RaycastHit hit3;
    RaycastHit hit4;

    void Start () {
        // resets the position and rotation of the player
        tr.eulerAngles = new Vector3(0, 90, 0);
        tr.position = new Vector3(-5, 11, 7);
    }

    void FixedUpdate () {
        // MOVEMENT
        // this turns the player in the direction the mouse moves
        tr.eulerAngles += new Vector3(0, Mathf.Clamp(Input.GetAxis("Mouse X") * Time.deltaTime * 500, -300, 300), 0);
        // these if statements move the player in the direction of the WASD key they pressed
        if (Input.GetKey("w")) {
            rb.AddForce(11000 * Mathf.Sin(Mathf.Deg2Rad * tr.eulerAngles.y) * Time.deltaTime, 0, 11000 * Mathf.Cos(Mathf.Deg2Rad * tr.eulerAngles.y) * Time.deltaTime);
        }
        if (Input.GetKey("d")) {
            rb.AddForce(11000 * Mathf.Cos(Mathf.Deg2Rad * tr.eulerAngles.y) * Time.deltaTime, 0, -11000 * Mathf.Sin(Mathf.Deg2Rad * tr.eulerAngles.y) * Time.deltaTime);
        }
        if (Input.GetKey("a")) {
            rb.AddForce(-11000 * Mathf.Cos(Mathf.Deg2Rad * tr.eulerAngles.y) * Time.deltaTime, 0, 11000 * Mathf.Sin(Mathf.Deg2Rad * tr.eulerAngles.y) * Time.deltaTime);
        }
        if (Input.GetKey("s")) {
            rb.AddForce(-11000 * Mathf.Sin(Mathf.Deg2Rad * tr.eulerAngles.y) * Time.deltaTime, 0, -11000 * Mathf.Cos(Mathf.Deg2Rad * tr.eulerAngles.y) * Time.deltaTime);
        }
        // this if statement clamps the player's speed to just 12 units, no matter which direction they are facing
        if (Mathf.Sqrt((rb.velocity.z * rb.velocity.z + rb.velocity.x * rb.velocity.x)) * Time.deltaTime > 12 * Time.deltaTime) {
            rb.velocity = new Vector3(rb.velocity.x * 12 / Mathf.Sqrt(rb.velocity.z * rb.velocity.z + rb.velocity.x * rb.velocity.x), rb.velocity.y, rb.velocity.z * 12 / Mathf.Sqrt(rb.velocity.z * rb.velocity.z + rb.velocity.x * rb.velocity.x));
        }

        // JUMPING
        // four raycasts are sent, one for each corner of the cube. If at least one corner is right on top of ground, then the player "isGrounded:"
        if ((Physics.Raycast(tr.position + new Vector3(0.5f * Mathf.Cos(Mathf.Deg2Rad * tr.eulerAngles.y), 0, 0.5f * Mathf.Sin(Mathf.Deg2Rad * tr.eulerAngles.y)), new Vector3(0, -1, 0), out hit1, .51f) && hit1.transform.tag == "Ground") || (Physics.Raycast(tr.position + new Vector3(-0.5f * Mathf.Cos(Mathf.Deg2Rad * tr.eulerAngles.y), 0, 0.5f * Mathf.Sin(Mathf.Deg2Rad * tr.eulerAngles.y)), new Vector3(0, -1, 0), out hit2, .51f) && hit2.transform.tag == "Ground") || (Physics.Raycast(tr.position + new Vector3(-0.5f * Mathf.Cos(Mathf.Deg2Rad * tr.eulerAngles.y), 0, -0.5f * Mathf.Sin(Mathf.Deg2Rad * tr.eulerAngles.y)), new Vector3(0, -1, 0), out hit3, .51f) && hit3.transform.tag == "Ground") || (Physics.Raycast(tr.position + new Vector3(0.5f * Mathf.Cos(Mathf.Deg2Rad * tr.eulerAngles.y), 0, -0.5f * Mathf.Sin(Mathf.Deg2Rad * tr.eulerAngles.y)), new Vector3(0, -1, 0), out hit4, .51f) && hit4.transform.tag == "Ground")) {
            isGrounded = .2f;
        }
        // if the player is grounded, then they can jump when space is pressed
        if (isGrounded > 0) {
            isGrounded -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Space)) {
                isGrounded = 0;
                rb.velocity = new Vector3(rb.velocity.x, 40, rb.velocity.z);
            }
        } else {
            rb.velocity -= new Vector3(0, 55, 0) * Time.deltaTime;
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
            // once the player lets go of the spacebar, then their jump velocity gets cut short
            if (!Input.GetKey(KeyCode.Space) && rb.velocity.y > 0) {
                rb.velocity -= new Vector3(0, rb.velocity.y / 3, 0);
            }
        }

        // RESPAWN
        if (tr.position.y < 0) {
            Start();
        }
    }
}
