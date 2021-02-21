using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	[SerializeField] CanvasGroup pauseScreen;
	[SerializeField] public static bool isPaused;

	//TItle stuff
	[SerializeField] TMP_Text titleText;
	[SerializeField] Image titleImage;
	private bool isFading;


	// Start is called before the first frame update
	void Start()
	{
		isPaused = false;
		Time.timeScale = 1;

		isFading = false;
		titleText.text = SceneManager.GetActiveScene().name.ToUpperInvariant();
		StartCoroutine(LevelTextFade());
	}

	// Update is called once per frame
	void Update()
	{
		if (isFading && titleImage.color.a > 0) {
			titleImage.color = new Color(1, 1, 1, titleImage.color.a - 1f/255f);
			titleText.color = new Color(0, 0, 0, titleImage.color.a - 1f / 255f);
			if (titleImage.color.a <= 0) {
				titleImage.gameObject.SetActive(false);
				titleText.gameObject.SetActive(false);
            }
        }

		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPaused = true;
			Time.timeScale = 0;
			pauseScreen.gameObject.SetActive(true);
        }
		if (Input.GetKeyDown(KeyCode.R)) {
			Restart();
        }
	}

	IEnumerator LevelTextFade() {
		yield return new WaitForSeconds(2);
		isFading = true;
		
	}

	public void RestartButton() {
		FindObjectOfType<AudioManager>().Play("Connect");
		Restart();
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Menu() {
		SceneManager.LoadScene("Scenes/Level Select");
	}

	public void Resume() {
		FindObjectOfType<AudioManager>().Play("Connect");
		isPaused = false;
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive(false);
    }

	public void NextLevel() {
		FindObjectOfType<AudioManager>().Play("Connect");
		//Temporary, decide later how to find next level depending on how we store level order
		if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		} else {
			SceneManager.LoadScene("Scenes/Level Select");
		}
	}
}
