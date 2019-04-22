using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip loseSound;

    public GameObject player;
    public GameObject canvas;
    public GameObject highScore;


    public Text winText;
    public Text loseText;
    public Text scoreText;
    public Text livesText;

    private float startLives = 3;
    public float score;
    public float lives;
    public float coinCount;
    public bool isStage2;
    public bool gameOver;
    public bool restart;
    public bool gotoStage2;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        canvas = GameObject.FindWithTag("Canvas");
        highScore = GameObject.FindWithTag("HighScore");
        Time.timeScale = 1f;
    }

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        lives = startLives;
        score = 0;



        winText.text = "";
        scoreText.text = "";
        livesText.text = "";
        loseText.text = "";

        SetLivesText();
        SetScoreText();

    }

    void Update()
    {
        if(highScore == null)
        {
            highScore = GameObject.FindWithTag("HighScore");
        }
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("It Quit");

        }

        if(restart)
        {
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.R))
            {
                Destroy(this.gameObject);
                Destroy(canvas.gameObject);
                restart = false;
                SceneManager.LoadScene("Game");
            }
        }

    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level 2");
    }

    public void CoinCollected()
    {
        coinCount++;
        SetScoreText();
    }

    public void EnemyHit()
    {
        lives--;
        SetLivesText();
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();

        if(coinCount == 4 && isStage2 == false)
        {
            coinCount = 0;
            isStage2 = true;
            canvas.GetComponent<DontDestroyOnLoad>().Check();
            highScore.GetComponent<DontDestroyOnLoad>().Check();
            this.gameObject.GetComponent<DontDestroyOnLoad>().Check();
            //gotoStage2 = true;
            StartCoroutine(NextLevel());


            lives = startLives;
            SetLivesText();
            SetScoreText();
            
        }
        else if (coinCount == 4 && isStage2 == true)
        {
            audioSource.PlayOneShot(winSound);
            winText.text = "You Win!" + "\n\nScore: " + score.ToString() + "\n\n\nPress 'R' To Play Again";
            highScore.GetComponent<HighScoreController>().CheckHighScore(score);
            highScore.GetComponent<HighScoreController>().SetHighScoreText();
            restart = true;
        }
        
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            audioSource.PlayOneShot(loseSound);
            loseText.text = "You Lose!" + "\n\nScore: " + score.ToString() + "\nPress 'R' To Restart";
            gameOver = true;
            restart= true;
        }
    }
}
