using UnityEngine;
using System.Collections;

/* Fireball that travels forward by translating until it collides.
 * If collided with player, it triggers the player.Hurt method. 
 * After colliding it destroys itself immideately.
 */

public class Fireball : MonoBehaviour {
	public float speed = 10.0f;
	public int damage = 5;

	void Update() {
		transform.Translate(0, 0, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		PlayerCharacter player = other.GetComponent<PlayerCharacter>();
		if (player != null) {
			player.Hurt(damage);
		}
		Destroy(this.gameObject);
	}
}
