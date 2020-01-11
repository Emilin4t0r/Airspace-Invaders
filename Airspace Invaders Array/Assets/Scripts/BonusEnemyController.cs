using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEnemyController : MonoBehaviour {

	public float moveSpeed;
	public GameObject bonusEnemy;

	public int spawnCheck;
	public float counter;
	public float maxCounter = 3f;

	void Update() {

		if (counter > maxCounter) {
			spawnCheck = Random.Range(5, 0);
			if (spawnCheck == 1) {
				Spawn();
			}
			counter = 0;
		} else {
			counter += Time.deltaTime;
		}
	}

	void Spawn() {
		GameObject newBonus = Instantiate(bonusEnemy, transform.position, Quaternion.identity) as GameObject;
		newBonus.GetComponent<Rigidbody2D>().AddForce(-transform.right * moveSpeed);
		Destroy(newBonus, 20);
	}
}
