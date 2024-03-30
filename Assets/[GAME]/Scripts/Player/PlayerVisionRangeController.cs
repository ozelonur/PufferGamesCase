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

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            element.enabled = false;
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.VisionRangeVisibility, VisionRangeStatus);
            }

            else
            {
                Unregister(CustomEvents.VisionRangeVisibility, VisionRangeStatus);
            }
        }

        private void VisionRangeStatus(object[] arguments)
        {
            element.enabled = (bool)arguments[0];
        }

        #endregion
    }
}