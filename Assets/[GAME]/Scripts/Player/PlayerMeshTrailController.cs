using System.Collections;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerMeshTrailController : Bear
    {
        #region Serialized Fields

        [Header("Configurations")] [SerializeField]
        private float activeTime = 2f;

        [SerializeField] private float meshRefreshRate = .1f;
        [SerializeField] private float meshDestroyDelay = 1f;
        [SerializeField] private float shaderVariableRate = .1f;
        [SerializeField] private float shaderVariableRefreshRate = .05f;

        [Header("Components")] [SerializeField]
        private SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeField] private Transform transformToPosition;
        [SerializeField] private Material material;

        #endregion

        #region Private Variables

        private bool _isTrailActive;
        private const string shaderReference = "_Alpha";

        #endregion

        #region Public Methods

        public void ActivateTrail()
        {
            if (_isTrailActive)
            {
                return;
            }

            _isTrailActive = true;

            StartCoroutine(ActivateTrailCoroutine(activeTime));
        }

        #endregion

        #region Private Methods

        private IEnumerator ActivateTrailCoroutine(float timeActive)
        {
            while (timeActive > 0)
            {
                timeActive -= meshRefreshRate;

                GameObject obj = new();
                obj.transform.SetPositionAndRotation(transformToPosition.position, transformToPosition.rotation);
                MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
                MeshFilter meshFilter = obj.AddComponent<MeshFilter>();

                Mesh mesh = new();
                skinnedMeshRenderer.BakeMesh(mesh);

                meshFilter.mesh = mesh;

                meshRenderer.materials = new[] { material, material, material };

                StartCoroutine(AnimateMaterialFloat(meshRenderer.materials, 0, shaderVariableRate,
                    shaderVariableRefreshRate));

                Destroy(obj, meshDestroyDelay);

                yield return new WaitForSeconds(meshRefreshRate);
            }

            _isTrailActive = false;
        }

        private IEnumerator AnimateMaterialFloat(Material[] mats, float goal, float rate, float refreshRate)
        {
            float valueToAnimate = mats[0].GetFloat(shaderReference);

            while (valueToAnimate > goal)
            {
                valueToAnimate -= rate;
                foreach (Material mat in mats)
                {
                    mat.SetFloat(shaderReference, valueToAnimate);
                }

                yield return new WaitForSeconds(refreshRate);
            }
        }

        #endregion
    }
}