using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

   
    public CollectArea collectAreaPrefab;
    public int cubeCountMe, cubeCountAi;
    public GameObject cubeSpawnerPrefab;
    LevelType currentLevelType;
    protected override void OnAwake()
    {
        LevelManager.OnLevelStart += OnLevelStart;
        LevelManager.OnLevelComplete += OnLevelComplete;
        LevelManager.OnLevelFail += OnLevelFail;
        LevelManager.OnLevelInit += OnLevelInit;

        Application.targetFrameRate = 60;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLevelInit(LevelContainer.LevelData _level)
    {
        PlayerManager.instance.m_Player.transform.position = _level.level.myStartPos.position;
        PlayerManager.instance.m_Player.transform.rotation = _level.level.myStartPos.rotation;
        CollectArea colMe = Instantiate(collectAreaPrefab);
        colMe.myArea = true;
        colMe.transform.position = _level.level.myAreaPos.position;
        colMe.transform.rotation = _level.level.myAreaPos.rotation;
        if (_level.levelType == LevelType.STANDART)
        {

        }
        else if (_level.levelType == LevelType.AI_PLAYER)
        {
            PlayerManager.instance.ai_Player.gameObject.SetActive(true);
            PlayerManager.instance.ai_Player.transform.position = _level.level.aiStartPos.position;
            PlayerManager.instance.ai_Player.transform.rotation = _level.level.aiStartPos.rotation;

            CollectArea colAi = Instantiate(collectAreaPrefab);
            colAi.myArea = false;
            colAi.transform.position = _level.level.aiAreaPos.position;
            colAi.transform.rotation = _level.level.aiAreaPos.rotation;
        }
        else if (_level.levelType == LevelType.TIME_CHALLENGE)
        {
            
        }
        currentLevelType=_level.levelType;
    }

     void OnLevelStart(LevelContainer.LevelData _level)
    {
        
 
        if (_level.levelType == LevelType.STANDART)
        {

        }else if (_level.levelType == LevelType.AI_PLAYER)
        {
            
        }
        else if (_level.levelType == LevelType.TIME_CHALLENGE)
        {
            Instantiate(cubeSpawnerPrefab);
        }
    }

     void OnLevelComplete( int _collectedCubes)
    {
        
    }

     void OnLevelFail()
    {
        
    }

    public void OnCollectCube(bool _me)
    {
        if (_me)
        {
            cubeCountMe++;
        }
        else
        {
            cubeCountAi++;
        }
        DetectGameProgress();


    }

    private void DetectGameProgress()
    {
        if (currentLevelType == LevelType.STANDART)
        {
            float val= (float)cubeCountMe/(float)LevelManager.Instance.currentLevel.cubeCount;
            UIManager.Instance.defaultGameplayBar.value = val;
            if (val >= 1)
            {
                LevelManager.OnLevelCompleted(0);
            }
        }
        else if (currentLevelType == LevelType.AI_PLAYER)
        {
            float f =  (cubeCountMe - cubeCountAi);
            float val = Mathf.InverseLerp(-LevelManager.Instance.currentLevel.cubeCount, LevelManager.Instance.currentLevel.cubeCount, f);
            UIManager.Instance.aiGameplayBar.value = Mathf.Lerp(0,10,val);

            if (cubeCountMe + cubeCountAi >= LevelManager.Instance.currentLevel.cubeCount){
                LevelManager.OnLevelCompleted(0);
            }
        }
        else if (currentLevelType == LevelType.TIME_CHALLENGE)
        {

        }
    }

    
}
