using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerHealthController : Bear
    {
        #region Private Variables

        private int _healthPoints;

        private Collider _collider;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _healthPoints = 20;
            _collider = GetComponent<Collider>();
        }

        #endregion

        #region Public Methods

        public bool TakeHit()
        {
            _healthPoints -= 10;
            bool isDead = _healthPoints <= 0;
            Debug.Log("Got Hit : " + _healthPoints);

            if (isDead)
            {
                Die();
            }

            return isDead;
        }

        #endregion

        #region Private Methods

        private void Die()
        {
            Debug.Log("Player Died!");
            _collider.enabled = false;
        }

        #endregion
    }
}