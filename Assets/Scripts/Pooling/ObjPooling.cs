using System.Collections.Generic;
using UnityEngine;

public class ObjPooling<T> where T : Component
{
    private T prefab;
    private int poolSize;
    private Transform parent;
    private Queue<T> objectQueue = new Queue<T>();

    public ObjPooling(T _prefab, int _poolSize, Transform _parent)
    {
        poolSize = _poolSize;
        parent = _parent;
        prefab = _prefab;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            T newObj = Object.Instantiate(prefab, parent);
            newObj.gameObject.SetActive(false);
            objectQueue.Enqueue(newObj);
        }
    }

    public T GetObjectFromPool()
    {
        if (objectQueue.Count > 0)
        {
            T obj = objectQueue.Dequeue();

            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T newObj = Object.Instantiate(prefab, parent);
            newObj.gameObject.SetActive(true);
            objectQueue.Enqueue(newObj);

            return newObj;
        }
    }

    public void ReturnObjectToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objectQueue.Enqueue(obj);
    }
}
