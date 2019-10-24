using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {
            PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0; // 일시정지
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1; // 원상복귀
        }
    }

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        /*
        if(GameManager.instance.stage == 1)
            SceneManager.LoadScene("Stage1");

        if (GameManager.instance.stage == 2)
            SceneManager.LoadScene("Stage2");

        if (GameManager.instance.stage == 3)
            SceneManager.LoadScene("Stage3");
            */
        if (Player.instance.CurrentStage == 0)
            SceneManager.LoadScene("Stage1");

        if (Player.instance.CurrentStage == 1)
            SceneManager.LoadScene("Stage2");

        if (Player.instance.CurrentStage == 2)
            SceneManager.LoadScene("Stage3");

    }

    public void MainMenu()
    {
        Player.instance.CurrentStage = 0;
        SceneManager.LoadScene("Gun&Sword");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
