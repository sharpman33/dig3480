using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    public AudioClip pickupSound;
    public AudioClip winSound;

    public Text winText;
    public Text loseText;
    public Text scoreText;
    public Text livesText;

    private AudioSource source;
    private Rigidbody2D rb;

    public float speed;
    public float jumpForce;

    private float score;
    private float lives;

    public bool isGrounded;
    public bool lookingRight;



    void Start()
    {
        lives = 3;
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        winText.text = " ";
        loseText.text = "";
        setScoreText();
        setLivesText();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb.AddForce(movement * speed);

        if (moveHorizontal >= 0.1 && lookingRight || moveHorizontal <= -0.01 && !lookingRight)
        {
            Flip();
        }

        if(isGrounded == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("jump");
        }
        if(moveHorizontal == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag =="Ground")
        {
            isGrounded = true;
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            source.PlayOneShot(pickupSound);
            score++;
            setScoreText();
        }

       if(other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives--;
            setLivesText();

            if(lives <= 0)
            {
                setLivesText();
            }
        }
    }

    void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();

        if(score == 4 && gameController.isStage2 == true)
        {
            source.PlayOneShot(winSound);
            winText.text = "You Win!";
            Time.timeScale = 0f;
        }
        else if(score == 4 && gameController.isStage2 == false)
        {
            SceneManager.LoadScene("Level 2");
            gameController.isStage2 = true;
        }
    }

    void setLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if(lives <= 0)
        {
            loseText.text = "You Lose!";
            Time.timeScale = 0f;
        }
    }

    void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 characterScale = transform.localScale;
        characterScale.x *= -1;
        transform.localScale = characterScale;
    }
}
