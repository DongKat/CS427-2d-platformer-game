using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI startText;

    public GameManager gameManager;

    [Header("Menu Groups")]
    public GameObject start;
    public GameObject menu;
    public GameObject settings;

    [Header("Settings")]
    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI sfxText;
    public TextMeshProUGUI bgmTextCounter;
    public TextMeshProUGUI sfxTextCounter;

    private GameObject currentMenu;

    void Start()
    {
        gameManager = GameManager.instance;
        StartCoroutine("blinkStart");
    }

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }

    private IEnumerator blinkStart()
    {
        while (true)
        {
            while (startText.alpha > 0f)
            {
                startText.alpha -= 0.1f;
                yield return new WaitForSecondsRealtime(0.05f);
            }
            while (startText.alpha < 1f)
            {
                startText.alpha += 0.1f;
                yield return new WaitForSecondsRealtime(0.05f);
            }
        }
    }

    public void Back()
    {
        currentMenu.SetActive(false);

        if (currentMenu == settings)
        {
            menu.gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    public void PressStart()
    {
        menu.gameObject.SetActive(true);
        start.gameObject.SetActive(false);
        StopCoroutine("blinkStart");

        //Set the clip for effect audio
        AudioManager.PlayMenuSelect();

        //Set the clip for music audio, tell it to loop, and then tell it to play
    }

    public void PressStartGame()
    {
        //Set the clip for effect audio
        AudioManager.PlayMenuSelect();
        
        startMission();

    }

    public void PressSettings()
    {
        RefreshAudioText();
        settings.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
        currentMenu = settings;

        //Set the clip for effect audio
        AudioManager.PlayMenuSelect();
    }

    public void PressQuit()
    {
        //Set the clip for effect audio
        AudioManager.PlayMenuSelect();

        Application.Quit();
    }

    public void startMission()
    {
        SceneManager.LoadScene(1); // Load level 1 scene
    }

    /* Start settings */
    public void SetBgmCounterPressed()
    {
        //Click color
        bgmText.color = new Color32(255, 255, 255, 255);
        sfxText.color = new Color32(255, 141, 0, 255);
        bgmTextCounter.color = new Color32(255, 255, 255, 255);
        sfxTextCounter.color = new Color32(255, 141, 0, 255);

        float cnt = gameManager.bgmVolume;
        cnt += .1f;
        if (cnt >= 1.1f)
            cnt = 0f;
        gameManager.bgmVolume = cnt;

        AudioManager.RefreshAudioVolume();
        RefreshAudioText();
    }

    public void SetSfxCounterPressed()
    {
        //Click color
        bgmText.color = new Color32(255, 141, 0, 255);
        sfxText.color = new Color32(255, 255, 255, 255);
        bgmTextCounter.color = new Color32(255, 141, 0, 255);
        sfxTextCounter.color = new Color32(255, 255, 255, 255);

        float cnt = gameManager.sfxVolume;
        cnt += .1f;
        if (cnt >= 1.1f)
            cnt = 0f;
        gameManager.sfxVolume = cnt;

        AudioManager.RefreshAudioVolume();
        RefreshAudioText();
    }

    void RefreshAudioText()
    {
        float bgmCnt = gameManager.bgmVolume;
        if (bgmCnt < 0.1f)
            bgmTextCounter.SetText("OFF");
        else
            bgmTextCounter.SetText(Math.Round(bgmCnt * 10).ToString());

        float sfxCnt = gameManager.sfxVolume;
        if (sfxCnt < 0.1f)
            sfxTextCounter.SetText("OFF");
        else
            sfxTextCounter.SetText(Math.Round(sfxCnt * 10).ToString());
    }
    /* End settings */
}
