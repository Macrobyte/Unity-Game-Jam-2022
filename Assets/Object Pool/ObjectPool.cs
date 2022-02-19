using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object pool", menuName = "My Game/Object Pool")]
public class ObjectPool : ScriptableObject
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 initialScale;

    List<PooledObject> objectsInPool = new List<PooledObject>();
    List<PooledObject> objectsInUse = new List<PooledObject>();

    private void OnEnable()
    {
        objectsInUse.Clear();
        objectsInPool.Clear();
    }

    public GameObject SpawnObject(Transform spawnPos, Quaternion rotation, bool setParent)
    {
        PooledObject currentObject;

        if(objectsInPool.Count <= 0)
        {
            GameObject newGameobject = Instantiate(prefab);

            if(!setParent) newGameobject.transform.SetParent(GameObject.FindGameObjectWithTag("PoolObjects").transform);

            currentObject = newGameobject.AddComponent<PooledObject>();
            currentObject.pool = this;
        }
        else
        {
            currentObject = objectsInPool[0];
            objectsInPool.Remove(currentObject);
        }

        objectsInUse.Add(currentObject);

        if (setParent) currentObject.transform.SetParent(spawnPos);

        currentObject.gameObject.transform.position = spawnPos.position;
        currentObject.gameObject.transform.rotation = rotation;
        currentObject.gameObject.transform.localScale = initialScale;

        currentObject.gameObject.SetActive(true);
        return currentObject.gameObject;
    }

    public void ReturnToPool(PooledObject objectToPool)
    {
        if (objectsInPool.Contains(objectToPool)) return;

        objectToPool.gameObject.SetActive(false);
        objectsInPool.Add(objectToPool);
        objectsInUse.Remove(objectToPool);
    }

    public void RemoveObject(PooledObject objectToRemove)
    {
        objectsInUse.Remove(objectToRemove);
        objectsInPool.Remove(objectToRemove);
    }
}
