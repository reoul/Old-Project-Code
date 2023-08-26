using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Attack1 : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("G_Attack_1", 1f);
    }
}
