using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour {

    public Transform target1;
    public Transform target2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "teleport")
        {
            this.transform.position = target1.position;
        }
        if (other.gameObject.tag == "teleport2")
        {
            this.transform.position = target2.position;
        }
    }
}
