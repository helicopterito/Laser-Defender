using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;


	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit ();
			if (health <= 0) {
				Die ();
			}
			Debug.Log ("Hit by a projectile");
		}
	}

	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject beam = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void Update(){
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void Die(){
		Destroy (gameObject);
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
		scoreKeeper.Score (scoreValue);
	}

}
