using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Destroyed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<CubePixel>())
        {
            if (LevelManager.Instance.activeCubes.Contains(collision.transform))
            {
                LevelManager.Instance.activeCubes.Remove(collision.transform);
            }
            Destroy(collision.transform.gameObject);
            LevelManager.Instance.currentLevel.cubeCount--;
            
        }
    }
}
