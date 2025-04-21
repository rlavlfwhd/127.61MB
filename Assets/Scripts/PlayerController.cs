using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;

    private bool isInvincibility = false;
    private bool isHaste = false;
    private bool isJumpUp = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }


    void Update()
    {
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        pAni.SetFloat("Walk", rb.velocity.sqrMagnitude > 0 ? 1.0f : 0.0f);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (moveInput < 0)
            transform.localScale = new Vector3(-0.7f, 0.7f, 1f);

        if (moveInput > 0)
            transform.localScale = new Vector3(0.7f, 0.7f, 1f);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("JumpAction");
        }

        if (isHaste)
        {
            moveSpeed = 4;
        }

        if(isJumpUp)
        {
            jumpForce = 13f;
        }
        else
        {
            jumpForce = 5f;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Respawn"))
        {
            if (!isInvincibility)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if (collision.CompareTag("Enemy"))
        {
            if (!isInvincibility)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(InvincibilityCoroutine());
        }

        if (collision.CompareTag("Item2"))
        {
            Destroy(collision.gameObject);            
        }

        if (collision.CompareTag("Item3"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(JumpUpCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincibility = true;
        yield return new WaitForSeconds(3f);
        isInvincibility = false;
    }
    private IEnumerator JumpUpCoroutine()
    {
        isJumpUp = true;
        yield return new WaitForSeconds(4f);
        isJumpUp = false;
    }
}
