using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float jetpackForce;
    private Rigidbody2D rb;
    public float forwardMovementSpeed;

    public Transform groundCheckTransform;
    public LayerMask groundCheckLayerMask;
    private bool grounded;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
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

        UpdateGroundedStatus();
    }

    private void UpdateGroundedStatus()
    {
        grounded = Physics2D.OverlapCircle(
            groundCheckTransform.position,
            .1f,
            groundCheckLayerMask);

        Debug.Log(grounded);
        animator.SetBool("grounded", grounded);
    }
}
