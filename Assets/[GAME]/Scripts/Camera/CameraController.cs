using System.Collections;
using _GAME_.Scripts.GlobalVariables;
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
            }

            else
            {
                Unregister(CustomEvents.Shot, ShakeCamera);
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

        #endregion
    }
}