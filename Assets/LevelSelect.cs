using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    private const float Y_START = 30f;
    private const float X_START = -340f;
    private const float BUTT_DIST = 85f;
    private const int ROW_COUNT = 2;
    private const int COLUMN_COUNT = 9;

    [SerializeField] GameObject buttonPrefab;

    private List<GameObject[][]> buttons;
    // Start is called before the first frame update
    void Start()
    {
        List<string> q = new List<string>();
        buttons = new List<GameObject[][]>();

        //Load files
        DirectoryInfo dir = new DirectoryInfo("Assets/Scenes/Levels/");
        FileInfo[] info = dir.GetFiles("*.unity");

        //Get numbered levels first
        for (int i = 0; i < info.Length; i++) {
            if (info[i].Name.ToLowerInvariant().Contains("level") && info[i].Name.ToLowerInvariant().Any(char.IsDigit)) {
                q.Add(info[i].Name);
                info[i] = null;
            }
        }

        //Get rest of levels into queue
        for (int i = 0; i < info.Length; i++) {
            if (info[i] != null && !info[i].Name.Equals("Test Level.unity") && !info[i].Name.Equals("Level Template.unity")) {
                q.Add(info[i].Name);
            }
        }

        //Process levels into buttons
        int k = 0;
        while(q.Count != 0) {
            print(q[0]);
            GameObject pageParent = new GameObject("page " + k);
            pageParent.transform.SetParent(gameObject.transform.parent);
            pageParent.transform.localPosition = Vector2.zero;
            GameObject[][] page = new GameObject[ROW_COUNT][];
            for (int i = 0; i < page.Length; i++) {
                page[i] = new GameObject[COLUMN_COUNT];
            }

            for (int i = 0; i < page.Length; i++) {
                for (int j = 0; j < page[0].Length; j++) {
                    //Check if done
                    if (q.Count == 0) {
                        break;
                    }

                    //Create button
                    page[i][j] = Instantiate(buttonPrefab);
                    page[i][j].transform.SetParent(pageParent.transform);
                    page[i][j].name = q[0].Substring(0, q[0].LastIndexOf(".unity"));
                    page[i][j].transform.localPosition = new Vector2(X_START + j * BUTT_DIST, Y_START - i * BUTT_DIST);

                    //Calculate level number
                    int levelNum= k * 18 + i * 9 + j + 1;
                    string levelNumStr = levelNum.ToString();
                    if (levelNum < 10) {
                        levelNumStr = "0" + levelNumStr;
                    }
                    page[i][j].GetComponentInChildren<TMP_Text>().text = levelNumStr;
                    q.RemoveAt(0);

                }
                //Check if done
                if (q.Count == 0) {
                    break;
                }
            }
            k++;
            buttons.Add(page);
        }

    }
}
