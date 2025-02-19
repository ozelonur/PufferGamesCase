using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Core.StateMachine
{
    public class StateMachineBase : Bear
    {
        #region Private Variables

        private State _currentState;

        #endregion

        #region MonoBehaviour Methods

        private void Update()
        {
            _currentState?.OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _currentState?.OnFixedUpdate(Time.fixedDeltaTime);
        }

        private void OnDrawGizmos()
        {
            _currentState?.OnDrawGizmos();
        }

        #endregion

        #region Public Methods

        public void SwitchState(State newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        #endregion
    }
}