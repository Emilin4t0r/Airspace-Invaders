using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float distance = 0.5f;
	public float waitTime;
	public float movingAmount = 10f;
	public float accelMultip = 0.025f;
	public int enemyCount;
	public int oldEnemyCount;
	public bool atLeftWall;
	public bool atRightWall;
	public bool pause = false;
	public bool columnMissing;
	public GameObject enemyHolder;
	public GameObject[] columns;

	public GameObject pausedText;
	public GameObject mainMenuBtn;
	public GUIStyle myStyle;

	void Start() {
		StartCoroutine(MoveEnemies());

		CountColumns();
	}

	IEnumerator MoveEnemies() {
		float x = 0f;
		float y = 0f;
		float i = 0f;

		while (i < 20) {
			//LEFT
			while (atLeftWall == false) {
				x = -distance;
				enemyHolder.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
				if (atRightWall == true) {
					atRightWall = false;
				}
				yield return new WaitForSeconds(waitTime);
			}
			x = 0f;

			//DOWN FROM LEFT 1
			y = -distance;
			enemyHolder.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
			yield return new WaitForSeconds(waitTime);
			y = 0f;

			//RIGHT
			x = distance;
			while (atRightWall == false) {
				enemyHolder.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
				if (atLeftWall == true) {
					atLeftWall = false;
				}
				yield return new WaitForSeconds(waitTime);
			}
			x = 0f;

			//DOWN FROM RIGHT 1
			y = -distance;
			enemyHolder.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
			yield return new WaitForSeconds(waitTime);
			y = 0f;

			i++;
		}
	}

	private void FixedUpdate() {

		//*Enemy counting
		enemyCount = 0;
		for (int i = 0; i < transform.childCount; i++) {
			if (columns[i] != null) {
				enemyCount += columns[i].GetComponent<Transform>().childCount;
			}
		}
	}

	private void Update() {

		//*Enemy Acceleration
		if (enemyCount < oldEnemyCount && waitTime > 0.05f) {
			waitTime -= accelMultip;
		}
		Pause();
		oldEnemyCount = enemyCount;
		//*

		//Column counting when one column is destroyed
		if (columnMissing == true) {
			CountColumns();
			columnMissing = false;
		}
	}

	void Pause() {
		if (Input.GetKeyDown(KeyCode.P)) {
			if (pause == false) {
				Time.timeScale = 0;
				pausedText.SetActive(true);
				mainMenuBtn.SetActive(true);
				pause = true;
			} else {
				Time.timeScale = 1;
				pausedText.SetActive(false);
				mainMenuBtn.SetActive(false);
				pause = false;
			}
		}
	}

	void CountColumns() {
		columns = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			columns[i] = transform.GetChild(i).gameObject;
		}
	}
}

