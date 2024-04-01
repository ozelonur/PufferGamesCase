using Vector3 = UnityEngine.Vector3;

namespace _GAME_.Scripts.Player.States
{
    public class PlayerDashState : PlayerBaseState
    {
        #region Private Variables

        private float _dashSpeed;
        private float _dashDistance;
        private Vector3 _direction;
        private Vector3 _startPosition;

        private bool _isDashing;

        #endregion

        #region Constructor

        public PlayerDashState(PlayerStateMachine stateMachine, float dashSpeed, float dashDistance) : base(
            stateMachine)
        {
            _dashSpeed = dashSpeed;
            _dashDistance = dashDistance;
        }

        #endregion

        #region Inherit Methods

        public override void OnEnter()
        {
            _direction = rotateTransform.forward;
            _isDashing = true;
            _startPosition = moveTransform.position;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!_isDashing)
            {
                return;
            }

            moveTransform.position += _direction * (_dashSpeed * deltaTime);
            
            if (Vector3.Distance(moveTransform.position, _startPosition) >= _dashDistance)
            {
                _isDashing = false;
                stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            }
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
        }

        public override void OnExit()
        {
        }

        public override void OnDrawGizmos()
        {
        }

        #endregion
    }
}