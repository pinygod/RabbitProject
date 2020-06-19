using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrientationScript : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "FarmScene")
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

    }
}
