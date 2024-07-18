using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
     [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    
    }

    #region Singleton 
    // region allows to collapse lines
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // Create a pool of objects
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj); // Add object to the queue
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) return null;
        GameObject objectToSpawn = poolDictionary[tag].Dequeue(); // Get the object from the queue

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // If you want to use the Start method when spawning object from pool, you must call it from interface because start method will run only once when the object is created
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn); // Add object back to the queue

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
}
