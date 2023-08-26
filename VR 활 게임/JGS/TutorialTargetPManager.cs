using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTargetPManager : MonoBehaviour
{
    public bool isClear()
    {
        Transform[] children;

        children = this.gameObject.GetComponentsInChildren<Transform>();

        foreach(var obj in children)
        {
            if (obj.gameObject.active)
            {
                return false;
            }
        }

        return true;
    }
}
