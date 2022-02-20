using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool[] notePoolers;

    [SerializeField] Difficulty[] difficulties;

    [SerializeField] float[] xSpawnPos;

    [Header("Spawner")]
    [SerializeField] int amountToSpawn;

    [SerializeField] Difficulty currentDifficulty;

    bool hasStarted;
    float lastSpawnPoint;
    float timer = 0;

    public int TimeLimit
    {
        get => (int)Random.Range(currentDifficulty.spawnTimeRange.x, currentDifficulty.spawnTimeRange.y);
    }

    public void Start()
    {
        currentDifficulty = difficulties[0];
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
        amountToSpawn = Random.Range(1, currentDifficulty.maxWaste + 1);
    }

    public void Spawn()
    {
        RandomAmount();

        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 pos = new Vector3(RandomSpawnPos(), ScreenBoundary.Instance.screenBounds.y, 0);
            GameObject newObject = RandomWastePool().SpawnObject(transform, Quaternion.identity, true);
            newObject.GetComponent<NoteObject>().SetUp(pos, currentDifficulty.wasteSpeed);
            newObject.transform.position = pos;
        }
    }

    public ObjectPool RandomWastePool()
    {
        return notePoolers[Random.Range(0, notePoolers.Length)];
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

    public void ChangeDifficulty(Difficulty newDifficulty)
    {
        currentDifficulty = newDifficulty;
    }
}
