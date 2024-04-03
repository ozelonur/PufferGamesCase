using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _GAME_.Scripts.Player.Bullet;
using OrangeBear.EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GAME_.Scripts.Player
{
    public class PlayerShotGunController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private ShotGunBullet bullet;

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Transform targetDirectionTransform;

        [Header("Configurations")]
        [SerializeField] private float shotPower = 20f;

        #endregion

        #region Private Variables

        private int _pelletCount;
        private float _maxDistance;
        private float _spreadAngle;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            _pelletCount = DataManager.Instance.GetShotgunSkillData().pelletCount;
            _maxDistance = DataManager.Instance.GetShotgunSkillData().range;
            _spreadAngle = DataManager.Instance.GetShotgunSkillData().angle;
        }

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
            for (int i = 0; i < _pelletCount; i++)
            {
                float randomAngleY = Random.Range(-_spreadAngle / 2, _spreadAngle / 2);
                Quaternion pelletRotation = Quaternion.Euler(0, randomAngleY, 0) * Quaternion.LookRotation(targetDirectionTransform.forward);

                ShotGunBullet pellet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation * pelletRotation);
        
                pellet.Shot(shotPower, _maxDistance, _pelletCount);
            }
        }

        #endregion
    }
}