using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Adds 1.0f to player when 
 * it picks up the health potion. 
 */

public class HealthPotion : MonoBehaviour {

    public int healthAmt = 5;
    PlayerCharacter pc;
	// Use this for initialization
	void Awake()
    {
        pc = FindObjectOfType<PlayerCharacter>();
    }

    private void OnTriggerEnter(Collider other)
    {
           if (pc._health < 10)
        {
            Destroy(gameObject);
            pc._health += healthAmt;
        }
    }
}
