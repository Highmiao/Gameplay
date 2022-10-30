﻿using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.CharacterController
{
    public class CharacterControllerBiped : MonoBehaviour, ICharacterController
    {
        public KinematicCharacterMotor motor;
        public RootMotion rootMotion;

        [Header("Stable Movement")]
        public float MaxStableMoveSpeed = 10f;
        public float StableMovementSharpness = 15;
        public float OrientationSharpness = 10;

        [Header("Air Movement")]
        public float MaxAirMoveSpeed = 10f;
        public float AirAccelerationSpeed = 5f;
        public float Drag = 0.1f;

        [Header("Animation Parameters")]
        public Animator CharacterAnimator;
        public float ForwardAxisSharpness = 10;
        public float TurnAxisSharpness = 5;

        [Header("Misc")]
        public Vector3 Gravity = new Vector3(0, -30f, 0);
        public Transform MeshRoot;

        private Vector3 _moveInputVector;
        private Vector3 _lookInputVector;
        private float _forwardAxis;
        private float _rightAxis;
        private float _targetForwardAxis;
        private float _targetRightAxis;

        /// <summary>
        /// This is called every frame by MyPlayer in order to tell the character what its inputs are
        /// </summary>
        public void SetInputs(ref PlayerCharacterInputs inputs)
        {
            // Axis inputs
            _targetForwardAxis = inputs.MoveAxisForward;
            _targetRightAxis = inputs.MoveAxisRight;
        }

        private void Start()
        {
            // Assign to motor
            motor.CharacterController = this;
        }

        private void Update()
        {
            // Handle animation
            _forwardAxis = Mathf.Lerp(_forwardAxis, _targetForwardAxis, 1f - Mathf.Exp(-ForwardAxisSharpness * Time.deltaTime));
            //_rightAxis = Mathf.Lerp(_rightAxis, _targetRightAxis, 1f - Mathf.Exp(-TurnAxisSharpness * Time.deltaTime));
            CharacterAnimator.SetFloat("Forward", _forwardAxis);
            //CharacterAnimator.SetFloat("Turn", _rightAxis);
            //CharacterAnimator.SetBool("OnGround", motor.GroundingStatus.IsStableOnGround);
        }

        /// <summary>
        /// (Called by KinematicCharacterMotor during its update cycle)
        /// This is called before the character begins its movement update
        /// </summary>
        public void BeforeCharacterUpdate(float deltaTime)
        {
        }

        /// <summary>
        /// (Called by KinematicCharacterMotor during its update cycle)
        /// This is where you tell your character what its rotation should be right now. 
        /// This is the ONLY place where you should set the character's rotation
        /// </summary>
        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            currentRotation = rootMotion.rotationDelta * currentRotation;
        }

        /// <summary>
        /// (Called by KinematicCharacterMotor during its update cycle)
        /// This is where you tell your character what its velocity should be right now. 
        /// This is the ONLY place where you can set the character's velocity
        /// </summary>
        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            if (motor.GroundingStatus.IsStableOnGround)
            {
                if (deltaTime > 0)
                {
                    // The final velocity is the velocity from root motion reoriented on the ground plane
                    currentVelocity = rootMotion.positionDelta / deltaTime;
                    currentVelocity = motor.GetDirectionTangentToSurface(currentVelocity, motor.GroundingStatus.GroundNormal) * currentVelocity.magnitude;
                    //Debug.Log("root position delta z " + rootMotion.positionDelta.z + "    velocity z " + currentVelocity.z);
                }
                else
                {
                    // Prevent division by zero
                    currentVelocity = Vector3.zero;
                }
            }
            else
            {
                if (_forwardAxis > 0f)
                {
                    // If we want to move, add an acceleration to the velocity
                    Vector3 targetMovementVelocity = motor.CharacterForward * _forwardAxis * MaxAirMoveSpeed;
                    Vector3 velocityDiff = Vector3.ProjectOnPlane(targetMovementVelocity - currentVelocity, Gravity);
                    currentVelocity += velocityDiff * AirAccelerationSpeed * deltaTime;
                }

                // Gravity
                currentVelocity += Gravity * deltaTime;

                // Drag
                currentVelocity *= (1f / (1f + (Drag * deltaTime)));
            }
        }

        /// <summary>
        /// (Called by KinematicCharacterMotor during its update cycle)
        /// This is called after the character has finished its movement update
        /// </summary>
        public void AfterCharacterUpdate(float deltaTime)
        {
            // Reset root motion deltas
            rootMotion.ResetDelta();
        }

        public bool IsColliderValidForCollisions(Collider coll)
        {
            return true;
        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
        }

        public void PostGroundingUpdate(float deltaTime)
        {
        }

        public void AddVelocity(Vector3 velocity)
        {
        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
        }

        public void OnDiscreteCollisionDetected(Collider hitCollider)
        {
        }
    }
}