using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1.0f;
	public float shootForce = 100f;
	public float fireRate;
	private float nextFire;
	public int lives;
	public bool doubleBulletsOn;
	public GameObject playerBullet;
	public GameObject gunC;
	public GameObject gunL;
	public GameObject gunR;
	public Rigidbody2D rb;

	public AudioSource shootSFX;
	public AudioSource hitSFX;
	public GUIStyle myStyle;
	public Sprite forwardSprt;
	public Sprite turnRightSprt;
	public Sprite turnLeftSprt;
	public Animator animator;
	SpriteRenderer sr;

	void Start() {
		myStyle.fontSize = 32;
		myStyle.normal.textColor = Color.black;
		sr = GetComponent<SpriteRenderer>();
		lives = 3;
	}

	void Update() {

		if (Input.GetKeyDown(KeyCode.Space) && GameObject.FindGameObjectWithTag("PlayerBullet") == null) {
			Shoot();
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			animator.SetBool("turningLeft", true);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			animator.SetBool("turningRight", true);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			animator.SetBool("turningRight", false);
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			animator.SetBool("turningLeft", false);
		}
	}

	void FixedUpdate() {
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0);
		transform.position += move * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("EnemyBullet")) {
			lives -= 1;
			hitSFX.Play();
			Destroy(collision.gameObject);
		} else if (collision.gameObject.CompareTag("Enemy")) {
			lives = 0;
		}
	}

	void Shoot() {
		if (doubleBulletsOn == false) {
			GameObject bullet = Instantiate(playerBullet, gunC.transform.position, Quaternion.identity) as GameObject;
			bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * shootForce);
			Destroy(bullet, 1);

		} else if (doubleBulletsOn == true) {
			GameObject bullet1 = Instantiate(playerBullet, gunL.transform.position, Quaternion.identity) as GameObject;
			bullet1.GetComponent<Rigidbody2D>().AddForce(transform.up * shootForce);
			Destroy(bullet1, 1);
			//**********//
			GameObject bullet2 = Instantiate(playerBullet, gunR.transform.position, Quaternion.identity) as GameObject;
			bullet2.GetComponent<Rigidbody2D>().AddForce(transform.up * shootForce);
			Destroy(bullet2, 1);
		}
		shootSFX.Play();
	}

	private void OnGUI() {
		GUI.Label(new Rect(10, 70, 100, 20), "Lives:  " + lives, myStyle);
	}
}
