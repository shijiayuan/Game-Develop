using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour 
{
	public enum ME_Enum{
		Idle,
		Move,
		MoveBack,
		Attack,
		Die
	}

	private GameObject stork;
	public float atkSpeed;
	public const float MOVE_TOWARD_SPEED = 0.8f;
	public const float ATTACK_MOVE_SPEED = 0.5F;
	public const float MOVE_BACK_SPEED = 3;
	public const float ATTACK_DISTENCE = 10;
	private ME_Enum currState;
	private int hp = 100;
	private float attackFlag;
	private Vector3 oriPosition;

	public void setState(ME_Enum newState){
		currState = newState;
	}

	void Start(){
		stork = GameObject.Find ("stork");
		oriPosition = transform.position;
		currState = ME_Enum.Idle;
	}

	public void Update(){
		switch (currState) {
		case ME_Enum.Idle:
			DoIdle ();
			break;
		case ME_Enum.Move:
			DoMove ();
			break;
		case ME_Enum.MoveBack:
			DoMoveBack ();
			break;
		case ME_Enum.Attack:
			DoAttack ();
			break;
		case ME_Enum.Die:
			DoDie ();
			break;
		}
	}

	void DoIdle(){
		transform.Rotate(Vector3.up*Time.deltaTime*45);
		if (Vector3.Distance (transform.position, stork.transform.position) <= ATTACK_DISTENCE) {
			setState (ME_Enum.Move);
			//transform.Rotate(Vector3.right*Time.deltaTime);
			//Rigidbody rigidBody = GetComponent<Rigidbody> ();
			//rigidBody.velocity = transform.forward * 20;
		}
	}

	void DoMove(){
		if (Vector3.Distance (oriPosition, stork.transform.position) > ATTACK_DISTENCE) {
			setState (ME_Enum.MoveBack);
		}
		transform.LookAt(stork.transform);
		transform.Translate (stork.transform.position * Time.deltaTime * MOVE_TOWARD_SPEED);
	}

	void DoMoveBack(){
		if (Vector3.Distance (oriPosition, transform.position) > 0.2) {
			transform.Translate ((oriPosition - transform.position) * Time.deltaTime * MOVE_BACK_SPEED, Space.World);
		} else {
			setState (ME_Enum.Idle);
		}
	}

	void DoAttack(){
		if (Vector3.Distance (oriPosition, stork.transform.position) > ATTACK_DISTENCE) {
			setState (ME_Enum.MoveBack);
		}
		if (Time.time-attackFlag<(0.5/atkSpeed)) {
			transform.LookAt(stork.transform);
			transform.Translate (stork.transform.position * Time.deltaTime * -ATTACK_MOVE_SPEED);
		} else {
			transform.LookAt(stork.transform);
			transform.Translate (stork.transform.position * Time.deltaTime * ATTACK_MOVE_SPEED);
		}
	}

	void DoDie(){
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.name == "stork") {
			setState (ME_Enum.Attack);
			//UpdateHP (-10);
		}
		attackFlag = Time.time;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "playerBullet"){
			Destroy (gameObject);
			Destroy (other.gameObject);
		}
	}

	void UpdateHP(int num){
		hp += num;
		if(hp <= 0) setState(ME_Enum.Die);
	}
}
