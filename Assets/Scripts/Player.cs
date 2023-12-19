using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public bool isAi;

    public float moveSpeed = 5f;
    public float rotateSpeed = 50f;

    private bool isTouching = false;
    public Vector3 moveDirection;
    private float rotationAngle;
    Rigidbody rb;
    int myLayer, myCubesLayer;
    private void Start()
    {
        if (!isAi)
        {
            TouchHelper.instance.OnRotate += RotatePlayer;
            TouchHelper.instance.OnTouch += MovePlayer;
            myLayer = GameConst.layerPlayer;
            myCubesLayer = GameConst.layerMyCubes;
        }
        else
        {
            myLayer = GameConst.layerAi;
            myCubesLayer = GameConst.layerAiCubes;
        }

       

        rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        if (!isAi)
        {

            TouchHelper.instance.OnRotate -= RotatePlayer;
            TouchHelper.instance.OnTouch -= MovePlayer;
        }

    }

    public void MovePlayer(bool isTouch, Vector3 direction)
    {
        isTouching = isTouch;
        moveDirection = new Vector3(direction.x, transform.position.y, direction.z);


    }

    public void MoveAI(float _aimoveSpeed)
    {
        
        rb.velocity = transform.forward * _aimoveSpeed;
    }


    public void RotatePlayer(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 50f);
    }

    void FixedUpdate()
    {
        if (isAi) return;
        if (!isAi)
        {
            TouchHelper.instance.lastObjectPosition = new Vector3(transform.position.x, transform.position.z, transform.position.z);
        }

        if (isTouching)
        {


            {
                Vector3 vel = ((moveDirection - transform.position));
                vel.y = 0;
                // rb.MovePosition(Vector3.Lerp(transform.position, moveDirection, Time.deltaTime * moveSpeed);
                rb.velocity = vel * moveSpeed;
                // transform.position = Vector3.Lerp(transform.position, moveDirection, Time.deltaTime * moveSpeed);
            }

        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity =Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameConst.layerCube)
        {
           
                other.gameObject.layer = myCubesLayer;
            LevelManager.Instance.activeCubes.Remove(other.transform);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == myCubesLayer)
        {
            other.gameObject.layer = GameConst.layerCube;
            LevelManager.Instance.activeCubes.Add(other.transform);
        }
    }

}
    
