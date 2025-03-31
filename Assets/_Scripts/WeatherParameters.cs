using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

[Serializable]
public class WeatherParameters
{
    [Range(0, 100)] public float stormStrength;
    [Range(0, 100)] public float tornadoStrength;
    [Range(0, 100)] public float lightningFrequency;
}

