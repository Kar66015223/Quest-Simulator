using UnityEngine;
using System.Collections.Generic;

public static class GameObjectHelper
{
    public static List<GameObject> FindObjectsWithTagIncludingInactive(string tag)
    {
        List<GameObject> results = new List<GameObject>();
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            // Skip objects in assets (like prefabs), only scene objects
            if (obj.hideFlags == HideFlags.None && obj.CompareTag(tag))
            {
                results.Add(obj);
            }
        }
        return results;
    }
}