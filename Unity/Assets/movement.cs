using UnityEngine;

public class movement : MonoBehaviour
{
    public Rigidbody rb;
    public float vel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity = new Vector3(0, 0, vel);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.linearVelocity = new Vector3(0, 0, -vel);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocity = new Vector3(vel, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocity = new Vector3(-vel, 0, 0);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rb.linearVelocity = new Vector3(0, vel, 0);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            rb.linearVelocity = new Vector3(0, -vel, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, 0, 0);
        }
    }
}
