using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {

	public int lives;
	SpriteRenderer sr;

	public AudioSource hitSFX;

	private void Start() {
		sr = GetComponent<SpriteRenderer>();
		sr.color = Color.white;
		lives = 3;
		hitSFX = transform.parent.GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		if (collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("EnemyBullet")) {
			lives -= 1;
			hitSFX.Play();
			Destroy(collision.gameObject);
		}
	}

	private void Update() {
		if (lives == 2) {
			sr.color = Color.gray;
		} else if (lives == 1) {
			sr.color = Color.black;
		}
		if (lives < 1) {
			Destroy(gameObject);
		}
	}
}
