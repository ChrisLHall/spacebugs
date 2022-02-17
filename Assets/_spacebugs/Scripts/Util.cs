using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Util
{
    private static WaitForEndOfFrame wfeof = new WaitForEndOfFrame();

    public static Coroutine DelayedAction(this MonoBehaviour obj, float delay, UnityAction action)
    {
        return obj.StartCoroutine(DelayedActionCR(delay, action));
    }

    private static IEnumerator DelayedActionCR(float delay, UnityAction action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    public static Coroutine DelayedActionEndOfFrame(this MonoBehaviour obj, UnityAction action)
    {
        return obj.StartCoroutine(DelayedActionEndOfFrameCR(action));
    }

    private static IEnumerator DelayedActionEndOfFrameCR(UnityAction action)
    {
        yield return wfeof;
        action();
    }

    public static Vector2 ToVector2(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    public static Vector3 ToVector3(this Vector2 vec)
    {
        return new Vector3(vec.x, vec.y, 0f);
    }
}
