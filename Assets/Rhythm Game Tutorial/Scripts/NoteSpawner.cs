using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool[] notePoolers;
    [SerializeField] float[] xSpawnPos;
    [SerializeField] Vector2 timeRange;
    [SerializeField] bool hasStarted;

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

    public void Spawn()
    {
        Vector3 pos = new Vector3(RandomSpawnPos(), ScreenBoundary.Instance.screenBounds.y, 0);
        GameObject newObject = notePoolers[0].SpawnObject(transform, Quaternion.identity, true);
        newObject.transform.position = pos;

        GameManager.Instance.AddNote();
    }


    public float RandomSpawnPos()
    {
        return xSpawnPos[Random.Range(0, xSpawnPos.Length)];
    }

    public void ToggleStart(bool newState) => hasStarted = newState;
}
