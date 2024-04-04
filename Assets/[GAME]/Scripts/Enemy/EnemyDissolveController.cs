using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace _GAME_.Scripts.Enemy
{
    public class EnemyDissolveController : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeField] private VisualEffect vfx;

        [Header("Configurations")] [SerializeField]
        private float dissolveRate = .0125f;

        [SerializeField] private float refreshRate = .025f;
        [SerializeField] private float delayAmount;

        #endregion

        #region Private Variables

        private Material[] _materials;
        private bool _dissolving;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _materials = skinnedMeshRenderer.materials;
        }

        #endregion

        #region Public Methods

        public void DissolveMesh(float delay)
        {
            StartCoroutine(Dissolve(delay));
        }

        #endregion

        #region Private Methods

        private IEnumerator Dissolve(float delay)
        {
            if (_dissolving)
            {
                yield break;
            }


            _dissolving = true;

            vfx.Play();
            float counter = 0;
            yield return new WaitForSeconds(delayAmount);

            while (_materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;

                foreach (Material material in _materials)
                {
                    material.SetFloat("_DissolveAmount", counter);
                }

                yield return new WaitForSeconds(refreshRate);
            }
        }

        #endregion
    }
}