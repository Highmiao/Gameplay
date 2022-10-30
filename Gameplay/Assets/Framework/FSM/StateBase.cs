using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.FSM
{
    public class StateBase
    {
        public FSM fsm;
        public int stateId;

        public virtual void InitState() { }

        public virtual void EnterState(StateBase _fromState, object _params = null) { }

        public virtual void UpdateState() { }

        public virtual void FixedUpdate() { }

        public virtual void ExitState(StateBase _toState, object _params = null) { }

        public virtual bool CanTransitionTo(StateBase _toState, object _params = null)
        {
            return true;
        }

        public virtual bool CanTransitionFrom(StateBase _fromState, object _params = null)
        {
            return true;
        }

        public virtual void Dispose() { }
    }
}
