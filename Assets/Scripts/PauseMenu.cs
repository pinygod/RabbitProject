using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, IPointerClickHandler
{
    public GameObject MenuPanel;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("AudioController").GetComponent<AudioSource>().Play();
        switch (gameObject.name)
        {
            case "MenuButton":
                MenuPanel.SetActive(true);
                Time.timeScale = 0f;
                break;
            case "ResumeButton":
                MenuPanel.SetActive(false);
                Time.timeScale = 1f;
                break;
            case "QuitButton":
                SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1f;
                break;

        }

    }
}

