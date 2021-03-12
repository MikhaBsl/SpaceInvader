using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static bool m_DoNotDestroyOnLoad = true;

    private static bool s_ShuttingDown;
    private static object s_Lock = new object();
    private static T s_Instance;

    public static T Instance
    {
        get
        {
            if (s_ShuttingDown)
            {
                Debug.LogError("SingletonMonoBehaviour: Instance \"" + typeof(T) + "\" already destroyed. Returning null.");
                return null;
            }

            lock (s_Lock)
            {
                if (s_Instance == null)
                {
                    T[] instances = FindObjectsOfType<T>();
                    if (instances.Length > 0)
                    {
                        if (instances.Length > 1)
                            Debug.LogError("SingletonMonoBehaviour: There is " + instances.Length + " instances of \"" + typeof(T) + "\"");
                        s_Instance = instances[0];
                    }
                    else
                    {
                        string tName = typeof(T).Name;
                        GameObject tGO = GameObject.Find(tName);
                        if (!tGO)
                            tGO = new GameObject(tName);
                        s_Instance = tGO.AddComponent<T>();
                    }

                    if (m_DoNotDestroyOnLoad)
                        DontDestroyOnLoad(s_Instance.gameObject);
                }
            }
            return s_Instance;
        }
    }

    private void OnApplicationQuit()
    {
        s_ShuttingDown = true;
    }

    private void OnDestroy()
    {
        s_ShuttingDown = true;
    }
}
