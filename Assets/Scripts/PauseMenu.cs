using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuPanel;
    private void OnMouseUpAsButton() {
        switch (gameObject.name) {
            case "MenuButton":
                //EditorApplication.isPaused = true;
                Time.timeScale = 0f;
                MenuPanel.SetActive(true);
                break;
            case "ResumeButton":
                //EditorApplication.isPaused = false;
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
