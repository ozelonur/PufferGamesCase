using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player.Bullet
{
    public class Grenade : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private ParticleSystem explodeParticle;

        [SerializeField] private GameObject model;

        #endregion

        #region Private Variables

        private Rigidbody _rigidBody;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        #endregion

        #region Public Methods

        public void Init(Vector3 velocity)
        {
            _rigidBody.AddForce(velocity, ForceMode.Impulse);
        }

        public void Go(Vector3[] path, float glidingDuration)
        {
            transform.DOPath(path, glidingDuration, PathType.CatmullRom).SetOptions(false).SetEase(Ease.Linear)
                .SetLink(gameObject).OnComplete(() =>
                {
                    model.SetActive(false);
                    _rigidBody.isKinematic = true;
                    explodeParticle.Play();
                    Destroy(gameObject, 2);
                });
        }

        public void DisablePhysics()
        {
            _rigidBody.isKinematic = true;
        }

        #endregion
    }
}