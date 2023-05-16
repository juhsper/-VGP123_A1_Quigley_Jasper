using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Component references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // movement var
    public float speed = 5.0f;
    public float jumpForce = 420.0f;

    //groundcheck stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    Coroutine jumpForceChange = null;

    public int lives
    {
        get => _lives;
        set
        {
            //if (_lives < value)  gained a life
            //if (_lives > value)  lost a life
           _lives = value;

            Debug.Log("Lives value has changed to " + _lives.ToString());
            //if (_lives <= 0)  gameover
        }
    }
    private int _lives = 3;

    public int score
    {
        get => _score;
        set
        {
         
            _score = value;

            Debug.Log("Score value has changed to " + _score.ToString());
           
        }
    }
    private int _score = 3;


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       sr = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();
       
    // protects against bad input 
        if (speed <= 0) speed = 5.0f;
        if (jumpForce <= 0) jumpForce = 420.0f;
        if (groundCheckRadius <= 0) groundCheckRadius = 0.02f;

        if (!groundCheck) groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer); 
        if (isGrounded) rb.gravityScale = 1;


        if (curPlayingClips.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClips[0].clip.name != "attack")
                anim.SetTrigger("Fire");
            else if (curPlayingClips[0].clip.name == "attack")
                rb.velocity = Vector2.zero;
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (!isGrounded && Input.GetButtonDown("Jump"))
            anim.SetTrigger("jumpAttack");
            

        

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        if (hInput != 0)
            sr.flipX = (hInput > 0);

       

    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 10;
    }

    public void StartJumpForceChange()
    {
        Debug.Log("Powerup Picked up!");
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
            return;
        }

        StopCoroutine(jumpForceChange);
        jumpForceChange = null;
        jumpForce /= 2;
        StartJumpForceChange();


    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        jumpForceChange = null;
    }
}
