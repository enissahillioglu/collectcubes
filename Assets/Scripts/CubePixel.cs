using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePixel : MonoBehaviour
{
    Rigidbody rb;
    bool isCollected;
    Vector3 lastPos;
    Vector3 startScale;
    ParticleSystem ps;
    public Renderer mRenderer;
    private void Awake()
    {
        rb=GetComponent<Rigidbody>();
        startScale = transform.localScale;


    }

    public void Init(Vector3 pos, Color color,bool isEditor=true)
    {
        
        if (mRenderer == null)
        {
            mRenderer = GetComponent<MeshRenderer>();
        }
        if (isEditor)
        {
            var tempMaterial = new Material(mRenderer.sharedMaterial);
            tempMaterial.SetColor("_Color", color);
            mRenderer.sharedMaterial = tempMaterial;
        }else
        {
            mRenderer.material.SetColor("_Color", color);
        }
       
       
        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        // rb.velocity = new Vector3 (Mathf.Clamp(rb.velocity.x,-4,4), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -4, 4));
       
        if (isCollected)
        {
            if(transform.localScale.x >= 0.1f)
            {
                transform.localScale -= Vector3.one * Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position,lastPos,Time.deltaTime*20);
            }else
            {
                Restart();
               
            }
        }else
        {
            rb.maxDepenetrationVelocity = 1;
            
        }

       



    }

    private void Restart()
    {
        isCollected = false;
         transform.localScale= startScale ;
        rb.isKinematic = false;
        gameObject.layer = GameConst.layerDefaut;
       
       
         PoolManager.Instance.pooligCubeParticle.ReturnObjectToPool(ps);

        gameObject.SetActive(false);
    }

    public void Collected(bool isMe, Vector3 _lastPos)
    {
       
        lastPos = _lastPos;
        LevelManager.Instance.activeCubes.Remove(transform);
        StartCoroutine(AA());
        IEnumerator AA()
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 0.7f));
            ps = PoolManager.Instance.pooligCubeParticle.GetObjectFromPool();
            ps.transform.position = transform.position;
            ps.gameObject.SetActive(true);
            ps.Play();
            rb.isKinematic = true;
            isCollected = true;
        }
        if (isMe)
        {
            GameManager.Instance.OnCollectCube(true);
        }else
        {
            GameManager.Instance.OnCollectCube(false);
        }
    }
}
