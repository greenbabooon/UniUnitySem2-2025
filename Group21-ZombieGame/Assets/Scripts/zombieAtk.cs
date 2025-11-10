using UnityEngine;

public class zombieAtk : StateMachineBehaviour
{
    public int frameNum = 24;
    int curFrame = 1;
    bool dmgDealt = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dmgDealt = false;
        curFrame = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        curFrame++;
        if (curFrame >= frameNum && dmgDealt == false)
        {
            animator.GetComponentInParent<StandardZombie>().OnAnimationEnded();
            dmgDealt = true; 
        }  
         
             
         
    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.GetComponentInParent<StandardZombie>().CanAttack();  
       
    }


}
