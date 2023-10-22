using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace playground
{
    public class ProceduralLevelControl : MonoBehaviour
    {
        [SerializeField] private PressureButton _mapSizeXIncreaseButton, _mapSizeXDecreaseButton;
        [SerializeField] private PressureButton _mapSizeYIncreaseButton, _mapSizeYDecreaseButton;
        [SerializeField] private PressureButton _mapGenerateButton, _mapDeleteButton;
        [SerializeField] private TMP_Text _currentMapSizeText;

        private ProceduralLevel _procLevel;

        public void Init(ProceduralLevel level)
        {
            _procLevel = level;
            UpdateMapSizeText();
            _mapSizeXIncreaseButton.SetTipText("X+");
            _mapSizeXIncreaseButton.OnButtonPress += () =>
            {
                level.MapSize.x++;
                UpdateMapSizeText();
            };
            _mapSizeXDecreaseButton.SetTipText("X-");
            _mapSizeXDecreaseButton.OnButtonPress += () =>
            {
                level.MapSize.x--;
                if (level.MapSize.x < 0)
                    level.MapSize.x = 0;
                UpdateMapSizeText();
            };
            _mapSizeYIncreaseButton.SetTipText("Y+");
            _mapSizeYIncreaseButton.OnButtonPress += () =>
            {
                level.MapSize.y++;
                UpdateMapSizeText();
            };
            _mapSizeYDecreaseButton.SetTipText("Y-");
            _mapSizeYDecreaseButton.OnButtonPress += () =>
            {
                level.MapSize.y--;
                if (level.MapSize.y < 0)
                    level.MapSize.y = 0;
                UpdateMapSizeText();
            };

            _mapGenerateButton.SetTipText("GENERATE");
            _mapGenerateButton.OnButtonPress += level.GenerateLevel;
            _mapDeleteButton.SetTipText("DELETE");
            _mapDeleteButton.OnButtonPress += level.DestroyLevel;
        }

        private void UpdateMapSizeText()
        {
            _currentMapSizeText.text = "Map size: " + _procLevel.MapSize;
        }
    }
}
