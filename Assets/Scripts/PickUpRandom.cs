using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRandom : MonoBehaviour {

    public GameObject healthPotion;
    public Vector3 center;
    public Vector3 size;
 

	// Use this for initialization
	void Start () {
        SpawnRandomLoc();
	}

    void SpawnRandomLoc()
    {
       
         Vector3 position = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2),
         Random.Range(-size.z / 2, size.z / 2));
        Instantiate(healthPotion, position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.H))
        {
            SpawnRandomLoc();
        }
	}
}
