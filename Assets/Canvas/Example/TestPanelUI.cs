using NoodleEater.Canvas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NoodleEater
{
    public class TestPanelUI : CanvasUI
    {
        [SerializeField, BindUI] private Button ChangeTextOne;
        [SerializeField, BindUI] private Button ChangeTextTwo;
        [SerializeField, BindUI] private TMP_Text ChangeTextTarget;

        public override void Initialize()
        {
            base.Initialize();
            ChangeTextOne.onClick.AddListener(() => ChangeTextTarget.SetText("Button One"));
            ChangeTextTwo.onClick.AddListener(() => ChangeTextTarget.SetText("Button Two"));
        }
    }
}
