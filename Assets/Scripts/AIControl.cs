using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    Player myPlayer;
    public NavMeshAgent agent;
    Vector3 aiAreaPos;  
    float timeToChangeDirection = 0.5f; 
    float timer = 0f;
    public bool isMovingToCubes = true;  
    public bool isReachedAiAreaPos = false;  

    float timeToEmptyCubes = 0.2f; // Küpleri boþaltma süresi
    float emptyCubesTimer = 0f;

    public float randomSpeed;

    float maxSpeed;
    float dirRate;

    private void OnEnable()
    {
        LevelManager.OnLevelStart += OnLevelStart;
    }
    private void OnDisable()
    {
        LevelManager.OnLevelStart -= OnLevelStart;
    }
    private void Awake()
    {
        myPlayer = GetComponent<Player>();
        agent.transform.parent = null;
        
    }

    void Start()
    {
        agent.updatePosition = true;
        agent.stoppingDistance = 2.1f;  
        ChangeDirection();  
    }
    void OnLevelStart(LevelContainer.LevelData _level)
    {
        if (_level.aiPower == 1)
        {
            dirRate = 0.2f;
            maxSpeed = 6;
        }
           
        else if (_level.aiPower == 2)
        {
            dirRate = 0.3f;
            maxSpeed = 10;
        }
           
        else if (_level.aiPower == 3)
        {
            dirRate = 0.4f;
            maxSpeed = 25;
        }
           

        randomSpeed = Random.Range(6, maxSpeed);
       
    }

    private void Update()
    {
        if (!LevelManager.Instance.levelStarted) return;
        timer += Time.deltaTime;

        if (isMovingToCubes)
        {
            if (timer >= timeToChangeDirection)
            {
                if (Random.value < dirRate)
                    isMovingToCubes = false;
                else isMovingToCubes = true;
                ChangeDirection();
                timer = 0f;
            }
        }
        else
        {
            
        }
    }

    private void FixedUpdate()
    {
        if (!LevelManager.Instance.levelStarted) return;
        if (isMovingToCubes)
        {
            agent.SetDestination(aiAreaPos);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                
            }
        }
        if (isReachedAiAreaPos)
        {
            
            agent.SetDestination(aiAreaPos);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                isMovingToCubes = true;
                isReachedAiAreaPos = false;
                
                ChangeDirection();
            }
        }

        if (agent.remainingDistance > 1f)
        {
            myPlayer.MoveAI(randomSpeed);

        }
        else
        {
            myPlayer.MoveAI( 0);
        }
        myPlayer.RotatePlayer(agent.transform.eulerAngles.y);
    }

    void ChangeDirection()
    {
        if (isMovingToCubes)
        {
             
            if (LevelManager.Instance.activeCubes.Count > 0)
            {
                int randomIndex = Random.Range(0, LevelManager.Instance.activeCubes.Count);
                Vector3 pos = LevelManager.Instance.activeCubes[randomIndex].position;
                pos.y=agent.transform.position.y;
                aiAreaPos = pos;

              
                float randomOffset = Random.Range(-2f, 2f);
                aiAreaPos += transform.right * randomOffset;
                randomSpeed = Random.Range(6, maxSpeed);
                isReachedAiAreaPos = false;  
            }
        }
        else
        {
            
            randomSpeed = Random.Range(6, maxSpeed);
            aiAreaPos = new Vector3(LevelManager.Instance.currentLevel.aiAreaPos.position.x, agent.transform.position.y, LevelManager.Instance.currentLevel.aiAreaPos.position.z) ;
            isReachedAiAreaPos = true;  
        }

       
        
    }
    void LateUpdate()
    {
        agent.transform.position = transform.position;
    }
}
