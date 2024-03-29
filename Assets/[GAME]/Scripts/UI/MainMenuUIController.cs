using _GAME_.Scripts.GlobalVariables;
using DG.Tweening;
using OrangeBear.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _GAME_.Scripts.UI
{
    public class MainMenuUIController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private GameObject mainMenuPanel;

        [SerializeField] private Button startButton;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            startButton.onClick.AddListener(OnClickStartButton);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.OnGameFailed, OnGameFailed);
            }

            else
            {
                Unregister(CustomEvents.OnGameFailed, OnGameFailed);
            }
        }

        private void OnGameFailed(object[] arguments)
        {
            float delay = (float)arguments[0];

            DOVirtual.DelayedCall(delay, () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex))
                .SetLink(gameObject);
        }

        #endregion

        #region Private Methods

        private void OnClickStartButton()
        {
            mainMenuPanel.SetActive(false);
            Roar(CustomEvents.OnGameStart);
        }

        #endregion
    }
}