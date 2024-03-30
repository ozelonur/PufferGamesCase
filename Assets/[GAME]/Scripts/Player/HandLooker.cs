using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class HandLooker : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Vector3 eulerAngles;

        [SerializeField] private Vector3 position;

        #endregion

        #region MonoBehaviour Methods

        private void Update()
        {
            transform.localEulerAngles = eulerAngles;
            transform.localPosition = position;
        }

        #endregion
    }
}