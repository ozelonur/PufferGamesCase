using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Core.Manager
{
    public class Manager<T> : Bear where T : Bear
    {
        private static T _instance;
        private static object _lock = new();
    
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));
    
                        if (_instance == null)
                        {
                            Debug.LogError("[Singleton] An instance of " + typeof(T) +
                                           " is needed in the scene, but there is none.");
                        }
                        else if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went very wrong " +
                                           " - there should never be more than 1 singleton! Reopening the scene might fix it.");
                        }
                    }
                }
                return _instance;
            }
        }
    
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Debug.LogError("[Singleton] Attempting to create another instance of singleton class " +
                               typeof(T) + ". Destroying the new one.");
                Destroy(gameObject);
            }
        }
    
        public static bool IsInitialized => _instance != null;
    }
}
