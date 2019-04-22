using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");



        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Check()
    {
            DontDestroyOnLoad(this.gameObject);
    }
}
