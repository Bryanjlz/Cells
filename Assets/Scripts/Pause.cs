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
	[SerializeField] TMP_Text warningText;
	[SerializeField] Image warningImage;
	private bool isFading;

	private bool isHell;


	// Start is called before the first frame update
	void Start()
	{
		isPaused = false;
		Time.timeScale = 1;

		isHell = false;
		isFading = false;
		if (titleText != null) {
			titleText.text = SceneManager.GetActiveScene().name.ToUpperInvariant();
		}

		if (SceneManager.GetActiveScene().buildIndex > SceneManager.sceneCountInBuildSettings - 3) {
			warningImage.gameObject.SetActive(true);
			isHell = true;

		}

		StartCoroutine(LevelTextFade());
	}

	// Update is called once per frame
	void Update()
	{
		if (titleImage != null && isFading && titleImage.color.a > 0) {
			titleImage.color = new Color(1, 1, 1, titleImage.color.a - 1f/255f);
			titleText.color = new Color(0, 0, 0, titleImage.color.a - 1f / 255f);
			
			if (isHell) {
				warningImage.color = new Color(1, 1, 1, titleImage.color.a - 1f / 255f);
				warningText.color = new Color(0, 0, 0, titleImage.color.a - 1f / 255f);
			}

			if (titleImage.color.a <= 0) {
				titleImage.gameObject.SetActive(false);
				titleText.gameObject.SetActive(false);
				if (isHell) {
					warningImage.gameObject.SetActive(false);
					warningText.gameObject.SetActive(false);
				}
			}
        }

		if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Backstory") {
			if (isPaused) {
				Resume();
            } else {
				isPaused = true;
				Time.timeScale = 0;
				pauseScreen.gameObject.SetActive(true);
			}
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
		FindObjectOfType<AudioManager>().Play("Connect");
		SceneManager.LoadScene("Scenes/Level Select");
	}

	public void ResumeButton() {
		FindObjectOfType<AudioManager>().Play("Connect");
		Resume();
	}

	public void Resume() {
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
