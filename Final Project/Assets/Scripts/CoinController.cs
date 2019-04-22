using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float points;
    public GameObject gameController;
    public GameObject player;
    public Animator animator;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();

        //animator.SetBool("start", true);
    }

    
    void Update()
    {
        
    }

    public void GivePoints(float score)
    {
        if (player.GetComponent<PlayerController>().isPointBoost == false)
            gameController.GetComponent<GameController>().score += score;
        else if (player.GetComponent<PlayerController>().isPointBoost == true)
            gameController.GetComponent<GameController>().score += (score * 2);
    }
}
