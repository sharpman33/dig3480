using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Vector3 pointB;
    public Vector3 pointA;
    public float speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time/2 * speed, 1) * speed);
      /*  if (transform.position != pointB)
        {
            Debug.Log("Moving");
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }*/
    }
}
