using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Rigidbody rb;
    public float vel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity = transform.TransformDirection(new Vector3(0, 0, vel)); 
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = transform.TransformDirection(new Vector3(0, 0, -vel));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = transform.TransformDirection(new Vector3(vel, 0, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = transform.TransformDirection(new Vector3(-vel, 0, 0));
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = transform.TransformDirection(new Vector3(0, vel, 0));
        }
        else if (Input.GetKey(KeyCode.C))
        {
            rb.linearVelocity = transform.TransformDirection(new Vector3(0, -vel, 0));
        }
        else
        {
            rb.linearVelocity = transform.TransformDirection(new Vector3(0, 0, 0));
        }
    }
}
