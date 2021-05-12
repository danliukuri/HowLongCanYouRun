using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Singleton that inherits MonoBehaviour.
    /// This implementation guarantees to create one instance only and is thread-safe thanks to locking.
    /// </summary>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool _isShuttingDown; // Check if the class is about to be destroyed
        private static readonly object _lock = new object();
        private static T _instance;

        /// <summary>
        /// Access singleton instance through this propriety
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_isShuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed. Returning null.");
                    return null;
                }
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        // Search for existing instance.
                        _instance = (T)FindObjectOfType(typeof(T));

                        // Create new instance if one doesn't already exist.
                        if (_instance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";

                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                    return _instance;
                }
            }
        }
        private void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }
        private void OnDestroy()
        {
            _isShuttingDown = true;
        }
    }
}