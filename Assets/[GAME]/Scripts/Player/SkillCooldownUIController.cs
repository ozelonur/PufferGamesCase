using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Helpers;
using _GAME_.Scripts.Managers;
using DG.Tweening;
using OrangeBear.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _GAME_.Scripts.Player
{
    public class SkillCooldownUIController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private SkillType skillType;

        [SerializeField] private Key skillKey;

        [SerializeField] private Image coolDownImage;

        [SerializeField] private TextMeshProUGUI skillKeyText;
        [SerializeField] private TextMeshProUGUI skillCoolDownText;

        #endregion

        #region Private Methods

        private float _coolDown;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            switch (skillType)
            {
                case SkillType.Dash:
                    _coolDown = DataManager.Instance.GetDashSkillData().coolDown;
                    break;
                case SkillType.Grenade:
                    _coolDown = DataManager.Instance.GetGrenadeSkillData().coolDown;
                    break;
                case SkillType.Shotgun:
                    _coolDown = DataManager.Instance.GetShotgunSkillData().coolDown;
                    break;
                case SkillType.Stun:
                    _coolDown = DataManager.Instance.GetStunSkillData().coolDown;
                    break;
                default:
                    Debug.LogError("Type is not found in the current context!");
                    break;
            }

            skillKeyText.text = skillKey.ToString();

            skillCoolDownText.text = "";
            coolDownImage.gameObject.SetActive(false);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.StartCoolDown, StartCoolDown);
            }

            else
            {
                Unregister(CustomEvents.StartCoolDown, StartCoolDown);
            }
        }

        private void StartCoolDown(object[] arguments)
        {
            SkillType type = (SkillType)arguments[0];

            if (skillType != type)
            {
                return;
            }

            float _timer = _coolDown;

            coolDownImage.fillAmount = 1;
            
            SkillCooldownHelper.RecordSkillUse(skillType);
            
            coolDownImage.gameObject.SetActive(true);
            
            coolDownImage.DOFillAmount(0, _coolDown).SetEase(Ease.Linear).OnComplete(() =>
            {
                coolDownImage.gameObject.SetActive(false);
            });

            DOTween.To(() => _timer, x => _timer = x, 0, _timer).OnUpdate(() =>
            {
                skillCoolDownText.text = $"{_timer:F2}";
            }).SetEase(Ease.Linear);
        }

        #endregion
    }
}