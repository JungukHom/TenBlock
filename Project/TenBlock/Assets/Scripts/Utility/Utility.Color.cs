// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public partial class UtilityExtension
{
    private static float Min = 0;
    private static float Max = 255;

    public static Color GetNormalizedColor(float r, float g, float b, float a = 255.0f)
    {
        r = Clamp(r, Min, Max) / Max;
        g = Clamp(g, Min, Max) / Max;
        b = Clamp(b, Min, Max) / Max;
        a = Clamp(a, Min, Max) / Max;

        return new Color(r, g, b, a);
    }
}
