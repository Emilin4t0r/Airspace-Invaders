using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnController : MonoBehaviour {

	public GameObject lowestEnemy;
	private int children;
	public Collider2D leftWall;
	public Collider2D rightWall;

	void Update() {
		if (transform.childCount == 0) {
			Destroy(gameObject);
		}
		children = transform.childCount;
		lowestEnemy = transform.GetChild(children - 1).gameObject;

		//Checking surroundings for walls
		Vector2 left = transform.TransformDirection(Vector2.left);
		Vector2 right = transform.TransformDirection(Vector2.right);

		RaycastHit2D leftHit = Physics2D.Raycast(transform.position, left, 1);
		RaycastHit2D rightHit = Physics2D.Raycast(transform.position, right, 1);

		if (leftHit.collider == leftWall && GameObject.Find("EnemyHolder").GetComponent<EnemyController>().atLeftWall == false) {
			GameObject.Find("EnemyHolder").GetComponent<EnemyController>().atLeftWall = true;
		} else if (rightHit.collider == rightWall && GameObject.Find("EnemyHolder").GetComponent<EnemyController>().atRightWall == false) {
			GameObject.Find("EnemyHolder").GetComponent<EnemyController>().atRightWall = true;
		}
	}
}

