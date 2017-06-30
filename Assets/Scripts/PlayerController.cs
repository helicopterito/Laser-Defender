using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 200;
	public AudioClip fireSound;

	float minX;
	float maxX;
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));

		minX = leftmost.x + padding;
		maxX = rightmost.x - padding;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			//gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			//gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		float newX = Mathf.Clamp (transform.position.x, minX, maxX);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0, 1, 0);
		GameObject beam = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
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

	void Die(){
		LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		Destroy (gameObject);
		levelManager.LoadLevel ("Lose");
	}
}
