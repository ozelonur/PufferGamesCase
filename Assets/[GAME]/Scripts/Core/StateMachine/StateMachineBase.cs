using UnityEngine;

namespace _GAME_.Scripts.Core.StateMachine
{
    public class StateMachineBase : MonoBehaviour
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
            _currentState?.OnFixedUpdate(Time.deltaTime);
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