using System.Collections.Generic;
using UnityEngine;

public class ObjPool
 : MonoBehaviour
{
    GameObject pooledObj;
    public int pooledCount = 5;
    private List<GameObject> pool=new List<GameObject>();
    public GameObject GetPooledObj()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                print("Reusing pooled object: " + pool[i].name+"pool item "+i+" of "+pool.Count);
                return pool[i]; 
            }
        }
        var obj = pool[0];
        obj.SetActive(true);
        print("Pool exhausted");
        return obj;
    }
    // this is used to pool many of the same object at once
    public void SetPooled(GameObject obj, int quantity)
    {
        pooledObj = obj;
        pooledCount = quantity;
        initialize();
    }

    public void SetPooled(GameObject obj)
    {
        pooledObj = obj;
        initialize();
    }
    void initialize()
    {
        for (int i = 0; i < pooledCount; i++)
        {
            var obj = Instantiate(pooledObj);
            obj.SetActive(false);
            pool.Add(obj);
            print("pooled " + obj.name);
        }
        }
    }
