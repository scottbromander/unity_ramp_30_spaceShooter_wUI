using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuBehaviour : MainMenuBehaviour {
	public static bool isPaused;
	public GameObject pauseMenu;
	public GameObject optionsMenu;


	// Use this for initialization
	public void Start () {
		isPaused = false;
		pauseMenu.SetActive (false);
		optionsMenu.SetActive (false);

		UpdateQualityLabel ();
		UpdateVolumeLabel ();
	}
	
	// Update is called once per frame
	public void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (!optionsMenu.activeInHierarchy) {
				isPaused = !isPaused;
				Time.timeScale = (isPaused) ? 0 : 1;
				pauseMenu.SetActive (isPaused);
			} else {
				OpenOptions ();
			}
		}
	}

	public void ResumeGame(){
		isPaused = false;
		pauseMenu.SetActive (false);
		optionsMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void RestartGame(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void IncreaseQuality(){
		QualitySettings.IncreaseLevel ();
		UpdateQualityLabel ();
	}

	public void DecreaseQuality(){
		QualitySettings.DecreaseLevel ();
		UpdateQualityLabel ();
	}

	public void SetVolume(float value){
		AudioListener.volume = value;
		UpdateVolumeLabel ();
	}

	private void UpdateQualityLabel(){
		int currentQuality = QualitySettings.GetQualityLevel ();
		string qualityName = QualitySettings.names [currentQuality];

		optionsMenu.transform.FindChild ("Quality Level").GetComponent<UnityEngine.UI.Text> ().text = "Quality Level: " + qualityName;
	}

	private void UpdateVolumeLabel(){
		optionsMenu.transform.FindChild ("Master Volume").GetComponent<UnityEngine.UI.Text> ().text = "Master Volume: " + (AudioListener.volume * 100).ToString ("f2") + "%";
	}

	public void OpenOptions(){
		optionsMenu.SetActive (true);
		pauseMenu.SetActive (false);
	}

	public void OpenPauseMenu(){
		optionsMenu.SetActive (false);
		pauseMenu.SetActive (true);
	}
}
