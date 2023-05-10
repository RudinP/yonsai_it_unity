using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float jetpackForce;
    private Rigidbody2D rb;
    public float forwardMovementSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");

        if (jetpackActive)
        {
            rb.AddForce(jetpackForce * Vector2.up);
        }

        Vector2 newVelocity = rb.velocity;
        newVelocity.x = forwardMovementSpeed;
        rb.velocity = newVelocity;
    }
}
