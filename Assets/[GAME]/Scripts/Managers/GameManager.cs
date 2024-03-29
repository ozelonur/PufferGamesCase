using _GAME_.Scripts.Core.Manager;
using _GAME_.Scripts.GlobalVariables;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    [DefaultExecutionOrder(-1000)]
    public class GameManager : Manager<GameManager>
    {
        #region Public Variables

        public bool IsGameStarted;
        public bool IsGameFailed;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.OnGameStart, OnGameStart);
                Register(CustomEvents.OnGameFailed, OnGameFailed);
            }
            else
            {
                Unregister(CustomEvents.OnGameStart, OnGameStart);
                Unregister(CustomEvents.OnGameFailed, OnGameFailed);
            }
        }

        private void OnGameFailed(object[] arguments)
        {
            IsGameStarted = false;
            IsGameFailed = true;
        }

        private void OnGameStart(object[] arguments)
        {
            IsGameFailed = false;
            IsGameStarted = true;
        }

        #endregion
    }
}