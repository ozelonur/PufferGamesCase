using UnityEngine;

namespace _GAME_.Scripts.Enemy
{
    public class EnemyHealthController : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Configurations")] [SerializeField]
        private int maxHealth;

        #endregion

        #region Private Variables

        private int _currentHealth;
        private EnemyAnimateController _enemyAnimateController;
        private Collider _collider;

        #endregion

        #region Properties

        public bool IsDamaged { get; set; }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _enemyAnimateController = transform.GetChild(0).GetChild(0).GetComponent<EnemyAnimateController>();
            _currentHealth = maxHealth;
            IsDamaged = false;
        }

        #endregion

        #region Public Methods

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            IsDamaged = true;

            if (_currentHealth <= 0)
            {
                Die();
                IsDamaged = false;
            }
        }

        public float CurrentHealth => _currentHealth;

        #endregion

        #region Private Methods

        private void Die()
        {
            _collider.enabled = false;
            _enemyAnimateController.Die();
        }

        #endregion
    }
}