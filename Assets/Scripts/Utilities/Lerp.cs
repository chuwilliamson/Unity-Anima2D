
using UnityEngine;
public static class Lerp
{
    /// <summary>
    /// Equations from the following website
    /// https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
    /// </summary> 
    static public float GetLerp(float t, LerpType lerpType, AnimationCurve ac = null)
    {
        switch (lerpType)
        {
            case LerpType.Linear:
                break;
            case LerpType.Exponential:
                t = t * t;
                break;
            case LerpType.EaseIn:
                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
                break;
            case LerpType.EaseOut:
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
                break;
            case LerpType.Smoothe:
                t = t * t * (3f - 2f * t);
                break;
            case LerpType.Smoother:
                t = t * t * t * (t * (6f * t - 15f) + 10f);
                break;
            case LerpType.Custom:
                t = ac.Evaluate(t);
                break;
            default:
                break;
        }
        return t;
    }

    
}
public enum LerpType
{
    Linear,
    Exponential,
    EaseIn,
    EaseOut,
    Smoothe,
    Smoother,
    Custom,

}