// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public partial class Utility
{
    private static float Min = 0;
    private static float Max = 255;

    public static Color GetNormalizedColor(float r, float g, float b, float a = 255.0f)
    {
        r = r.Clamp(Min, Max) / Max;
        g = g.Clamp(Min, Max) / Max;
        b = b.Clamp(Min, Max) / Max;
        a = a.Clamp(Min, Max) / Max;

        return new Color(r, g, b, a);
    }
}
