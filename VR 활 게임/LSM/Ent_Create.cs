using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent_Create : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("magical-futuristic-gate-opening-SBA-300054819-preview", 1f);
    }
}

