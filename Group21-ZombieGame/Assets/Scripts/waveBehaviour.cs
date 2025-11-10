using UnityEngine;

public class waveBehaviour : StateMachineBehaviour
{

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<wave>().EndFire();   
    }


}
