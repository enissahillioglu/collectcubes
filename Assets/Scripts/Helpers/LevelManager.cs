using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelContainer container;
    public Level currentLevel;
    public LevelContainer.LevelData currentLevelData;
    
    public int levelIndex
    {
        get
        {
            return PrefManager.scene;
        }
        set
        {
            PrefManager.scene= value;
        }
    }
    public static event Action<LevelContainer.LevelData> OnLevelInit;
    public static event Action<LevelContainer.LevelData> OnLevelStart;
    public static event Action< int> OnLevelComplete;
    public static event Action OnLevelFail;

    public bool levelStarted=false;

    public List<Transform> activeCubes = new List<Transform>();
    // Start is called before the first frame update
    protected override void OnAwake()
    {
       base.OnAwake();
        
    }
    private void Start()
    {
        CreateLevel(levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static LevelContainer.LevelData getCurrentLevelData()
    {
        return Instance.container.levels[Instance.levelIndex];
    }

    private static void CreateLevel(int index)
    {
        if (Instance.currentLevel)
        {
            Destroy(Instance.currentLevel.gameObject);
        }
        Instance.currentLevel = Instantiate(Instance.container.levels[index].level ,Instance.transform);
        Instance.currentLevel.Init(Instance.container.levels[index]);
        Instance.currentLevelData = Instance.container.levels[index];
        OnLevelInit?.Invoke(Instance.container.levels[Instance.levelIndex]);
        
    }
    public static void StartLevel ()
    {
        Instance.levelStarted = true;
        OnLevelStart?.Invoke(Instance.container.levels[Instance.levelIndex]);
    }

    public static void OnLevelCompleted( int _cubeGained)
    {
        Instance.levelStarted = false;
        OnLevelComplete?.Invoke( _cubeGained);
    }

    public static void OnLevelFailed()
    {
        OnLevelFail?.Invoke();
    }
}
