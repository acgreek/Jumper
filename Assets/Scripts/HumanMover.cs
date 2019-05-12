using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMover : MonoBehaviour
{
 
    public float speed = .1f;
    public float jumpForce = 2f;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector3 playerPos = new Vector3 (2f, -1f, 0);
    private bool facingRight = true;
    public bool grounded = false;
    private int jumpCountdown =0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update () 
    {
        float moveSpeed = (Input.GetAxis("Horizontal") * speed);
        if (moveSpeed < 0 && facingRight) {
            facingRight = false;
            transform.Rotate(0, 180, 0);
        }
        if (moveSpeed > 0 && !facingRight)
        {
            transform.Rotate(0, 180, 0);
            facingRight = true;
        }
        anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
        float xPos = transform.position.x + moveSpeed;
        Vector3 currentPos = transform.position;
	    currentPos.x = Mathf.Clamp (xPos, -8f, 8f);
        transform.position = currentPos;
        if (grounded || jumpCountdown > 0)
        {
            bool jumping = Input.GetButton("Fire1");
            if (jumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                if (0 == jumpCountdown)
                {
                    jumpCountdown = 10;
                } 
            }
            jumpCountdown--;

        }

    }
}
