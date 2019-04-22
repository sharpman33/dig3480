using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    public Vector3 pointA;
    public Vector3 pointB;

    private bool lookingRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time / 2 * speed, 1) * speed);
        
        //Debug.Log(transform.position.x + " " + this.transform.position.x);
        
    }

    private void FixedUpdate()
    {
        if (transform.position == pointA)
        {
            Debug.Log("Iflip");
            Flip();
        }

        if (this.transform.position.x == pointB.x)
        {
            Debug.Log("Iflip");
            Flip();
        }
    }

    void Flip()
    {
        Vector3 characterScale = transform.localScale;
        characterScale.x *= -1;
        transform.localScale = characterScale;
    }
}
