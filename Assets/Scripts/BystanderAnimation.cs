using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderAnimation : MonoBehaviour {

    public float speed = 3.0f;
    public float obstacleRange = 10.0f;
    static Animator animator;

    SceneControls scene;

    void Start()
    {
        scene = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneControls>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!scene.inputIsEnabled())
        {
            return;
        }
        transform.Translate(0, 0, speed * Time.deltaTime);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                Vector3 moveDir = transform.position - hitObject.transform.position;
                moveDir.y = 0;
                speed = 5.0f;
                transform.Translate(moveDir.normalized * speed * Time.deltaTime);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
    }
}
