using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Player.Bullet;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerShotGunController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private ShotGunBullet bullet;

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Transform targetDirectionTransform;

        [Header("Configurations")] [SerializeField]
        private int pelletCount = 10;

        [SerializeField] private float spreadAngle = 30f;
        [SerializeField] private float shotPower = 20f;
        [SerializeField] private float maxDistance = 6f;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.FireShotGun, Fire);
            }

            else
            {
                Unregister(CustomEvents.FireShotGun, Fire);
            }
        }

        private void Fire(object[] arguments)
        {
            Roar(CustomEvents.ShakeCameraToShotGun);
            for (int i = 0; i < pelletCount; i++)
            {
                // Oyuncunun ileri yönüne göre rastgele bir açı hesapla
                float randomAngleY = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                // Oyuncunun ileri yönünü temel alarak Quaternion rotasyonunu hesapla
                Quaternion pelletRotation = Quaternion.Euler(0, randomAngleY, 0) * Quaternion.LookRotation(targetDirectionTransform.forward);

                ShotGunBullet pellet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation * pelletRotation);
        
                pellet.Shot(shotPower, maxDistance, pelletCount);
            }
        }

        #endregion
    }
}