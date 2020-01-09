using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour {

    public void Replay()
    {
        //Application.LoadLevel("遊戲");     //舊版 API
        SceneManager.LoadScene(0);    //新版 API
    }

    public void Quit()
    {
        Application.Quit(); // 應用程式.離開遊戲
    }
}
