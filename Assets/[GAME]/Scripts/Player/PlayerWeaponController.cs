using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Player.Bullet;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerWeaponController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Bullet.Bullet bulletPrefab;

        [SerializeField] private Magazine magazinePrefab;
        [SerializeField] private Transform magazineParent;

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private GameObject magazineOnWeapon;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.Shot, Shot);
                Register(CustomEvents.GenerateMagazine, GenerateMagazine);
                Register(CustomEvents.InsertMagazine, InsertMagazine);
            }

            else
            {
                Unregister(CustomEvents.Shot, Shot);
                Unregister(CustomEvents.GenerateMagazine, GenerateMagazine);
                Unregister(CustomEvents.InsertMagazine, InsertMagazine);
            }
        }

        private void InsertMagazine(object[] arguments)
        {
            magazineOnWeapon.gameObject.SetActive(true);
        }

        private void GenerateMagazine(object[] arguments)
        {
            MagazineType type = (MagazineType)arguments[0];

            Magazine magazine = Instantiate(magazinePrefab, magazineParent);

            magazine.magazineType = type;

            magazine.transform.localPosition = Vector3.zero;
            magazine.transform.localEulerAngles = Vector3.zero;

            magazineOnWeapon.SetActive(false);
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