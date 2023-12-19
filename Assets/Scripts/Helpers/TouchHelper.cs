using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHelper : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static TouchHelper instance;

    public delegate void Rotate(float angle);
    public event Rotate OnRotate;

    public delegate void Touch(bool isTouch,Vector3 direction);
    public event Touch OnTouch; 
    bool dragging = false; 
    Vector3 initialMousePosition;
    Vector3 initialObjectPosition;
    public Vector3 lastObjectPosition;
    
    Vector3 currentMousePosition;

    public bool lookAtMode = false;
    public float sensv;

    bool once;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!LevelManager.Instance.levelStarted) return;
        if (dragging)
        {
            
            currentMousePosition.z = lastObjectPosition.y;

            Vector3 offset = currentMousePosition - initialMousePosition;
            Vector3 newPosition = initialObjectPosition + offset * (1f / sensv);

            Vector3 lastPos = new Vector3(newPosition.x, lastObjectPosition.y, newPosition.y);
            
            if (lookAtMode)
            {
                Vector3 lookDir = (lastPos - lastObjectPosition).normalized;
                if (lookDir != Vector3.zero)
                {
                    float angle = Vector3.SignedAngle(Vector3.forward, lookDir, Vector3.up) * 1f;
                    if (OnRotate != null)
                        OnRotate(angle);
                     
                }
            }
             

            if (OnTouch != null)
                OnTouch(true,lastPos);
          
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (!LevelManager.Instance.levelStarted && !once)
        {
            LevelManager.StartLevel();
            once = true;
        }

        initialMousePosition = eventData.position;
        initialMousePosition.z = lastObjectPosition.y;
        initialObjectPosition = lastObjectPosition;
        currentMousePosition = eventData.position;

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        initialObjectPosition = lastObjectPosition;

        dragging = false;

       
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragging = true;
        currentMousePosition = eventData.position;
    }

}
