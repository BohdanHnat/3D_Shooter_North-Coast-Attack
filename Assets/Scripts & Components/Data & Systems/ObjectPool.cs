using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool<T>
    where T : Transform
{
    private T _prefab;
    private List<T> objectPool;
    public ObjectPool(T prefab, Vector3 position, Quaternion rotation, int preWarmSize = 0)
    {
        _prefab = prefab;
        objectPool = new List<T>(preWarmSize);
        for (int i = 0; i < preWarmSize; ++i)
        {
            CreateInstance(position, rotation);
        }
    }
    public T GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        foreach (var obj in objectPool)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                obj.position = position;
                obj.rotation = rotation;
                return obj;
            }
        }
        var instance = CreateInstance(position, rotation);
        instance.gameObject.SetActive(true);
        return instance;
    }
    private T CreateInstance(Vector3 position, Quaternion rotation)
    {
        var instace = Object.Instantiate(_prefab, position, rotation);
        instace.gameObject.SetActive(false);
        objectPool.Add(instace);
        return instace;
    }
}
