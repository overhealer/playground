using overhealer.Core;
using UnityEngine;

namespace playground
{
    [CreateAssetMenu(menuName = "Configs/LevelConfig")]
    public class LevelsConfig :
        BaseConfig<LevelsConfig>
    {
        public float Gravity = -9.81f;
    }
}
