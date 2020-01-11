using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

	public float counter;
	public float maxCounter = 5;
	public int points;
	public int oldPoints;
	public bool timer;
	public bool isWon;

	public string nextLevelName;
	public GameObject dbText;
	public GameObject scText;
	public GameObject elText;
	public GameObject gameOverText;
	public GameObject youWinText;
	public GameObject mainMenuBtn;
	private GameObject player;
	public GUIStyle myStyle;
	public AudioSource enemyHitSFX;
	public AudioSource powerUpSFX;
	public AudioSource gameOverSFX;
	public AudioSource youWinSFX;

	private void Start() {
		myStyle.fontSize = 32;
		myStyle.normal.textColor = Color.black;
		player = GameObject.FindWithTag("Player");
	}

	private void Update() {
		if (counter > maxCounter) {
			player.GetComponent<PlayerController>().doubleBulletsOn = false;
			player.GetComponent<PlayerController>().speed = 4;
			dbText.SetActive(false);
			scText.SetActive(false);
			elText.SetActive(false);
			if (isWon) {
				SceneManager.LoadScene(nextLevelName);
			}
			timer = false;
			counter = 0;
		} else if (timer == true) {
			counter += Time.deltaTime;
		}

		//GameOver
		if (player.GetComponent<PlayerController>().lives == 0) {
			gameOverSFX.Play();
			Destroy(player.gameObject);
			gameOverText.SetActive(true);
			mainMenuBtn.SetActive(true);
			Time.timeScale = 0;
		}

		//Winning
		if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
			if (GameObject.Find("EnemyHolder").activeSelf == true) {
				Destroy(GameObject.Find("EnemyHolder").gameObject);
				youWinSFX.Play();
			}
			youWinText.SetActive(true);
			isWon = true;
			timer = true;
		}

		//Enemy hit SFX
		if (points > oldPoints) {
			enemyHitSFX.Play();
		}

		oldPoints = points;
	}

	public void GetPowerUp() {
		powerUpSFX.Play();
		int powerUpNmbr = Random.Range(3, 0);
		if (powerUpNmbr == 1) {
			DoubleBullets();
		} else if (powerUpNmbr == 2) {
			SuperCharge();
		} else if (powerUpNmbr == 3) {
			ExtraLife();
		}
	}

	public void DoubleBullets() {
		if (player.GetComponent<PlayerController>().doubleBulletsOn == false) {
			player.GetComponent<PlayerController>().doubleBulletsOn = true;
			dbText.SetActive(true);
			timer = true;
		}
	}

	public void SuperCharge() {
		player.GetComponent<PlayerController>().speed = 8;
		scText.SetActive(true);
		timer = true;
	}

	public void ExtraLife() {
		if (player.GetComponent<PlayerController>().lives < 3) {
			player.GetComponent<PlayerController>().lives++;
			elText.SetActive(true);
			timer = true;
		}
	}

	public void LoadMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	private void OnGUI() {
		GUI.Label(new Rect(10, 110, 100, 20), "Points:  " + points, myStyle);
	}
}
