using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.CharacterController
{
    public class RootMotion : MonoBehaviour
    {
        public Animator animator;
        public Vector3 positionDelta;
        public Quaternion rotationDelta;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            ResetDelta();
        }

        private void OnAnimatorMove()
        {
            //Debug.Log(animator.deltaPosition.z);
            // Accumulate rootMotion deltas between character updates 
            positionDelta += animator.deltaPosition;
            rotationDelta = animator.deltaRotation * rotationDelta;
        }

        public void ResetDelta()
        {
            positionDelta = Vector3.zero;
            rotationDelta = Quaternion.identity;
        }
    }
}
