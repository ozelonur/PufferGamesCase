using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.Managers;
using UnityEngine;

namespace _GAME_.Scripts.Player.States
{
    public class PlayerGrenadeState : PlayerBaseState
    {
        #region Private Variables

        private Projection _projection;
        private UnityEngine.Camera _mainCamera;
        private LayerMask _groundLayer;
        private int _frameCount;
        private float _force;
        private float _tilling;

        private float _minDistance = 3f;
        private float _maxDistance = 10f;

        private const float _frameCountMin = 34f;
        private const float _forceMin = 5f;
        private const float _tillingMin = 15f;

        private const float _frameCountMax = 71f;
        private const float _forceMax = 10f;
        private const float _tillingMax = 51f;

        private bool _isGrenadeThrew;

        #endregion

        #region Constructor

        public PlayerGrenadeState(PlayerStateMachine stateMachine, LayerMask groundLayer) : base(stateMachine)
        {
            _projection = stateMachine.projection;
            _mainCamera = UnityEngine.Camera.main;
            _groundLayer = groundLayer;
        }

        #endregion

        #region Inherit Methods

        public override void OnEnter()
        {
            look = true;
            stateMachine.canLook = true;
            // playerAnimateController.SetLayerWeight(1,1);
            // playerAnimateController.ThrowGrenade();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!Input.GetMouseButtonDown(0) && !_isGrenadeThrew)
            {
                input = playerInputController.moveVector.ToVector3XZ();
                Look(deltaTime);

                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
                {
                    float distance = Vector3.Distance(moveTransform.position, hit.point);

                    distance = Mathf.Clamp(distance, _minDistance, _maxDistance);

                    float t = (distance - _minDistance) / (_maxDistance - _minDistance);
                    _frameCount = Mathf.RoundToInt(Mathf.Lerp(_frameCountMin, _frameCountMax, t));
                    _force = Mathf.Lerp(_forceMin, _forceMax, t);
                    _tilling = Mathf.Lerp(_tillingMin, _tillingMax, t);

                    _projection.SimulateStatus(true, _frameCount, _force, _tilling);
                }
                
                return;
            }

            
            look = true;
            stateMachine.canLook = true;
            _isGrenadeThrew = true;
            _projection.DisableLineAndIndicator();
            playerAnimateController.ThrowGrenade();
            playerAnimateController.SetLayerWeight(1,1);
            
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }

            Move(fixedDeltaTime);
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