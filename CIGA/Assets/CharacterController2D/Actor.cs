﻿using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Serialization;
using zoroiscrying;

[RequireComponent(typeof(CharacterController2D),typeof(Animator))]
public class Actor : MonoBehaviour {
	//Actor，场景中的所有角色，受重力影响，有基本的碰撞和相应事件
	
	#region Consts
	
	

	#endregion
	
	#region Public Variables

	[Header("AutoJump")]
	public JumpType ActorJumpType = JumpType.SimpleJump;
	public Vector2 JumpForce = Vector2.right;
	public bool AutoJump = false;
	public float JumpStopTime = 1.0f;
	
	[Header("AutoPatrol")]
	[FormerlySerializedAs("_patrolType")] public PatrolType ActorPatrolType;
	public bool _isPatrolling = false;
	public float PatrolStopTime = 1.0f;
	
	[Header("PreciseMovementCtrl")]
	public float _accelerationTimeAirborne = .2f;
	public float _accelerationTimeGrounded = .0f;
	[Space(5)]
	public float _timeToJumpApex = .4f;
	public float _maxJumpHeight = 1f;
	[Space(5)]
	public float _runSpeed = 8f;
	[Space(5)]
	public float _horizontalSpeedMultiplier = 1f;
	public float _verticalSpeedMultiplier = 1f;
	public bool _affectedByGravity = true;

	public Animator _animator;
	
	public int _normalizedDirX
	{
		get
		{
			if (_directionalInput.x > 0)
			{
				return 1;
			}
			if (_directionalInput.x < 0)
			{
				return -1;
			}
			return 0;
			
		}
	}

	public int _faceDir = 1;

	
	#endregion

	#region Private Variables
	
	private bool IsAtCorner
	{
		//判断到达边角的条件：横向射线和竖向射线
		get
		{
			//速度足够大，才进行边缘检测
			if (Mathf.Abs(_velocity.x) - 0.1f >= 0)
			{
				//横向射线检测
				var rayDistance = 2 * _controller.skinWidth;
				var rayDirectionDownWard = Vector2.down;
				var initialRayOriginL = _controller._raycastOrigins.bottomLeft - new Vector2(_controller.skinWidth,0);
				var initialRayOriginR = _controller._raycastOrigins.bottomRight + new Vector2(_controller.skinWidth,0);
				RaycastHit2D hit;
	
				if (_controller.collisionState.right )
				{
					Debug.Log("Right: " + _controller.collisionState.right + "NormalizedX: " + _normalizedDirX);
					Debug.Log("Right Collision");
					return true;
				}
				if (_controller.collisionState.left )
				{
					Debug.Log("Left Collision");
					return true;
				}
				
				//竖向射线检测
				//向右走
				if (_velocity.x > 0)
				{
					hit = Physics2D.Raycast(initialRayOriginR, rayDirectionDownWard, rayDistance ,_controller.platformMask &~ _controller.oneWayPlatformMask);
				}
				else 
				{
					hit = Physics2D.Raycast(initialRayOriginL, rayDirectionDownWard, rayDistance,_controller.platformMask &~ _controller.oneWayPlatformMask);
				}
	
				if (!hit)
				{
					Debug.Log("Down Below no collision");
					return true;
				}
			}
			return false;
		}
	}
	
	private int _movementMultiplier = 1;
	
	private float _gravity = -25f;

	private float _movementDamping;
	
	
	#endregion

	#region Protected Variables
	
	protected float _maxJumpVelocity;	
	public Vector3 _velocity;
	protected CharacterController2D _controller;
	protected Vector2 _directionalInput;//暂时无法确定要不要加这个接受输入的变量
	
	#endregion

	#region Struct,Class,Enums..

	public enum PatrolType
	{
		SimpleCornerP,
		PatrolWithDistance,
		PatrolBetweenDistanceNCorner
	}

	public enum JumpType
	{
		SimpleJump,
		AlwaysJump,
		VerticalJumpWithTime
	}

	#endregion

	#region Monobehaviors

	public virtual void Awake()
	{
		_jumpStopTimer = JumpStopTime;
		_patrolStopTimer = PatrolStopTime;
		_controller = GetComponent<CharacterController2D>();
		_animator = GetComponent<Animator>();

		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerExitEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
		
		CalculateGravityNVelocity();
	}

	public virtual void Update () 
	{

		if (AutoJump)
		{
			switch (ActorJumpType)
			{
				case JumpType.SimpleJump:
					JumpSeveralTimes(JumpForce,2);
					break;
				case JumpType.AlwaysJump:
					JumpAllTheTime(JumpForce);
					break;
				case JumpType.VerticalJumpWithTime:
					
					break;
			}
		}
		
		if (_isPatrolling && this._controller.isGrounded)
		{
			switch (ActorPatrolType)
			{
				case PatrolType.SimpleCornerP:
					Patrol();
					break;
				case PatrolType.PatrolWithDistance:
					PatrolOneDirInDistance(2.0f);
					break;
				case PatrolType.PatrolBetweenDistanceNCorner:
					BetweenDistancePatrol(2.0f);
					break;
			}
		}
		
		CalculateGravityNVelocity();
		
		CalculateVelocity();
		
		CalculateMovementBasedOnVelocity();//这个是每帧根据Velocity对角色进行移动，但是被继承之后还是把移动放到所有计算最后比较好，所以注释掉
		
		CheckCollisions();
		
		AnimFaceDirControl();
		
	}	

