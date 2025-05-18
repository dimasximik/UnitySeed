using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }
}