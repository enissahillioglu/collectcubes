using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiManager : Singleton<LevelManager>
{
    NavMeshSurface navMeshSurface;
    protected override void OnAwake()
    {
        base.OnAwake();
        navMeshSurface=GetComponent<NavMeshSurface>();


    }
    private void OnEnable()
    {
        LevelManager.OnLevelInit += OnLevelInit;
    }
    private void OnDisable()
    {
        LevelManager.OnLevelInit -= OnLevelInit;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnLevelInit(LevelContainer.LevelData _level)
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
