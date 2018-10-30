using UnityEngine;
using System.Collections;

/* Controls the players health, and whether the player is still alive.
 * Has a function to decrement the health, and die to be called when the
 * health is equal to 0.
 */

public class PlayerCharacter : MonoBehaviour {
    public int _health = 10;


	void Start() {
		
	}

	public void Hurt(int damage) {
		_health -= damage;
        if(_health <= 0)
        {
            die();
        }
	}

    public void reset()
    {
        _health = 10;
    }

    void die()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.Translate(0, 1, 0);
        player.transform.Rotate(0, 0, -90);
        SceneControls scene = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneControls>();
        scene.gameOver();
    }


}
