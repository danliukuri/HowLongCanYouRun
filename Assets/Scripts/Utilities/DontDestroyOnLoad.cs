using UnityEngine;

namespace Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        static DontDestroyOnLoad instance;
        void Awake()
        {
            if (instance is null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}