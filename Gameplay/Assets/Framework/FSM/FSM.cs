using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace Framework.FSM
{
    public class FSM
    {
        public Dictionary<int, StateBase> id2state = new Dictionary<int, StateBase>();

        public StateBase lastState;
        public StateBase currentState;

        public bool isInTransition;

        public void AddState<T>(int _stateId) where T : StateBase
        {
            if (id2state.ContainsKey(_stateId))
            {
                Logger.Info("state " + _stateId + " already exist.", "FSM");
                return;
            }

            T state = default;
            state.stateId = _stateId;
            state.fsm = this;
            state.InitState();

            id2state.Add(_stateId, state);
        }

        public bool TryTransitionTo(int toStateId, object _param)
        {
            if (isInTransition) return false;

            if (!id2state.TryGetValue(toStateId, out StateBase toState))
            {
                Logger.Info("no state " + toStateId, "FSM");
                return false;
            }

            try
            {
                isInTransition = true;

                if (!currentState.CanTransitionTo(toState, _param))
                {
                    isInTransition = false;
                    return false;
                }

                if (!toState.CanTransitionFrom(currentState, _param))
                {
                    isInTransition = false;
                    return false;
                }

                currentState.ExitState(toState, _param);

                lastState = currentState;
                currentState = toState;

                toState.EnterState(lastState, _param);

                isInTransition = false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Info(e.ToString());
                isInTransition = false;
                return false;
            }
        }

        public bool IsInState(int stateId)
        {
            return currentState.stateId == stateId;
        }
    }
}
