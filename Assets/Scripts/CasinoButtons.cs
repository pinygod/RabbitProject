using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CasinoButtons : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("AudioController").GetComponent<AudioSource>().Play();
        switch (gameObject.name)
        {
            case "DoubleButton":
                SceneManager.LoadScene("DoubleScene");
                break;
            case "SlotButton":
                SceneManager.LoadScene("SlotScene");
                break;
            case "ShellButton":
                SceneManager.LoadScene("ShellGame");
                break;
        }

    }
}
