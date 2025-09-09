using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;


    public static T Instance
    {
        get
        {
            // In the Editor, static fields like _instance are cleared on domain reload.
            // If _instance is null, try to find the object in the scene.
            // This helps keep the singleton reference valid during Play Mode debugging.
        #if UNITY_EDITOR
            if (_instance == null)
            {
                _instance = Object.FindFirstObjectByType<T>();
            }
        #endif

            return _instance;
        }
    }


    protected void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(Instance.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
