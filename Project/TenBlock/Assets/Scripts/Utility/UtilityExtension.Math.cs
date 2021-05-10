public static partial class UtilityExtension
{
    #region Abs
    public static int Abs(int value)
    {
        return value < 0 ? -value : value;
    }

    public static float Abs(float value)
    {
        return value < 0 ? -value : value;
    }
    #endregion

    #region Clamp
    public static int Clamp(this int value, int min, int max)
    {
        if (value < min)
            value = min;
        else if (value > max)
            value = max;

        return value;
    }


    public static float Clamp(this float value, float min, float max)
    {
        if (value < min)
            value = min;
        else if (value > max)
            value = max;

        return value;
    }
    #endregion

    #region Round
    public static int Round(this float value)
    {
        return UnityEngine.Mathf.RoundToInt(value);
    }
    #endregion
}
