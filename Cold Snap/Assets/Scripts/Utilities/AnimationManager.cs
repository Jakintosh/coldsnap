using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	Animator animator;

	private void Awake(){
		animator = gameObject.GetComponent<Animator>();
	}

	public void UpdateAnimationState( bool frozen, bool isRunning , float yVelocity ){


		if (frozen){
			animator.SetBool("frozen",true);
			animator.SetBool("jumping",false);
			animator.SetBool("falling",false);
			animator.SetBool("running",false);
			animator.SetBool("idling",false);
			return;
		}

		// Jumping
		if ( yVelocity > 0 ){
			animator.SetBool("frozen",false);
			animator.SetBool("jumping",true);
			animator.SetBool("falling",false);
			animator.SetBool("running",false);
			animator.SetBool("idling",false);
		}

		// Falling
		else if ( yVelocity < 0 ){
			animator.SetBool("frozen",false);
			animator.SetBool("jumping",false);
			animator.SetBool("falling",true);
			animator.SetBool("running",false);
			animator.SetBool("idling",false);
		}

		// Running
		else if ( isRunning ){
			animator.SetBool("frozen",false);
			animator.SetBool("jumping",false);
			animator.SetBool("falling",false);
			animator.SetBool("running",true);
			animator.SetBool("idling",false);
		}

		// Idle
		else{
			animator.SetBool("frozen",false);
			animator.SetBool("jumping",false);
			animator.SetBool("falling",false);
			animator.SetBool("running",false);
			animator.SetBool("idling",true);
		}
	}

	public void RangedAttack(){
		animator.SetTrigger("rangedAttack");
	}

	public void MeleeAttack(){
	}

}
