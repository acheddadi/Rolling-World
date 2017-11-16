using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour {

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if ((animatorStateInfo.normalizedTime > 0.4f) && (animatorStateInfo.normalizedTime < 0.55f)) animator.SetBool("impact", true);
        else animator.SetBool("impact", false);
    }
}
