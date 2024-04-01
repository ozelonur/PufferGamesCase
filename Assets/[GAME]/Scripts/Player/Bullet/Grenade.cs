using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player.Bullet
{
    public class Grenade : Bear
    {
        #region Private Variables

        private Rigidbody _rigidBody;
        private bool _isGhost;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        #endregion
        
        #region Public Methods

        public void Init(Vector3 velocity, bool isGhost)
        {
            _isGhost = isGhost;
            _rigidBody.AddForce(velocity, ForceMode.Impulse);
        }

        #endregion
    }
}