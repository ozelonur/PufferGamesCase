using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;
using UnityEngine.VFX;

namespace _GAME_.Scripts.Player.Bullet
{
    public class OilBomb : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private VisualEffect explodeParticle;

        [SerializeField] private OilAreaController oilAreaPrefab;

        [SerializeField] private GameObject model;

        [Header("Configurations")] [SerializeField]
        private LayerMask layerMask;

        #endregion

        #region Private Variables

        private Rigidbody _rigidBody;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        #endregion

        #region Public Methods

        public void Init(Vector3 velocity)
        {
            _rigidBody.AddForce(velocity, ForceMode.Impulse);
        }

        public void Go(Vector3[] path, float glidingDuration)
        {
            transform.DOPath(path, glidingDuration, PathType.CatmullRom).SetOptions(false).SetEase(Ease.Linear)
                .SetLink(gameObject).OnComplete(() =>
                {
                    model.SetActive(false);
                    _rigidBody.isKinematic = true;

                    transform.localEulerAngles = Vector3.zero;


                    explodeParticle.Play();

                    OilAreaController oilArea = Instantiate(oilAreaPrefab);

                    oilArea.transform.GetChild(0).localScale = Vector3.zero;
                    oilArea.transform.position = transform.position;
                    oilArea.transform.localEulerAngles = Vector3.zero;

                    oilArea.Init(layerMask);
                    Destroy(gameObject, 2);
                });
        }

        public void DisablePhysics()
        {
            _rigidBody.isKinematic = true;
        }

        #endregion
    }
}