using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public GameObject bullet;

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Enemy")) {
			Destroy(other.gameObject);
			Destroy(bullet);
			GameObject.Find("GameController").GetComponent<GameController>().points += 10;
		}
		if (other.gameObject.CompareTag("BonusEnemy")) {
			GameObject.Find("GameController").GetComponent<GameController>().GetPowerUp();
			Destroy(other.gameObject);
			Destroy(bullet);
			GameObject.Find("GameController").GetComponent<GameController>().points += 50;
		}
	}
}
