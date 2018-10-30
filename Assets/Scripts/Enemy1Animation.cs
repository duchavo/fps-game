using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Animation : MonoBehaviour {

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public float detectionRange = 5.0f;
    static Animator animator;

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
        animator = GetComponent<Animator>();
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
            delta.y = 0;
            if (delta.magnitude < detectionRange)
            {
                transform.LookAt(player.transform);
                Debug.Log("hALLA");
            }
            transform.Translate(0, 0, speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
            animator.SetBool("isShooting", false);
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
                        animator.SetBool("isShooting", true);
                        Debug.Log("Halla, fireball");
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
