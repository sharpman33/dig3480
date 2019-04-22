using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public GameObject gameController;
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    public AudioClip coinSound;
    public AudioClip hitSound;
    public AudioClip pointBoostSound;
    public AudioClip speedBoostSound;

    private AudioSource source;
    private Rigidbody2D rb;

    public float startPowerUpTimer;
    public float powerUpTimer;
    public float speed;
    public float jumpForce;

    private float baseSpeed;

    public bool isGrounded;
    public bool lookingRight;

    public bool isSpeedUp;
    public bool isPointBoost;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        baseSpeed = 10;
        startPowerUpTimer = powerUpTimer;
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        startPowerUpTimer -= Time.deltaTime;

        if(startPowerUpTimer <= 0)
        {
            isSpeedUp = false;
            isPointBoost = false;
        }
        PowerUpCheck();
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
        if(other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            source.PlayOneShot(coinSound);
            other.GetComponent<CoinController>().GivePoints(other.GetComponent<CoinController>().points);
            gameController.GetComponent<GameController>().CoinCollected();
        }

        if(other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            gameController.GetComponent<GameController>().EnemyHit();
            source.PlayOneShot(hitSound);
        }

        if(other.CompareTag("Pickup"))
        {
            if(other.GetComponent<PowerUp>().type ==  "SpeedUp")
            {
                isSpeedUp = true;
                startPowerUpTimer = powerUpTimer;
                speed += 5;
                Debug.Log("Add Speed");
                source.PlayOneShot(speedBoostSound);
            }
            else if(other.GetComponent<PowerUp>().type == "PointBoost")
            {
                isPointBoost = true;
                startPowerUpTimer = powerUpTimer;
                Debug.Log("Point Boost");
                source.PlayOneShot(pointBoostSound);
            }
            other.gameObject.SetActive(false);
        }
       
    }

    void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 characterScale = transform.localScale;
        characterScale.x *= -1;
        transform.localScale = characterScale;
    }

    void PowerUpCheck()
    {
        if(!isSpeedUp)
        {
            speed = baseSpeed;
        }

    }
}
