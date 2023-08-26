using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Idle,
    Attack,
    Hit,
    Death
}

public static class AnimationTime
{
    public static float Duration(AnimationType type, Animator animator)
    {
        string animationName;
        switch (type)
        {
            case AnimationType.Idle:
                animationName = "Idle";
                break;
            case AnimationType.Attack:
                animationName = "Attack";
                break;
            case AnimationType.Hit:
                animationName = "Hit";
                break;
            case AnimationType.Death:
                animationName = "Death";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; ++i)
        {
            if (ac.animationClips[i].name.Equals(animationName))
            {
                time = ac.animationClips[i].length;
                break;
            }
        }

        return time;
    }
}
