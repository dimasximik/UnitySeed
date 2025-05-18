using Core.Services;
using Presentation.UI.Controls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.UI.Windows
{
    public class LevelUpWindow : WindowBase
    {
        [SerializeField] Button   speedBtn;
        [SerializeField] Button   dmgBtn;
        [SerializeField] TMP_Text title;

        GameStateService _state;

        [Inject]
        void Construct(GameStateService state) => _state = state;

        void Awake()
        {
            speedBtn.onClick.AddListener(() =>
            {
                _state.UpgradeSpeed();
                Close();
            });
            dmgBtn.onClick.AddListener(() =>
            {
                _state.UpgradeDamage();
                Close();
            });
        }

        public override void Open()
        {
            title.text = $"LEVEL {_state.Player.Level}";
            base.Open();
        }

        public class Factory : PlaceholderFactory<LevelUpWindow> { }
    }
}