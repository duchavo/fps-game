using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public Transform loc1;
    public Transform loc2;
    public Transform loc3;
    public Transform loc4;

    private void OnTriggerEnter(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "tel1":
                this.transform.position = loc1.position;
                break;
            case "tel2":
                this.transform.position = loc2.position;
                break;
            case "tel3":
                this.transform.position = loc3.position;
                break;
            case "tel4":
                this.transform.position = loc4.position;
                break;
        }
        /*
        if (other.gameObject.tag == "tel1")
        {
            this.transform.position = loc1.position;
        }
        */
    }
}
