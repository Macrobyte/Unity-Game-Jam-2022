using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool[] notePoolers;
    [SerializeField] float[] xSpawnPos;
    [SerializeField] Vector2 timeRange;

    [Header("Spawner")]
    [SerializeField] int maxAmountToSpawn;
    [SerializeField] int amountToSpawn;


    bool hasStarted;
    float lastSpawnPoint;
    float timer = 0;

    public int TimeLimit
    {
        get => (int)Random.Range(timeRange.x, timeRange.y);
    }

    public void Start()
    {
        timer = TimeLimit;
    }

    void Update()
    {
        if (!hasStarted) return;

        if (timer <= 0)
        {
            Spawn();
            timer = TimeLimit;
        }
        else timer -= Time.deltaTime;
    }

    public void RandomAmount()
    {
        amountToSpawn = Random.Range(1, maxAmountToSpawn + 1);
    }

    public void Spawn()
    {
        RandomAmount();

        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 pos = new Vector3(RandomSpawnPos(), ScreenBoundary.Instance.screenBounds.y, 0);
            GameObject newObject = notePoolers[0].SpawnObject(transform, Quaternion.identity, true);
            newObject.transform.position = pos;

            GameManager.Instance.AddNote();
        }
    }

    public float RandomSpawnPos()
    {
        float randomSpawn = xSpawnPos[Random.Range(0, xSpawnPos.Length)];

        while (lastSpawnPoint == randomSpawn)
        {
            randomSpawn = xSpawnPos[Random.Range(0, xSpawnPos.Length)];
        }

        lastSpawnPoint = randomSpawn;
        return lastSpawnPoint;
    }

    public void ToggleStart(bool newState) => hasStarted = newState;
}
