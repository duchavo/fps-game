using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private bool isCrouched = false;
    private CharacterController cc;
    private Camera mainCamera;

    private float centerStanding = 1.1f;
    private float centerCrouching = 0.505f;

    private float heightStanding = 2f;
    private float heightCrouching = 1f;

    private float camYStanding = 1.75f;
    private float camYCrouching = 0.875f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Crouch")) //is the crouch button being held?
        {
            if (!isCrouched)
                crouch();
            else
            {
                stand();
            }

        }
        
    }

    void crouch()
    {
        Debug.Log("Crouching");
        cc.height = heightCrouching;
        cc.center = new Vector3(0, centerCrouching, 0);
        mainCamera.transform.localPosition = new Vector3(0, camYCrouching, 0);
        isCrouched = true;
    }

    void stand()
    {
        Debug.Log("Standing");
        cc.height = heightStanding;
        cc.center = new Vector3(0, centerStanding, 0);
        mainCamera.transform.localPosition = new Vector3(0, camYStanding, 0);
        isCrouched = false;
    }
}