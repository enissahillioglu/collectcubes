using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    public float duration;
    float timer;
    float challengeTimer;
    public bool started;
    public Color[] colors;
    // Start is called before the first frame update
    void Start()
    {
        challengeTimer = LevelManager.Instance.currentLevelData.time;
    }

    private void OnEnable()
    {
       
        LevelManager.OnLevelComplete += OnLevelComplete;
        LevelManager.OnLevelFail += OnLevelFail;
        started = true;
    }
    private void OnDisable()
    {
        
        LevelManager.OnLevelComplete -= OnLevelComplete;
        LevelManager.OnLevelFail -= OnLevelFail;
    }
    void OnLevelStart(LevelContainer.LevelData _level) { 
        
    started = true;
    }

    void OnLevelComplete(int _collectedCubes) => started = false;

    public void OnLevelFail() => started = false;
    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.Instance.levelStarted) return;
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0;
            Spawn();
        }

        if (started)
        {
            challengeTimer -= Time.deltaTime;

            
            UIManager.Instance.collectedTimerText.text = challengeTimer.ToString("F1");
            UIManager.Instance.collectedCubeTotalText.text = GameManager.Instance.cubeCountMe.ToString(); 

            if (challengeTimer <= 0)
            {
                LevelManager.OnLevelCompleted(GameManager.Instance.cubeCountMe);
                
            }
        }

    }

    void Spawn()
    {
        CubePixel cube = PoolManager.Instance.poolingCubePixel.GetObjectFromPool();
        Vector3 ps = transform.position;
        ps.x = Random.Range(transform.position.x + .4f, transform.position.x - .4f);
        ps.z = Random.Range(transform.position.z + .4f, transform.position.z - .4f);
        cube.Init(ps, colors[Random.Range(0,colors.Length)]);
      
        
    }


   
}