	#endregion

	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	
	}


	void onTriggerEnterEvent( Collider2D col )
	{
//		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
//		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}


	#endregion

	#region Public Functions

	#region Patroling Methods
	//向一个方向一直巡逻，直到遇到碰撞体或者到达边缘，默认开始向右边巡逻
	private float _patrolStopTimer;
	public void Patrol()
	{
		//到达边角后，开启计时器（增加计时器），计时器到点，继续前进
		if (IsAtCorner)
		{
			//设置一个event
			
			//开启计时器
			_patrolStopTimer = 0.0f;
			_movementMultiplier = -_movementMultiplier;
			Debug.Log("Hit Corner");
		}

		if (_patrolStopTimer < PatrolStopTime)
		{
			_velocity.x = 0;
			_patrolStopTimer += Time.deltaTime;
		}
		else
		{
			_velocity.x = Mathf.Abs(_runSpeed) * _movementMultiplier;			
		}
		
	}


	private float _distanceCounter = 0;
	public void PatrolOneDirInDistance( float distance,bool isGoingRight = true)
	{
		
		_distanceCounter += Mathf.Abs(_runSpeed) * Time.deltaTime;
		
		if (IsAtCorner)
		{
			_movementMultiplier = -_movementMultiplier;
		}
		_velocity.x = _runSpeed * _movementMultiplier;
		
		if (_distanceCounter >= distance)
		{
			_distanceCounter = 0;
			//结束巡逻
			_isPatrolling = false;
		}
	}

	public void BetweenDistancePatrol(float distance)
	{
		_distanceCounter += Mathf.Abs(_runSpeed) * Time.deltaTime;
		
		if (IsAtCorner)
		{
			_movementMultiplier = -_movementMultiplier;
		}
		_velocity.x = _runSpeed * _movementMultiplier;
		
		if (_distanceCounter >= distance)
		{
			//改变巡逻方向
			_movementMultiplier = -_movementMultiplier;
			_distanceCounter = 0;//重新开始计算距离
			
		}
		
	}
	

	#endregion

	#region Jumping Methods
	
	private float _jumpStopTimer = 0.0f;
	
	/// <summary>
	/// 跳跃一次，功能较为有限
	/// </summary>
	/// <param name="jumpForce"></param>
	public void Jump(Vector2 jumpForce)
	{
		if (_controller.isGrounded)
		{
			_velocity.x = jumpForce.x;
            _velocity.y = jumpForce.y;
            AutoJump = false;
		}
		else
		{
			_velocity.x = jumpForce.x;
		}
	}
	
	private int timeCounter = 0;
	/// <summary>
	/// 跳跃几次，带有暂停效果，并可指定跳跃多少次，默认为1次
	/// </summary>
	/// <param name="jumpForce"></param>
	/// <param name="howManyTimes"></param>
	public void JumpSeveralTimes(Vector2 jumpForce, int howManyTimes = 1)
	{
		if (_jumpStopTimer <= JumpStopTime && _controller.isGrounded)
		{
			_jumpStopTimer += Time.deltaTime;
			return;
		}
		
		//正常跳跃
		if (timeCounter < howManyTimes)
		{
			if (_controller.isGrounded)
			{
				_velocity.x = jumpForce.x;
				_velocity.y = jumpForce.y;
				timeCounter++;
				_jumpStopTimer = 0.0f;
			}
			else
			{
				_velocity.x = jumpForce.x;
			}			
		}
		else
		{
			//落地前不撤掉力
			if (!_controller.isGrounded)
			{
				_velocity.x = jumpForce.x;
			}
			//落地后撤力，并停止跳跃
			else
			{
				//_velocity.x = _velocity.y = 0;
				timeCounter = 0;
				AutoJump = false;
			}
		}
	}

	public void JumpAllTheTime(Vector2 jumpForce)
	{
		if (_controller.isGrounded)
		{
			_velocity.x = jumpForce.x;
            _velocity.y = jumpForce.y;
		}
		else
		{
			_velocity.x = jumpForce.x;
		}
	}

	public void JumpWithTime(Vector2 jumpForce, float time)
	{
		
	}
	
	#endregion

	
	
	public virtual void CalculateGravityNVelocity()
		{
			if (_affectedByGravity)
			{
				_gravity = -(2 * _maxJumpHeight) / Mathf.Pow(_timeToJumpApex, 2);
			}
			else
			{
				_gravity = 0;
			}
			//print(gravity);
			_maxJumpVelocity = Mathf.Abs(_gravity) * _timeToJumpApex;
		}
	
	#endregion

	#region Private Functions


	private void ReverseXSpeed()
	{
		this._velocity.x = -this._velocity.x;
	}
	
	private void CheckCollisions()
	{
		if ((_controller.collisionState.below || _controller.collisionState.above) &&
		    !_controller.collisionState.fallingThroughPlatform)
		{
			_velocity.y = 0;
		}
	}

	private void CalculateMovementBasedOnVelocity()
	{
		_controller.Move(_velocity * Time.deltaTime,_directionalInput);
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}
	
	private void CalculateVelocity()
	{
		float targetVelocityX = _directionalInput.x * _runSpeed * _horizontalSpeedMultiplier;
		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		//var smoothedMovementFactor = _controller.isGrounded ? movementDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.SmoothDamp( _velocity.x, targetVelocityX, ref _movementDamping,
			(_controller.collisionState.below)?_accelerationTimeGrounded:_accelerationTimeAirborne);
		// apply gravity before moving
		_velocity.y += _gravity * Time.deltaTime;
		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
	}

	private void AnimFaceDirControl()
	{
		if( _normalizedDirX == 1 )//向右
		{
			//Debug.Log("Turn right!");
			_faceDir = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}
		else if(_normalizedDirX == -1 )//向左
		{
			_faceDir = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}
	}
	#endregion

	#region Enumerators


	#endregion

}
