using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject menu;

    public GameObject game;

    public GameObject pause;

    public GameObject over;

    public GameObject win;

    public GameObject lose;
    
    private void Awake()
    {
        Instance = this;

        Screen.orientation = ScreenOrientation.Landscape;

        Screen.autorotateToPortrait = false;

        Screen.autorotateToPortraitUpsideDown = false;
    }

    private void Start()
    {
        menu.SetActive(true);
        
        game.SetActive(false);
    }

    public void StartGame()
    {
        game.SetActive(true);
        
        menu.SetActive(false);
        
        Values.StartTimer(240);
        
        Values.ResetBarrie(PlayerPlanter.NowCountPlants);
    }
    
    public void SetPause(bool isActive)
    {
        pause.SetActive(isActive);

        Time.timeScale = isActive ? 0 : 1;
    }

    public void EndGame(bool isWin)
    {
        over.SetActive(true);
        
        win.SetActive(isWin);
        
        lose.SetActive(!isWin);
        
        SoundManage.Instance.GameOver(isWin);

        StartCoroutine(WaitLoadMenu());
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("game");
    }

    private IEnumerator WaitLoadMenu()
    {
        Time.timeScale = 0;
        
        yield return new WaitForSecondsRealtime(3);
        
        LoadMenu();
    }
}
