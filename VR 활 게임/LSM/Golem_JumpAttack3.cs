using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_JumpAttack3 : StateMachineBehaviour
{
    float batTime = 0;
    bool batCheck = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("G_Attack_2", 1f);
        batCheck = true;
        batTime = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        batTime += Time.deltaTime;
        if (batTime >= 0.4f && batCheck == true)
        {
            batCheck = false;
            SoundManager.Instance.PlaySoundSecond("G_JumpAttack_Impact", 0.5f);
        }
    }
}
