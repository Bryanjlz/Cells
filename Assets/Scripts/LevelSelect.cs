using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    private const float Y_START = 30f;
    private const float X_START = -340f;
    private const float BUTT_DIST = 85f;
    private const int ROW_COUNT = 2;
    private const int COLUMN_COUNT = 9;

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject hoverBubble;
    [SerializeField] Button leftArrow;
    [SerializeField] Button rightArrow;

    public static bool loaded;
    private List<GameObject> pages;
    private int currentPage;

    // Start is called before the first frame update
    void Start() {

        //Disable hover
        hoverBubble.SetActive(false);

        //Initialize stuff
        List<string> q = new List<string>();
        pages = new List<GameObject>();


        //Get numbered levels first
        print(SceneManager.sceneCountInBuildSettings);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
            string currentScene = SceneUtility.GetScenePathByBuildIndex(i);
            currentScene = currentScene.Substring(currentScene.LastIndexOf("/") + 1, currentScene.LastIndexOf(".") - currentScene.LastIndexOf("/") - 1);
            if (currentScene.ToLowerInvariant().Contains("level") && currentScene.ToLowerInvariant().Any(char.IsDigit)) {
                q.Add(currentScene);
            }
        }

        //Get rest of levels into queue
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
            string currentScene = SceneUtility.GetScenePathByBuildIndex(i);
            currentScene = currentScene.Substring(currentScene.LastIndexOf("/") + 1, currentScene.LastIndexOf(".") - currentScene.LastIndexOf("/") - 1);
            if (!currentScene.Equals("Level Select") && !currentScene.Equals("Test Level") && !currentScene.Equals("Level Template") && 
                !(currentScene.ToLowerInvariant().Contains("level") && currentScene.ToLowerInvariant().Any(char.IsDigit))) {
                q.Add(currentScene);
            }
        }

        //Process levels into buttons
        int k = 0;
        while(q.Count != 0) {
            GameObject page = new GameObject("page " + k);
            page.transform.SetParent(gameObject.transform.parent.GetChild(4));
            page.transform.localPosition = Vector2.zero;
            page.transform.localScale = Vector3.one * 0.875f;

            for (int i = 0; i < ROW_COUNT; i++) {
                for (int j = 0; j < COLUMN_COUNT; j++) {

                    //Check if done
                    if (q.Count == 0) {
                        break;
                    }

                    //Create button
                    GameObject currentButton = Instantiate(buttonPrefab);
                    currentButton.transform.SetParent(page.transform);
                    currentButton.name = q[0];
                    currentButton.transform.localPosition = new Vector2(X_START + j * BUTT_DIST, Y_START - i * BUTT_DIST);
                    currentButton.transform.localScale = Vector3.one;

                    //Calculate level number
                    int levelNum= k * 18 + i * 9 + j + 1;
                    string levelNumStr = levelNum.ToString();
                    if (levelNum < 10) {
                        levelNumStr = "0" + levelNumStr;
                    }
                    currentButton.GetComponentInChildren<TMP_Text>().text = levelNumStr;

                    //Give button hover reference
                    currentButton.GetComponent<LevelButton>().hoverBubble = hoverBubble;

                    //Remove from queue
                    q.RemoveAt(0);
                }
                //Check if done
                if (q.Count == 0) {
                    break;
                }
            }

            // if not first page, don't show
            if (k != 0) {
                page.SetActive(false);
            }

            //Increment page number
            k++;

            //Add page to list
            pages.Add(page);
        }

        //Update Arrows
        currentPage = 0;
        UpdateArrows();
    }

    public void LeftArrow () {
        pages[currentPage].SetActive(false);
        currentPage--;
        pages[currentPage].SetActive(true);
        UpdateArrows();
    }

    public void RightArrow () {
        pages[currentPage].SetActive(false);
        currentPage++;
        pages[currentPage].SetActive(true);
        UpdateArrows();
    }

    void UpdateArrows() {
        leftArrow.interactable = true;
        rightArrow.interactable = true;
        if (currentPage == 0) {
            leftArrow.interactable = false;
        }
        if(currentPage == pages.Count - 1) {
            rightArrow.interactable = false;
        }
    }


}
