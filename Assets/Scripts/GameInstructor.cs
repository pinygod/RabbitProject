using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstructor : MonoBehaviour
{
    public GameObject GameInstructorPanel;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "GI"))
        {
            GameInstructorPanel.SetActive(true);
        }
    }

    public void DisableGI(){
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "GI", "disabled");
    }

}
