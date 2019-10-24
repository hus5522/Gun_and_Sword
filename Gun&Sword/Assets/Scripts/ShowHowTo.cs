using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ShowHowTo : MonoBehaviour {
    
    [SerializeField]
    private GameObject howToPlay;

    [SerializeField]
    private GameObject startBtn;
    [SerializeField]
    private GameObject howtoBtn;
    [SerializeField]
    private GameObject exitBtn;

    void Start()
    {
        howToPlay.SetActive(false);
    }


    public void GameStart()
    {
        GameManager.instance.stage = 1;
        SceneManager.LoadScene("Stage1");
    }

    public void ShowHowToPlay()
    {
        howToPlay.SetActive(true);
        startBtn.SetActive(false);
        howtoBtn.SetActive(false);
        exitBtn.SetActive(false);
    }

    public void DeleteHowTo()
    {
        howToPlay.SetActive(false);
        startBtn.SetActive(true);
        howtoBtn.SetActive(true);
        exitBtn.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
