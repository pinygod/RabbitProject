using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
   private void OnMouseUpAsButton() {
       GameObject.Find("AudioController").GetComponent<AudioSource>().Play();
       switch (gameObject.name) {
            case "CatchButton":
                SceneManager.LoadScene("RabbitCatchScene");
                break;
            case "CasinoButton":
                SceneManager.LoadScene("SlotScene");
                break;
            case "OptionsButton":
                SceneManager.LoadScene("DoubleScene");
                break;
            case "PlayButton":
                SceneManager.LoadScene("FarmScene");
                break;                                
       }
   }
}
