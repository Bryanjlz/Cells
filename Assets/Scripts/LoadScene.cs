using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene: MonoBehaviour 
{
    public void Load(string name) {
        FindObjectOfType<AudioManager>().Play("Connect");
        SceneManager.LoadScene(name);
    }
    
    public void Load(int id) {
        FindObjectOfType<AudioManager>().Play("Connect");
        SceneManager.LoadScene(id);
    }
}