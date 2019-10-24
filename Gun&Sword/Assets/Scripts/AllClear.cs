using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AllClear : MonoBehaviour {


    public void GameStart()
    {
        GameManager.instance.stage = 1;
        SceneManager.LoadScene("Gun&Sword");
    }
    

    public void Quit()
    {
        Application.Quit();
    }
}
