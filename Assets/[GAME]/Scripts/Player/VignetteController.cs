using _GAME_.Scripts.GlobalVariables;
using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;
using UnityEngine.Rendering;
using Vignette = UnityEngine.Rendering.Universal.Vignette;

namespace _GAME_.Scripts.Player
{
    public class VignetteController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Volume volume;

        #endregion

        #region Private Variables

        private Vignette _vignette;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (!volume.profile.TryGet(out _vignette))
            {
                Debug.LogError("Vignette not found properly!");
            }
            else
            {
                _vignette.intensity.value = 0f;
            }
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.GetHit, GetHit);
            }

            else
            {
                Unregister(CustomEvents.GetHit, GetHit);
            }
        }

        private void GetHit(object[] arguments)
        {
            DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, .5f, .4f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => 
                {
                    DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, 0, .4f)
                        .SetEase(Ease.InOutQuad);
                });
        }

        #endregion
    }
}