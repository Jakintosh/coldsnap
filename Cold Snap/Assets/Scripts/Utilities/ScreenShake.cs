using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	private bool cam_shaking = false;
	private float cam_curTime = 0;

	private Vector3 cam_basePos;
	private float   cam_duration  = 0.0f;
	private float   cam_intensity = 0.0f;
	private bool    cam_decay 	  = false;

	private float cam_decayScaler = 1;
	private float cam_randX = 0;
	private float cam_randY = 0;

	private Camera mainCamera;



	private void Start(){
		mainCamera = GetComponent<Camera>();
		cam_basePos = transform.position;
	}
	private void Update(){
		if ( cam_shaking )
		{
			cam_updateTime();
			cam_checkForShakeEnd();
			cam_findDecayScaler();
			cam_rollNewPos();
			cam_moveToNewPos();
		}
		else{
			transform.position = cam_basePos;
		}
	}
	public void ShakeCamera ( float dur, float i, bool dec )
	{
		//cam_basePos = mainCamera.transform.position;
		cam_duration = dur;
		cam_intensity = i;
		cam_decay = dec;

		cam_shaking = true;
	}

	private void cam_checkForShakeEnd()
	{
		if (cam_curTime>=cam_duration)
		{
			//if (cam_basePos == new Vector3())
			//{
			//	cam_basePos = mainCamera.transform.position;
			//}

			cam_shaking = false;

			cam_curTime 	= 0;

			cam_duration 	= 0;
			cam_intensity 	= 0;
			cam_decay 		= false;

			cam_decayScaler = 1;
			cam_randX 		= 0;
			cam_randY 		= 0;

			mainCamera.transform.position = cam_basePos;
			//cam_basePos = new Vector3();

		}

		return;
	}
	private void cam_updateTime()
	{

		cam_curTime += Time.deltaTime;
	}
	private void cam_findDecayScaler()
	{
		if (cam_decay)
		{
			cam_decayScaler = 1 - (cam_curTime / cam_duration);
		}

		return;
	}
	private void cam_rollNewPos()
	{
		cam_randX = Random.Range(-cam_intensity,cam_intensity) * cam_decayScaler;
		cam_randY = Random.Range(-cam_intensity,cam_intensity) * cam_decayScaler;
	}
	private void cam_moveToNewPos()
	{

		mainCamera.transform.position = cam_basePos + new Vector3(cam_randX,cam_randY,0);
	}

}

