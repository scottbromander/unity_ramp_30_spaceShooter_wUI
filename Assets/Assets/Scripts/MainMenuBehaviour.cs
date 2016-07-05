using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour {

	public void LoadLevel(string levelname){
		Time.timeScale = 1;
		SceneManager.LoadScene (levelname);
	}

	public void QuitGame(){
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif	
	}
}
