using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler singleton { get; private set; }

    private void Awake()
    {
        if (singleton != null)
            Destroy(this);
        singleton = this;
    }
    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if ( !poolDictionary.ContainsKey(tag) )
        {
            Debug.LogWarning("Pool Dictionary does not contain tag " + tag);
            return null;
        }
        GameObject obj = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(obj);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        IPooledObject pooled = obj.GetComponent<IPooledObject>();
        if (pooled != null) pooled.OnObjectSpawn();
        return obj;
    }
}
