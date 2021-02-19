using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
	[SerializeField] CanvasGroup pauseScreen;
	[SerializeField] public static bool isPaused;
	// Start is called before the first frame update
	void Start()
	{
		isPaused = false;
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPaused = true;
			Time.timeScale = 0;
			pauseScreen.gameObject.SetActive(true);
        }
		if (Input.GetKeyDown(KeyCode.R)) {
			Restart();
        }
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void Menu() {
		SceneManager.LoadScene("Scenes/Menu");
	}

	public void Resume() {
		isPaused = false;
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive(false);
    }
}
