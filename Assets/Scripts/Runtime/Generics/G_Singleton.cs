using UnityEngine;
using UnityEngine.SceneManagement;
public class G_Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
#if UNITY_EDITOR
                DontDestroyOnLoad(Instance.gameObject);
#else
                DontDestroyOnLoad(Instance.gameObject);
#endif
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }
}

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Scene activeScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("ManagersScene"));
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>();
                SceneManager.SetActiveScene(activeScene);
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    //public virtual void Awake()
    //{
    //    if(_instance == null)
    //    {
    //        _instance = this as T;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this);
    //    }
    //}
}
