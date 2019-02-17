using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

    public Text scoreCount;
    public Text win;
    public Text itemCount;
    public Text lose;
    public Text lives;

    public float speed;
    private int score;
    private int life;
    private int pickupcount;


	void Start () {
        rb = GetComponent<Rigidbody>();

        life = 3;
        score = 0;
        pickupcount = 0;

        ScoreCount();
        ItemCount();
        LifeCount();

        win.text = "";
        lose.text = "";
	}
	

	void FixedUpdate () {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3 (moveX, 0f, moveY);

        rb.AddForce (movement * speed);
	}

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if(other.gameObject.CompareTag("PickUp"))
        {
            score += 10;
            pickupcount++;
            other.gameObject.SetActive(false);
            ScoreCount();
            ItemCount();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            life--;
            other.gameObject.SetActive(false);
            ScoreCount();
            LifeCount();
        }
    }

    void ScoreCount()
    {
        scoreCount.text = "Score: " + score.ToString();
        if(score >= 130)
        {
            win.text = "You Won!";
            Time.timeScale = 0f;
        }
    }

    void ItemCount()
    {
        itemCount.text = "Picked Up: " + pickupcount.ToString();
    }

    void LifeCount()
    {
        lives.text = "Lives: " + life.ToString();
        if(life <= 0)
        {
            lose.text = "Game Over";
            Time.timeScale = 0f;
        }
    }

}
