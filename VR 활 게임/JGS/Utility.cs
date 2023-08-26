using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = list.Count - 1; j > 0; j--)
            {
                int rand = Random.Range(0, j);

                (list[j], list[rand]) = (list[rand], list[j]);
            }
        }

        return list;
    }
}