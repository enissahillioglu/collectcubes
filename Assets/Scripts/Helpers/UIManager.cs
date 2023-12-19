using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    LevelType currentLevelType;
    
    [Header("Main")]
    public GameObject panelMain;
    public TextMeshProUGUI levelText;
    [Header("Gameplay")]
    public GameObject panelGameplayDefault;
    public TextMeshProUGUI gameplayDefaultLv, gameplayDefaultLvNext;
    public GameObject panelGameplayChallenge;
    public GameObject panelGameplayWithAI;
    [Header("Win")]
    public GameObject panelWinDefault;
    public GameObject panelWinChallenge;
    public GameObject panelWinWithAI;

    [Header("Fail")]
    public GameObject panelFail;

    [Header("Bars")]
    public Slider defaultGameplayBar;
    public Slider aiGameplayBar;
    public TextMeshProUGUI collectedCubeText;
    public TextMeshProUGUI collectedTimerText;
    public TextMeshProUGUI collectedCubeTotalText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        LevelManager.OnLevelStart += OnLevelStart;
        LevelManager.OnLevelComplete += OnLevelComplete;
        LevelManager.OnLevelFail += OnLevelFail;
        LevelManager.OnLevelInit += OnLevelInit;


    }

    private void OnDisable()
    {
        LevelManager.OnLevelStart -= OnLevelStart;
        LevelManager.OnLevelComplete -= OnLevelComplete;
        LevelManager.OnLevelFail -= OnLevelFail;
        LevelManager.OnLevelInit -= OnLevelInit;
    }

    private void Start()
    {
        currentLevelType=LevelManager.getCurrentLevelData().levelType;
    }

    void OnLevelInit(LevelContainer.LevelData _level)
    {
        OpenMainPanel();
        levelText.text = "Level "+PrefManager.level;
        gameplayDefaultLv.text = PrefManager.level.ToString();
        gameplayDefaultLvNext.text = (PrefManager.level+1).ToString();
    }

    void OnLevelStart(LevelContainer.LevelData _level)
    {
        OpenGameplayPanel();
    }

     void OnLevelComplete(int _collectedCubes)
    {
        StartCoroutine(AA());
        IEnumerator AA()
        {
            FXManager.Instance.levelWinParticle.Play();
            yield return new WaitForSeconds(1);
            OpenWinPanel();
        }
       
    }

     void OnLevelFail()
    {
        OpenFailPanel();
    }

    public void OpenMainPanel()
    {
        panelMain.SetActive(true);
        
    }

    public void OpenGameplayPanel()
    {
        panelMain.SetActive(false);
        if (currentLevelType == LevelType.STANDART)
        {
            panelGameplayDefault.SetActive(true);
        }else if (currentLevelType == LevelType.TIME_CHALLENGE)
        {
            panelGameplayChallenge.SetActive(true);
        }else if (currentLevelType == LevelType.AI_PLAYER)
        {
            panelGameplayWithAI.SetActive(true);
        }
    }

    public void OpenWinPanel( )
    {
        if (currentLevelType == LevelType.STANDART)
        {
            panelGameplayDefault.SetActive(false);
            panelWinDefault.SetActive(true);

            StartCoroutine(AA());
            IEnumerator AA()
            {
               
                yield return new WaitForSeconds(1.6f);
                ContinueBT();
            }
        }
        else if (currentLevelType == LevelType.TIME_CHALLENGE)
        {
            panelGameplayChallenge.SetActive(false);
            panelWinChallenge.SetActive(true);
            float collected = 0;
            DOTween.To(() => collected, x => collected = x, GameManager.Instance.cubeCountMe, 1).SetDelay(0.15f)
                .OnUpdate(() => {
                    collectedCubeText.text = "Collected:" + ((int)collected);
                });
            StartCoroutine(AA());
            IEnumerator AA()
            {

                yield return new WaitForSeconds(3f);
                ContinueBT();
            }
        }
        else if (currentLevelType == LevelType.AI_PLAYER)
        {
            panelGameplayWithAI.SetActive(false);
            panelWinWithAI.SetActive(true);
            StartCoroutine(AA());
            IEnumerator AA()
            {

                yield return new WaitForSeconds(2f);
                ContinueBT();
            }
        }
    }

    public void OpenFailPanel()
    {
        if (currentLevelType == LevelType.STANDART)
        {
            panelGameplayDefault.SetActive(false);
            panelWinDefault.SetActive(true);
        }
        else if (currentLevelType == LevelType.TIME_CHALLENGE)
        {
            panelGameplayChallenge.SetActive(false);
            panelWinChallenge.SetActive(true);
        }
        else if (currentLevelType == LevelType.AI_PLAYER)
        {
            panelGameplayWithAI.SetActive(false);
            panelWinWithAI.SetActive(true);
        }
    }

    public void ContinueBT()
    {
        PrefManager.scene++;
        if (PrefManager.scene >= LevelManager.Instance.container.levels.Count)
            PrefManager.scene = 0;
        PrefManager.level++;
        SceneManager.LoadScene(0);
    }

    public void SkipBT()
    {
        ContinueBT();
    }
    public void RetryBT()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
