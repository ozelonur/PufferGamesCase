using _GAME_.Scripts.Core.Manager;
using _GAME_.Scripts.Models;
using _GAME_.Scripts.ScriptableObjects;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class DataManager : Manager<DataManager>
    {
        #region Serialized Fields

        [Header("Settings")] [SerializeField]
        private PlayerMovementDataScriptableObject playerMovementDataScriptableObject;

        [SerializeField] private PlayerBaseAttackDataScriptableObject playerBaseAttackDataScriptableObject;
        [SerializeField] private DashSkillDataScriptableObject dashSkillDataScriptableObject;
        [SerializeField] private GrenadeSkillDataScriptableObject grenadeSkillDataScriptableObject;
        [SerializeField] private ShotGunSkillDataScriptableObject shotGunSkillDataScriptableObject;
        [SerializeField] private StunSkillDataScriptableObject stunSkillDataScriptableObject;
        [SerializeField] private EnemyDataScriptableObject enemyDataScriptableObject;

        #endregion
        #region Private Variables

        private PlayerMovementData _playerMovementData;
        private PlayerBaseAttackData _playerBaseAttackData;
        private DashSkillData _dashSkillData;
        private GrenadeSkillData _grenadeSkillData;
        private ShotGunSkillData _shotGunSkillData;
        private StunSkillData _stunSkillData;
        private EnemyData _enemyData;

        #endregion

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();
            _playerMovementData = playerMovementDataScriptableObject.movementData;
            _playerBaseAttackData = playerBaseAttackDataScriptableObject.data;
            _dashSkillData = dashSkillDataScriptableObject.data;
            _grenadeSkillData = grenadeSkillDataScriptableObject.data;
            _shotGunSkillData = shotGunSkillDataScriptableObject.data;
            _stunSkillData = stunSkillDataScriptableObject.data;
            _enemyData = enemyDataScriptableObject.data;
        }

        #endregion

        #region Public Methods

        public PlayerMovementData GetPlayerMovementData()
        {
            return _playerMovementData;
        }

        public PlayerBaseAttackData GetPlayerBaseAttackData()
        {
            return _playerBaseAttackData;
        }

        public DashSkillData GetDashSkillData()
        {
            return _dashSkillData;
        }

        public GrenadeSkillData GetGrenadeSkillData()
        {
            return _grenadeSkillData;
        }

        public ShotGunSkillData GetShotgunSkillData()
        {
            return _shotGunSkillData;
        }

        public StunSkillData GetStunSkillData()
        {
            return _stunSkillData;
        }

        public EnemyData GetEnemyData()
        {
            return _enemyData;
        }

        #endregion
    }
}