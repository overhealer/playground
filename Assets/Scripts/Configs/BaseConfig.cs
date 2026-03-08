using UnityEngine;

namespace playground.Assets.Scripts.Configs
{
    public class BaseConfig<T> :
        ScriptableObject where T : ScriptableObject
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<T>("Configs/" + typeof(T).Name);
                }
                return instance;
            }
        }

        private static T instance;
    }
}