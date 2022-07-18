using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScriptCanvas : MonoBehaviour
{
    public GameObject levelMenuPrefab;
    public GameObject mainMenuPrefab;
    public GameObject optionsMenuPrefab;
    public GameObject allMenus;
    public GameObject self;
    public string level1;
    public string level2;
    public string level3;
    public string level4;

    public void OnCancel()
    {
        if (allMenus.active)
        {
            allMenus.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            allMenus.SetActive(true);
            Time.timeScale =0;
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        self = gameObject;
        if (allMenus != null)
        {
            allMenus.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void beginPlay()
    {
        mainMenuPrefab.SetActive(false);
        levelMenuPrefab.SetActive(true);
        playAudio();
    }
    public void options()
    { 
        if (optionsMenuPrefab != null)
        {
            mainMenuPrefab.SetActive(false);
            optionsMenuPrefab.SetActive(true);
            playAudio();
        }
        playAudio();
    }
    public void backOptsToMenu()
    {
        mainMenuPrefab.SetActive(true);
        optionsMenuPrefab.SetActive(false);
        playAudio();
    }
    public void backLevelsToMenu()
    {
        levelMenuPrefab.SetActive(false);
        mainMenuPrefab.SetActive(true);
        playAudio();
    }
    public void closeGame()
    {
        Application.Quit();
    }

    public void levelOne()
    {
        SceneManager.LoadScene(level1);

    }
    public void levelTwo()
    {
        SceneManager.LoadScene(level2);
    }
    public void levelThree()
    {
        SceneManager.LoadScene(level3);
    }
    public void levelFour()
    {
        SceneManager.LoadScene(level4);
    }
    
    public void playAudio()
    {
        GetComponent<AudioSource>().pitch = Random.Range(1, 1.5f);
        GetComponent<AudioSource>().Play();
    }
    public void resume()
    {
        allMenus.SetActive(false);
        playAudio();
        Time.timeScale = 1;
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
