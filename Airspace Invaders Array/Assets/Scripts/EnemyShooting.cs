using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

	public float shootForce = 100f;
	public float counter;
	public float maxCounter;
	int shooterNmbr;
	public GameObject enemyBullet;
	GameObject newShooter;
	public GameObject[] enemies;
	public GameObject[] columns;

	public GUIStyle myStyle;
	public AudioSource shootSFX;

	void Start() {
		myStyle.fontSize = 16;
		myStyle.normal.textColor = Color.white;

		enemies = new GameObject[transform.childCount];
	}

	void Update() {

		columns = transform.GetComponent<EnemyController>().columns;

		//Find the lowest enemies
		for (int i = 0; i < transform.childCount; i++) {
			if (columns[i] != null) {
				enemies[i] = columns[i].GetComponent<ColumnController>().lowestEnemy;
			} else {
				transform.GetComponent<EnemyController>().columnMissing = true;

				enemies = new GameObject[transform.childCount];
				for (int u = 0; u < transform.childCount; u++) {
					enemies[u] = transform.GetChild(u).gameObject;
				}
			}
		}

		//SHOOTING
		if (counter > maxCounter) {

			shooterNmbr = Random.Range(0, enemies.Length);
			newShooter = enemies[shooterNmbr];

			Shoot(newShooter);
			counter = 0;
			//}
		} else {
			counter += Time.deltaTime / maxCounter;
		}
	}

	void Shoot(GameObject shooter) {
		GameObject bullet = Instantiate(enemyBullet, shooter.transform.position, Quaternion.identity) as GameObject;
		bullet.GetComponent<Rigidbody2D>().AddForce(-transform.up * shootForce);
		shootSFX.Play();
		Destroy(bullet, 3);
	}
}


