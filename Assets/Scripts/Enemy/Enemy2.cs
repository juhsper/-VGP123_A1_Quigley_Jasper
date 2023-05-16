using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{

    Animator anim;
    public float jumpForce = 8f;          // The force applied to make the enemy jump
    public float timeBetweenJumps = 3f;   // The time delay between each jump

    private Rigidbody2D rb;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("Jump", 0f, timeBetweenJumps);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
        if (isJumping == true)
        {
        //    anim.SetBool("isJumping", isJumping);

           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isJumping = false;
        }
    }
}


