using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 11f;
    public float fastFallSpeed = 20f; 

    private Rigidbody2D rb;
    private bool isGrounded = false;

    //animations & facing
    private Animator anim;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr   = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        //left-right movement
        float xInput = 0f;
        if (Input.GetKey(KeyCode.A)) xInput = -1f;
        if (Input.GetKey(KeyCode.D)) xInput =  1f;

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        
        // Debug.Log(rb.velocity.y);

        // kill the player if they fall off the map
        // if (rb.velocity.y < -15f) {
        //     Debug.Log("U died twin!");
        //     SceneManager.LoadScene("DeathScene");
        // }


        if (rb.velocity.y < -15f) {
            SceneManager.LoadScene("DeathScene");
        }

        //flip sprite to face direction
        if (xInput != 0f)
            sr.flipX = xInput > 0f;

        //jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //fast fall (optional)
        if (Input.GetKey(KeyCode.S) && !isGrounded)
        {
            rb.velocity += Vector2.down * fastFallSpeed * Time.deltaTime;
        }

        anim.SetBool("Grounded", isGrounded);

        //only allow running when grounded
        float horizontal = Mathf.Abs(rb.velocity.x);
        if (!isGrounded)
            horizontal = 0f;  //forces jump animation

        anim.SetFloat("horizontal", horizontal);

        //screen wrap
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth  = halfHeight * Camera.main.aspect;

        if (transform.position.x >  halfWidth)
            transform.position = new Vector3(-halfWidth, transform.position.y, transform.position.z);
        else if (transform.position.x < -halfWidth)
            transform.position = new Vector3( halfWidth, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground") || other.collider.CompareTag("Platform"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground") || other.collider.CompareTag("Platform"))
            isGrounded = false;
    }
}
