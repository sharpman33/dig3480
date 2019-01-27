using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

    public Text scoreCount;
    public Text win;
    public float speed;
    private int score;


	void Start () {
        rb = GetComponent<Rigidbody>();

        score = 0;
        ScoreCount();
        win.text = "";
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
            score++;
            Debug.Log(score);
            other.gameObject.SetActive(false);
            ScoreCount();
        }
    }

    void ScoreCount()
    {
        scoreCount.text = "Score: " + score.ToString();
        if(score >= 10)
        {
            win.text = "You Win!";
        }
    }


}
