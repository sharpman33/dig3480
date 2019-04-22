using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    public float[] highscore;

    public bool exist;
    public float highest;
    public float secondHighest;
    public float thirdHighest;

    public GameObject highScoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        highest = 0;
        secondHighest = 0;
        thirdHighest = 0;

        for (int i = 0; i < 3; i++)
        {
            highscore[i] = 0;
        }
        highScoreBoard = GameObject.FindWithTag("HighScoreBoardText");
        highScoreBoard.GetComponent<Text>().text = " ";
    }


    void Update()
    {
        highscore[0] = highest;
        highscore[1] = secondHighest;
        highscore[2] = thirdHighest;

        
    }

    public void CheckHighScore(float score)
    {
        if(score > highest)
        {
            if (secondHighest > thirdHighest)
                thirdHighest = secondHighest;
            if (highest > secondHighest)
                secondHighest = highest;
            
            highest = score;           
        }
        else if( score > secondHighest)
        {
            if (thirdHighest > secondHighest)
                thirdHighest = secondHighest;
            secondHighest = score;
  
        }
        else if(score > thirdHighest)
        {
            thirdHighest = score;
        }

    }

    public void SetHighScoreText()
    {
        if(highScoreBoard == null)
        {
            highScoreBoard = GameObject.FindWithTag("HighScoreBoardText");
        }
        highScoreBoard.GetComponent<Text>().text = "High Scores" + "\n1st : " + highest + "\n2nd : " + secondHighest + "\n3rd : " + thirdHighest;
    }

}
