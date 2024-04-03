using _GAME_.Scripts.Enemy;
using _GAME_.Scripts.Managers;
using UnityEngine;

namespace _GAME_.Scripts.Player.Bullet
{
    public class ShotGunBullet : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private GameObject muzzlePrefab;

        [SerializeField] private GameObject hitPrefab;

        #endregion
        
        #region Private Variables

        private Rigidbody _rigidBody;
        private bool _collided;
        private float _maxDistance;
        private float _totalPelletCount;
        private Vector3 _startPosition;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Vector3.Distance(_startPosition, transform.position) >= _maxDistance)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_collided)
            {
                return;
            }

            if (other.transform.TryGetComponent(out EnemyHealthController enemy))
            {
                Vector3 playerPos = PlayerManager.Instance.GetPlayer().playerMoveTransform.position;
                Vector3 enemyPos = enemy.transform.position;

                float damageForPerPellet = DataManager.Instance.GetShotgunSkillData().baseDamage / _totalPelletCount;

                float distance = Vector3.Distance(playerPos, enemyPos);

                float damage = damageForPerPellet + (_maxDistance - distance);
                
                enemy.TakeDamage(damage);
            }

            _collided = true;
            Destroy(gameObject);
        }

        #endregion

        #region Public Methods

        public void Shot(float force, float maxDistance, int totalPelletCount)
        {
            _totalPelletCount = totalPelletCount;
            _maxDistance = maxDistance;
            _startPosition = transform.position;
            InitBullet();
            _rigidBody.AddForce(transform.forward * force, ForceMode.Impulse);
            transform.parent = null;
        }

        #endregion

        #region Private Methods

        private void InitBullet()
        {
            GameObject muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.parent = transform.parent;
            muzzleVFX.transform.localPosition = Vector3.zero;
            muzzleVFX.transform.localEulerAngles = Vector3.zero;
            ParticleSystem ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(muzzleVFX, ps.main.duration);
            }
            else
            {
                ParticleSystem psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }

        #endregion
    }
}