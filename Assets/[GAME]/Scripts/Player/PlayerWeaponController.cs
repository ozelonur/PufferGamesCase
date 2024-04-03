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
        [SerializeField] private Transform leftHandParent;
        [SerializeField] private Transform rightHandParent;

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private GameObject magazineOnWeapon;

        #endregion

        #region Private Variables

        private Vector3 _startPosition;
        private Vector3 _startEulerAngles;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _startPosition = transform.localPosition;
            _startEulerAngles = transform.localEulerAngles;
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.Shot, Shot);
                Register(CustomEvents.GenerateMagazine, GenerateMagazine);
                Register(CustomEvents.InsertMagazine, InsertMagazine);
                Register(CustomEvents.WeaponPassTheLeftHand, PassLeftHand);
                Register(CustomEvents.WeaponPassTheRightHand, PassRightHand);
            }

            else
            {
                Unregister(CustomEvents.Shot, Shot);
                Unregister(CustomEvents.GenerateMagazine, GenerateMagazine);
                Unregister(CustomEvents.InsertMagazine, InsertMagazine);
                Unregister(CustomEvents.WeaponPassTheLeftHand, PassLeftHand);
                Unregister(CustomEvents.WeaponPassTheRightHand, PassRightHand);
            }
        }

        private void PassRightHand(object[] arguments)
        {
            transform.parent = rightHandParent;
            transform.localPosition = _startPosition;
            transform.localEulerAngles = _startEulerAngles;
        }

        private void PassLeftHand(object[] arguments)
        {
            transform.parent = leftHandParent;
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

            bullet.transform.parent = null;
        }

        #endregion
    }
}