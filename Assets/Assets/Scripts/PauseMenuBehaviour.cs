using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuBehaviour : MainMenuBehaviour {
	public static bool isPaused;
	public GameObject pauseMenu;


	// Use this for initialization
	public void Start () {
		isPaused = false;
		pauseMenu.SetActive (false);
	}
	
	// Update is called once per frame
	public void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			isPaused = !isPaused;
			Time.timeScale = (isPaused) ? 0 : 1;
			pauseMenu.SetActive (isPaused);
		}
	}

	public void ResumeGame(){
		isPaused = false;
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void RestartGame(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
