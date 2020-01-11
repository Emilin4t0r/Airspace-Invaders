using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {

	public GameObject help_menu;
	public GameObject level_menu;
	public GameObject back_button;
	public AudioSource button_sfx;

	private void Start() {
		button_sfx = GetComponent<AudioSource>();
	}

	private void Update() {
		if (help_menu.activeSelf == false || level_menu.activeSelf == false) {
			back_button.SetActive(false);
		}
		if (help_menu.activeSelf == true || level_menu.activeSelf == true) {
			back_button.SetActive(true);
		}
	}

	public void LoadLevel01() {
		SceneManager.LoadScene("Level01");
	}
	public void LoadLevel02() {
		SceneManager.LoadScene("Level02");
	}
	public void LoadLevel03() {
		SceneManager.LoadScene("Level03");
	}

	public void GoBack() {
		level_menu.SetActive(false);
		help_menu.SetActive(false);
		button_sfx.Play();
	}
	public void OpenHelpMenu() {
		help_menu.SetActive(true);
		button_sfx.Play();
	}
	public void OpenLevelMenu() {
		level_menu.SetActive(true);
		button_sfx.Play();
	}

	public void QuitGame() {
		Application.Quit();
	}
}
