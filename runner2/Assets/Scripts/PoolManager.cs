using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    Dictionary<int, Queue<GameObject>> poolDictinoary = new Dictionary<int, Queue<GameObject>>();

    static PoolManager _instance;

    public static PoolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();

        if (!poolDictinoary.ContainsKey(poolKey))
        {
            poolDictinoary.Add(poolKey, new Queue<GameObject>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab) as GameObject;
                newObject.SetActive(false);
                poolDictinoary[poolKey].Enqueue(newObject);
            }
        }

    }

    public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        //prefab.transform.position = Vector3.zero;

        if (poolDictinoary.ContainsKey(poolKey))
        {
            GameObject objectToReuse = poolDictinoary[poolKey].Dequeue();
            poolDictinoary[poolKey].Enqueue(objectToReuse);
            objectToReuse.SetActive(true);

            objectToReuse.transform.position = position;
            objectToReuse.transform.rotation = rotation;

            return objectToReuse;
        }
        return prefab;
    }
    
}
