using System.Collections;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using Cinemachine;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Camera
{
    public class CameraController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        [Header("Configurations")] [SerializeField]
        private float shakeIntensity;

        [SerializeField] private float shakeTime;

        #endregion

        #region Private Variables

        private float _timer;
        private CinemachineBasicMultiChannelPerlin _perlin;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        
        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.Shot, ShakeCamera);
                Register(CustomEvents.ShakeOnGrenadeExplode, ShakeOnGrenadeExplode);
            }

            else
            {
                Unregister(CustomEvents.Shot, ShakeCamera);
                Unregister(CustomEvents.ShakeOnGrenadeExplode, ShakeOnGrenadeExplode);
            }
        }

        private void ShakeOnGrenadeExplode(object[] arguments)
        {
            Vector3 grenadePos = (Vector3)arguments[0];
            Vector3 playerPos = PlayerManager.Instance.GetPlayer().playerMoveTransform.position;
            
            float distanceToPlayer = Vector3.Distance(grenadePos, playerPos);
            
            float normalizedDistance = Mathf.Clamp01(1 - distanceToPlayer / 20f);
            float dynamicShakeIntensity = Mathf.Lerp(1f, 5f, normalizedDistance);
            Debug.Log(dynamicShakeIntensity);

            float dynamicShakeTime = Mathf.Lerp(.2f, .5f, normalizedDistance);

            if (_timer <= 0)
            {
                _timer = dynamicShakeTime;
                StartCoroutine(ShakeRoutine(dynamicShakeIntensity, dynamicShakeTime));
            }
        }

        private void ShakeCamera(object[] arguments)
        {
            if (_timer <= 0)
            {
                _timer = shakeTime;
                StartCoroutine(ShakeRoutine());
            }
        }
        
        private IEnumerator ShakeRoutine()
        {
            _perlin.m_AmplitudeGain = shakeIntensity;
            while (_timer > 0)
            {
                _timer -= Time.deltaTime;
                yield return null;
            }
            _perlin.m_AmplitudeGain = 0;
        }
        
        private IEnumerator ShakeRoutine(float dynamicShakeIntensity, float dynamicShakeTime)
        {
            _perlin.m_AmplitudeGain = dynamicShakeIntensity;
            float localTimer = dynamicShakeTime;
            while (localTimer > 0)
            {
                localTimer -= Time.deltaTime;
                yield return null;
            }
            _perlin.m_AmplitudeGain = 0;
        }

        #endregion
    }
}