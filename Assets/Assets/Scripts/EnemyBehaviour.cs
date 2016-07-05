using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public int health = 2;
	public Transform explosion;
	public Transform playerHit;
	public AudioClip hitSound;

	private GameController controller; 

	void Start() {
		 controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	void OnCollisionEnter2D(Collision2D theCollision){
		

		if (theCollision.gameObject.name.Contains("laser")) {
			LaserBehaviour laser = theCollision.gameObject.GetComponent ("LaserBehaviour") as LaserBehaviour;
			health -= laser.damage;
			print ("Health: " + health);
			Destroy (theCollision.gameObject);
			GetComponent<AudioSource> ().PlayOneShot (hitSound);
		}

		if (theCollision.gameObject.name.Contains ("playerShip")) {
			controller.PlayerHit ();
			GameObject playerHitExplosion = ((Transform)Instantiate (playerHit, this.transform.position, this.transform.rotation)).gameObject;
			Destroy (playerHitExplosion, 1.0f);
			Destroy (this.gameObject);
			controller.KilledEnemy ();
		}

		if (health <= 0) {
			if (explosion) {
				GameObject exploder = ((Transform)Instantiate (explosion, this.transform.position, this.transform.rotation)).gameObject;
				Destroy (exploder, 2.0f);
			}

			Destroy (this.gameObject);

			controller.KilledEnemy ();
			controller.IncreaseScore (10);
		}
	}
}
