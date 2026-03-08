using TMPro;
using UnityEngine;

namespace playground.Assets.Scripts.Levels.Procedural
{
    public class ProceduralLevelController :
        MonoBehaviour
    {
        [SerializeField]
        private PressureButton mapSizeXIncreaseButton, mapSizeXDecreaseButton;
        [SerializeField]
        private PressureButton mapSizeYIncreaseButton, mapSizeYDecreaseButton;
        [SerializeField]
        private PressureButton mapGenerateButton, mapDeleteButton;
        [SerializeField]
        private TMP_Text currentMapSizeText;

        private ProceduralLevelGenerator procLevel;

        public void Init(ProceduralLevelGenerator level)
        {
            procLevel = level;
            UpdateMapSizeText();
            mapSizeXIncreaseButton.SetTipText("X+");
            mapSizeXIncreaseButton.OnButtonPress += () =>
            {
                level.MapSize.x++;
                UpdateMapSizeText();
            };
            mapSizeXDecreaseButton.SetTipText("X-");
            mapSizeXDecreaseButton.OnButtonPress += () =>
            {
                level.MapSize.x--;
                if (level.MapSize.x < 0)
                    level.MapSize.x = 0;
                UpdateMapSizeText();
            };
            mapSizeYIncreaseButton.SetTipText("Y+");
            mapSizeYIncreaseButton.OnButtonPress += () =>
            {
                level.MapSize.y++;
                UpdateMapSizeText();
            };
            mapSizeYDecreaseButton.SetTipText("Y-");
            mapSizeYDecreaseButton.OnButtonPress += () =>
            {
                level.MapSize.y--;
                if (level.MapSize.y < 0)
                    level.MapSize.y = 0;
                UpdateMapSizeText();
            };

            mapGenerateButton.SetTipText("GENERATE");
            mapGenerateButton.OnButtonPress += level.GenerateLevel;
            mapDeleteButton.SetTipText("DELETE");
            mapDeleteButton.OnButtonPress += level.DestroyLevel;
        }

        private void UpdateMapSizeText()
        {
            currentMapSizeText.text = "Map size: " + procLevel.MapSize;
        }
    }
}