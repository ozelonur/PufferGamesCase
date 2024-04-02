using _GAME_.Scripts.GlobalVariables;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerVisionRangeController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Renderer element;

        [SerializeField] private GameObject shotGunRange; 

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            element.enabled = false;
            shotGunRange.SetActive(false);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.VisionRangeVisibility, VisionRangeStatus);
                Register(CustomEvents.EnableShotgun, EnableShotGunRange);
            }

            else
            {
                Unregister(CustomEvents.VisionRangeVisibility, VisionRangeStatus);
                Unregister(CustomEvents.EnableShotgun, EnableShotGunRange);
            }
        }

        private void EnableShotGunRange(object[] arguments)
        {
            shotGunRange.SetActive(true);
        }

        private void VisionRangeStatus(object[] arguments)
        {
            element.enabled = (bool)arguments[0];
        }

        #endregion
    }
}