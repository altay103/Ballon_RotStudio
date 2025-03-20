using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BallonData
{
    public Color color = new Color(1, 1, 1, 1);
    [Range(0, 1)]
    public float spawnRate; // 1-0
    public int deltaScore;
    [HideInInspector]
    public int hit;
}
