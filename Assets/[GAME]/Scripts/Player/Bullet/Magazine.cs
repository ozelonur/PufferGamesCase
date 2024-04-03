using _GAME_.Scripts.GlobalVariables;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player.Bullet
{
    public class Magazine : Bear
    {
        #region Private Methods

        private Rigidbody _rigidBody;

        #endregion

        #region Public Variables

        public MagazineType magazineType;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.isKinematic = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.CompareTag("Ground"))
            {
                return;
            }

            transform.localEulerAngles = Vector3.zero;

            Destroy(gameObject, 2f);
        }

        #endregion
        
        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.DropMagazine, DropMagazine);
                Register(CustomEvents.InsertMagazine, InsertMagazine);
            }

            else
            {
                Unregister(CustomEvents.DropMagazine, DropMagazine);
                Unregister(CustomEvents.InsertMagazine, InsertMagazine);
            }
        }

        private void InsertMagazine(object[] arguments)
        {
            if (magazineType != MagazineType.Full)
            {
                return;
            }

            Destroy(gameObject);
        }

        private void DropMagazine(object[] arguments)
        {
            if (magazineType != MagazineType.Empty)
            {
                return;
            }

            transform.parent = null;
            _rigidBody.isKinematic = false;
        }

        #endregion
    }
}