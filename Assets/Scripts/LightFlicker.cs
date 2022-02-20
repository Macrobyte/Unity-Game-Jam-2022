using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] GameObject lightObject;
    [SerializeField] Vector2 timeRange;

    bool isChanged = true;
    float timer = 0;

    public int TimeLimit
    {
        get => (int)Random.Range(timeRange.x, timeRange.y);
    }
    void Start()
    {
        timer = TimeLimit;
    }

    void Update()
    {
        if (timer <= 0)
        {
            isChanged = !isChanged;
            ToggleLight(isChanged);
            timer = TimeLimit;
        }
        else timer -= Time.deltaTime;
    }

    public void ToggleLight(bool newState)
    {
        lightObject.SetActive(newState);
    }
}
