using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public GameObject hoverBubble;

    public void StartLevel () {
        FindObjectOfType<AudioManager>().Play("Connect");
        SceneManager.LoadScene(gameObject.name);
    }

    public void StartHover() {
        hoverBubble.GetComponentInChildren<TMP_Text>().text = gameObject.name.ToUpperInvariant();
        hoverBubble.SetActive(true);
    }

    public void StopHover() {
        hoverBubble.SetActive(false);
    }
}
