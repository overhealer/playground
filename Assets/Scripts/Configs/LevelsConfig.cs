using playground.Assets.Scripts.Configs;
using UnityEngine;

namespace playground
{
    [CreateAssetMenu(menuName = "Configs/Level Database")]
    public class LevelsConfig : BaseConfig<LevelsConfig>
    {
        public float Gravity = -9.81f;
    }
}
