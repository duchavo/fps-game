using UnityEngine;
using System.Collections;

/* Bystander will raycast, and if it hits the player it will turn away from the player. */

public class Bystander : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    SceneControls scene;

    void Start()
    {
        scene = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneControls>();
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
                transform.Translate(moveDir.normalized * speed * Time.deltaTime);
            }
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

}
