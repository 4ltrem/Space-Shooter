using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void LoadSinglePlayer()
    {
        Debug.Log("Sinleplayer Loading...");
        //load scene 1 -- single player scene
        SceneManager.LoadScene("Singleplayer");
    }

    public void LoadCoopMode()
    {
        Debug.Log("Coop mode Loading...");
        //load scene 2 -- coop mode scene
        SceneManager.LoadScene("Coop_Mode");
    }
}
