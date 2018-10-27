using UnityEngine;
using System.Collections;

/* Enemies wander and will shoot a fireball at the player on sight.
 * When the player is within the detection range, Enemy 1 will turn towards the player
 and shoot.
 Enemy 1 can only shoot in a rate given by the shootInterval to avoid spamming.*/

public class Enemy1 : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public float detectionRange = 1.0f;

    public float shootInterval = 50.0f;
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
            if(delta.magnitude < detectionRange)
            {
                transform.LookAt(player.transform);
            }
            transform.Translate(0, 0, speed * Time.deltaTime);

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
                        Debug.Log("Halla");
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
