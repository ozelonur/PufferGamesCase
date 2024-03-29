using _GAME_.Scripts.GlobalVariables;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerHealthController : Bear
    {
        #region Private Variables

        private int _healthPoints;

        private Collider _collider;
        private PlayerAnimateController _playerAnimateController;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _healthPoints = 20;
            _collider = GetComponent<Collider>();
            _playerAnimateController = transform.GetChild(0).GetChild(0).GetComponent<PlayerAnimateController>();
        }

        #endregion

        #region Public Methods

        public bool TakeHit()
        {
            _healthPoints -= 10;
            bool isDead = _healthPoints <= 0;

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
            _playerAnimateController.Die();
            _collider.enabled = false;
            
            Roar(CustomEvents.OnGameFailed, 3f);
        }

        #endregion
    }
}