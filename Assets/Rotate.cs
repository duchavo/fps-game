using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Explanation. Create two cubes, the other cube is the parent. Add script to the parent. Add child to public Transform cube. 

public class Rotate : MonoBehaviour {

    public Transform cube;
   
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
    // Confusing myself. Something is working at least
	void Update () {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 120f, Space.World);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cube.parent = transform;
            transform.Rotate(Vector3.up, Time.deltaTime * 90f, Space.World);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cube.parent = null;
        }
         
	}
}
