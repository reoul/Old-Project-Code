using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent_Attack : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("monster-low-roar-SBA-300055103-preview", 1f);
    }
}
