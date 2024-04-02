using _GAME_.Scripts.Core.Manager;
using _GAME_.Scripts.Player;

namespace _GAME_.Scripts.Managers
{
    public class PlayerManager : Manager<PlayerManager>
    {
        #region Private Variables

        private PlayerStateMachine _playerStateMachine;

        #endregion

        #region Public Methods

        public void SetPlayer(PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }

        public PlayerStateMachine GetPlayer()
        {
            return _playerStateMachine;
        }

        #endregion
    }
}