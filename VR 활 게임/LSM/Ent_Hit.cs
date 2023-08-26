using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent_Hit : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("punch-face-fist-cuff-SBA-300130187-preview", 1f);
        SoundManager.Instance.PlaySoundFourth("monster-groan-pain-slow-SBA-300117374-preview");
    }
}

