using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    public static void SetPosAllZero(this LineRenderer lineRenderer)
    {
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i,Vector3.zero);
        }
    }

    public static void Swap<T>(this List<T> list,int n)
    {
        for (int i = 0; i < n; i++)
        {
            int src = Random.Range(0, list.Count);
            int dest;
            do
            {
                dest = Random.Range(0, list.Count);
            } while (src == dest);
            (list[src], list[dest]) = (list[dest], list[src]);
        }
    }
}