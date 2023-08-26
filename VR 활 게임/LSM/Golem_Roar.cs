using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Roar : StateMachineBehaviour
{
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("GolemRoar", 0.7f);
    }
   
}
