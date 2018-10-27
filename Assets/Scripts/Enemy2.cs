using UnityEngine;
using System.Collections;

/* Enemies wander and will shoot a fireball at the player on sight.
 * When the player is within the detection range, Enemy 2 will turn towards the player
 and shoot.
 Enemy 2 can only shoot in a rate given by the shootInterval to avoid spamming.
 Enemy 2 will also stay put at its hiding spot, and shoot from there.*/

public class Enemy2 : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public float detectionRange = 1.0f;

    public float shootInterval = 5.0f;
    float dT = 0;

    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    private bool _alive;

    SceneControls scene;

    void Start()
    {
        _alive = true;
        scene = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneControls>();
    }

    void Update()
    {
        dT += Time.deltaTime;
        if (!scene.inputIsEnabled())
        {
            return;
        }
        if (_alive)
        {


            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 delta = player.transform.position - transform.position;
            if (delta.magnitude < detectionRange)
            {
                transform.LookAt(player.transform);
            }
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null && dT > shootInterval)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                        _fireball.transform.position += new Vector3(0, 1, 0);
                        dT = 0;
                    }
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}

