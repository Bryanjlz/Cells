using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        DirectoryInfo dir = new DirectoryInfo("Assets/Scenes/Levels/");
        FileInfo[] info = dir.GetFiles("*.unity");
        for (int i = 0; i < info.Length; i++) {
            if (info[i].Name.ToLowerInvariant().Contains("level") && info[i].Name.ToLowerInvariant().Any(char.IsDigit)) {
                q.Add(info[i].Name);
                info[i] = null;
            }
        }
        for (int i = 0; i < info.Length; i++) {
            if (info[i] != null && !info[i].Name.Equals("Test Level") && !info[i].Name.Equals("Level Template")) {
                q.Add(info[i].Name);
            }
        }

        while(q.Count != 0) {
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

                    
                }
                //Check if done
                if (q.Count == 0) {
                    break;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
