using _GAME_.Scripts.Enemy;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
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

        [SerializeField] private LayerMask layerMask;

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
                    Roar(CustomEvents.ShakeOnGrenadeExplode, transform.position);
                    TryGiveDamage();
                    Destroy(gameObject, 2);
                });
        }

        public void DisablePhysics()
        {
            _rigidBody.isKinematic = true;
        }

        #endregion

        #region Private Methods

        private void TryGiveDamage()
        {
            float impactRadius = DataManager.Instance.GetGrenadeSkillData().bombImpactRadius;
            float baseDamage = DataManager.Instance.GetGrenadeSkillData().bombBaseDamage;
            Collider[] colliders = Physics.OverlapSphere(transform.position, impactRadius, layerMask);

            if (colliders.Length <= 0) return;
            
            foreach (Collider col in colliders)
            {
                if (!col.TryGetComponent(out EnemyHealthController enemy)) continue;
                
                Vector3 enemyPos = enemy.transform.position;
                float distance = Vector3.Distance(enemyPos, transform.position);

                float damage = baseDamage + (impactRadius * 2 - distance);
                        
                enemy.TakeDamage(damage);
            }
        }

        #endregion
    }
}