using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "My Game/Difficulty")]
public class Difficulty : ScriptableObject
{
    public int maxWaste;
    public float wasteSpeed;
    public Vector2 spawnTimeRange;
}
