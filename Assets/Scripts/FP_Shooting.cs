using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Shooting : MonoBehaviour {

    public GameObject bullet_prefab;
    public float bulletImpulse = 100f;

    Camera c;

	// Use this for initialization
	void Start () {
        c = GameObject.FindGameObjectWithTag("MainCamera")
           .GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = (GameObject)Instantiate(bullet_prefab, c.transform.position + c.transform.forward, c.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(c.transform.forward * bulletImpulse, ForceMode.Impulse);
        }
		
	}
}
