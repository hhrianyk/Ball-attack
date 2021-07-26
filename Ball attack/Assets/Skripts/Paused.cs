using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{
    public GameObject pauseMenu;



    // Start is called before the first frame update
    void Start()
    {
      //  pauseMenu.SetActive(false);
    }

    public void PauseOn()
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
         
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

}
