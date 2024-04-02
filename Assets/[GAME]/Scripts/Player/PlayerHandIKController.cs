using _GAME_.Scripts.GlobalVariables;
using DitzelGames.FastIK;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerHandIKController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Transform weaponTarget;

        [SerializeField] private Transform shotGunTarget;
        [SerializeField] private FastIKFabric ik;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.EnableShotgun, EnableShotGun);
                Register(CustomEvents.DisableShotGun, DisableShotGun);
            }

            else
            {
                Unregister(CustomEvents.EnableShotgun, EnableShotGun);
                Unregister(CustomEvents.DisableShotGun, DisableShotGun);
            }
        }

        private void DisableShotGun(object[] arguments)
        {
            ik.Target = weaponTarget;
        }

        private void EnableShotGun(object[] arguments)
        {
            ik.Target = shotGunTarget;
        }

        #endregion
    }
}