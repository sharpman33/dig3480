using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float rotation;
    public float a;
	void Start () {
		
	}

	void Update () {
        transform.Rotate(new Vector3(15, a, rotation) * Time.deltaTime);
	}
}
