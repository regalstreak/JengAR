using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T m_Instance = null;
    public static T Instance {
        get {
            if (m_Instance == null) {
                m_Instance = FindObjectOfType<T>();
                
                // fallback, might not be necessary.
                if (m_Instance == null)
                    m_Instance = new GameObject(typeof(T).Name).AddComponent<T>();
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }
}
