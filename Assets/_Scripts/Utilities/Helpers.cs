using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    public static IEnumerator CallFunctionForTime(System.Action function, float duration, float interval, System.Action callback)
    {
        float timer = 0f;
        while (timer < duration)
        {
            function?.Invoke();
            yield return new WaitForSeconds(interval);
            timer += interval;
        }
        callback?.Invoke();
    }

    public static IEnumerator WaitForRealtime(System.Action function, int time)
    {
        yield return new WaitForSecondsRealtime(time);
        function?.Invoke();
    }
}
