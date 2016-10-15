using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	Animator animator;

	private void Awake(){
		animator = gameObject.GetComponent<Animator>();
	}

	public void UpdateAnimationState( bool isRunning , float yVelocity ){

		// Jumping
		if ( yVelocity > 0 ){
			animator.SetBool("jumping",true);
			animator.SetBool("falling",false);
			animator.SetBool("running",false);
			animator.SetBool("idling",false);
		}

		// Falling
		else if ( yVelocity < 0 ){
			animator.SetBool("jumping",false);
			animator.SetBool("falling",true);
			animator.SetBool("running",false);
			animator.SetBool("idling",false);
		}

		// Running
		else if ( isRunning ){
			animator.SetBool("jumping",false);
			animator.SetBool("falling",false);
			animator.SetBool("running",true);
			animator.SetBool("idling",false);
		}

		// Idle
		else{
			animator.SetBool("jumping",false);
			animator.SetBool("falling",false);
			animator.SetBool("running",false);
			animator.SetBool("idling",true);
		}
	}

}
