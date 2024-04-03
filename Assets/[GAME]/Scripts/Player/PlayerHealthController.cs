using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
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
            _healthPoints = DataManager.Instance.GetPlayerBaseAttackData().maxHealth;
            _collider = GetComponent<Collider>();
            _playerAnimateController = transform.GetChild(0).GetChild(0).GetComponent<PlayerAnimateController>();
        }

        #endregion

        #region Public Methods

        public bool TakeHit()
        {
            _healthPoints -= DataManager.Instance.GetEnemyData().damagePower;
            bool isDead = _healthPoints <= 0;

            if (isDead)
            {
                Die();
            }
            else
            {
                Roar(CustomEvents.GetHit);
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