using UnityEngine;
using System.Collections;

/* Controls targets that may react to being hit by the RayShooter objects. 
 * Has a method to be called whenever the object is supposed to react to being hit,
 * and if health is equal to 0 it calls the die method, causing the object to destroy itself
 * after a few seconds, and let SceneControls know that it died. */

public class ReactiveTarget : MonoBehaviour
{

    public float health = 3f;
    public float damage = 1f;

    SceneControls sceneControls;

    private void Start()
    {
        sceneControls = GameObject.FindGameObjectWithTag("Respawn")
        .GetComponent<SceneControls>();
    }

    public void ReactToHit()
    {
        health -= damage;
        if(health <= 0)
        {
            WanderingAI behavior = GetComponent<WanderingAI>();
            if (behavior != null)
            {
                behavior.SetAlive(false);
            }
            StartCoroutine(Die());
        }
        
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);

        sceneControls.killEnemy();

    }
}

