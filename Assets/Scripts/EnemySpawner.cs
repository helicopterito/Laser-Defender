using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;

	private bool movingRight = true;
	private float xMax;
	private float xMin;

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xMax = rightmost.x;
		xMin = leftmost.x;

		SpawnEnemies ();
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeFormation = transform.position.x + (0.3f * width);
		float leftEdgeFormation = transform.position.x - (0.3f * width);
		if (leftEdgeFormation < xMin) {
			movingRight = true;
		} else if (rightEdgeFormation > xMax) {
			movingRight = false;
		}

		if (AllMembersDeath ()) {
			Debug.Log ("Empty formation");
			SpawnUntilFull ();
		}
	}

	public void OnDrawGizmo(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	bool AllMembersDeath(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	void SpawnEnemies(){
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;	
		}
	}

	Transform NextFreePosition(){
		foreach (Transform childPositionObject in transform) {
			if (childPositionObject.childCount == 0) {
				return childPositionObject;
			}
		}
		return null;
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();

		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}

		if(NextFreePosition()){
			Invoke ("SpawnUntilFull", spawnDelay);	
		}

	}
}
