using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the Player control script, handling all player input, as well as everything related to
 * the player except for the health.
 * 
 * It takes the movement input from WASD and uses it to translate the player object.
 * 
 * It takes the input from Mouse X and rotates the player on the Y axis.
 * 
 * It takes the input from Mouse Y and rotates the camera on the X axis.
 * 
 * When spacebar is pressed, vertical velocity increases so that the player jumps.
 * 
 * Gravity always affects the player, making it fall towards the ground.
 * 
 * It uses an array of 3 weapons which is switched through upon right mouse click. */

public class FirstPersonController : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 5.0f;
    float verticalRotation = 0;
    public float upDownRange = 60f;

    public float gravity = -9.81f;
    public float jumpSpeed = 5f;

    float verticalVelocity = 0;

    CharacterController cc;
    Camera c;

    SceneControls scene;

    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;

    public GameObject bulletHole0;
    public GameObject bulletHole1;
    public GameObject bulletHole2;

    GameObject currentWeapon;
    GameObject[] weapons = new GameObject[3];
    int weaponIndex = 0;

    GameObject[] bulletHoles = new GameObject[3];

    public Vector3 holdingPoint = new Vector3(0.233374f, 12.496881f, 0.410088f);

    RayShooter rayShooter;


    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
        c = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<Camera>();
        rayShooter = gameObject.GetComponentInChildren<RayShooter>();

        scene = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneControls>();

        weapons[0] = (GameObject)Instantiate(weapon0, holdingPoint, Quaternion.identity);
        weapons[1] = (GameObject)Instantiate(weapon1, holdingPoint, Quaternion.identity);
        weapons[2] = (GameObject)Instantiate(weapon2, holdingPoint, Quaternion.identity);
   
        //weapons[0].SetActive(false);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);

        weapons[0].transform.parent = c.transform;
        weapons[1].transform.parent = c.transform;
        weapons[2].transform.parent = c.transform;

        bulletHoles[0] = bulletHole0;
        bulletHoles[1] = bulletHole1;
        bulletHoles[2] = bulletHole2;

        setBulletHole(bulletHoles[0]);
        currentWeapon = weapons[0];
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Cancel"))
        {
            scene.reset();
        }

        if (!scene.inputIsEnabled())
        {
            return;
        }

        // Rotation
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        c.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        // Movement
        float forwardSpeed = Input.GetAxis("Vertical");
        float sideSpeed = Input.GetAxis("Horizontal");
        verticalVelocity += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            toggleWeapon();
        }

        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;


        cc.Move(movementSpeed * speed * Time.deltaTime);
		
	}

    void toggleWeapon()
    {
        currentWeapon.SetActive(false);
        weaponIndex++;
        if(weaponIndex >= 3)
        {
            weaponIndex = 0;
        }
        currentWeapon = weapons[weaponIndex];
        currentWeapon.SetActive(true);
        setBulletHole(bulletHoles[weaponIndex]);
    }

    void setBulletHole(GameObject bulletHole)
    {
        rayShooter.setBulletHole(bulletHole);
    }
}
