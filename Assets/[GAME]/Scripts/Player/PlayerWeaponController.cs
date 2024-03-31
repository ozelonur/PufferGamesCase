using _GAME_.Scripts.GlobalVariables;
using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerWeaponController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Bullet.Bullet bulletPrefab;

        [SerializeField] private Transform bulletSpawnPoint;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.Shot, Shot);
            }

            else
            {
                Unregister(CustomEvents.Shot, Shot);
            }
        }

        private void Shot(object[] arguments)
        {
            Bullet.Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint);
            bullet.transform.localEulerAngles = Vector3.zero;
            bullet.transform.localPosition = Vector3.zero;
            
            bullet.InitBullet(bulletSpawnPoint);

            // DOVirtual.DelayedCall(0.01f, () => bullet.transform.parent = null).SetLink(bullet.gameObject);
            bullet.transform.parent = null;
        }

        #endregion
    }
}