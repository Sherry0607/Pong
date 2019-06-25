using UnityEngine;

namespace DAP
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {

        public static T instance;

        public static T GetSingleton()
        {
            if (!instance)
            {
                GameObject singleton = new GameObject(typeof(T).Name);
                if (!singleton)
                    throw new System.NullReferenceException();

                instance = singleton.AddComponent<T>();
                if (Application.isPlaying)
                    GameObject.DontDestroyOnLoad(singleton);
            }

            return instance;
        }
    }
}