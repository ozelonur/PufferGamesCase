using DamageNumbersPro;
using UnityEngine;

namespace _GAME_.Scripts.Enemy
{
    public class EnemyHealthController : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private DamageNumber damageNumber;

        [Header("Configurations")] [SerializeField]
        private int maxHealth;

        #endregion

        #region Private Variables

        private float _currentHealth;
        private Collider _collider;

        #endregion

        #region Properties

        public bool IsDamaged { get; set; }
        public bool IsDead { get; set; }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _collider = GetComponent<Collider>();   
            _currentHealth = maxHealth;
            IsDamaged = false;
        }

        #endregion

        #region Public Methods

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            IsDamaged = true;
            
            damageNumber.Spawn(transform.position + Vector3.up, damage);

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
            IsDead = true;
        }

        #endregion
    }
}