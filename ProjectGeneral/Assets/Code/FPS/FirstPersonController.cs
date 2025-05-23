using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public static FirstPersonController instance;

    public Rigidbody rb;
    public Collider Coll;

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;

    public bool FirstPersonActive = false;

    private void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        
        if (FirstPersonActive)
        {
            Movement();

        } else
        {
            return;
        }
    }

    public void Movement()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= speed;

		Vector3 velocity = rb.velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;
		rb.AddForce(velocityChange, ForceMode.VelocityChange);
		
		if (grounded)
		{
			if (canJump && Input.GetButton("Jump"))
			{
				rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}


		rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

		grounded = false;
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            grounded = true;
        }
    }


    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void SwitchFPSmode()
    {
        FirstPersonActive = !FirstPersonActive;
    }
}
