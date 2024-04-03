using _GAME_.Scripts.Enemy;
using _GAME_.Scripts.Managers;
using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player.Bullet
{
    public class OilAreaController : Bear
    {

        #region Private Variables

        private bool canEffectEnemies;
        private bool isDestroying;
        private float _timer;
        private LayerMask _layerMask;
        private float _effectTime;
        private float _radius;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _effectTime = DataManager.Instance.GetStunSkillData().stunTime;
            _radius = DataManager.Instance.GetStunSkillData().impactAreaRadius;
            transform.GetChild(0).localScale = Vector3.zero;
        }

        private void Update()
        {
            if (_timer >= _effectTime && !isDestroying)
            {
                GoSmallAndDestroy();
            }

            if (!canEffectEnemies)
            {
                return;
            }

            _timer += Time.deltaTime;

            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);

            foreach (Collider col in colliders)
            {
                if (col.TryGetComponent(out EnemyBehaviorTree enemy))
                {
                    if (!enemy.isSlipping)
                    {
                        Debug.Log("There was an enemy!");
                        enemy.Slip();
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public void Init(LayerMask layerMask)
        {
            _layerMask = layerMask;
            Vector3 pos = transform.position;
            pos.y = 0f;
            transform.position = pos;
            transform.GetChild(0).DOScale(Vector3.one * _radius * 2, .35f).SetEase(Ease.Linear).OnComplete(() =>
            {
                canEffectEnemies = true;
            });
        }

        #endregion

        #region Private Methods

        private void GoSmallAndDestroy()
        {
            isDestroying = true;
            transform.GetChild(0).DOScale(Vector3.zero, .2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        #endregion
    }
}