using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Presentation.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] Button playBtn;
        [SerializeField] Button quitBtn;

        void Awake()
        {
            playBtn.onClick.AddListener(() => SceneManager.LoadScene("Game"));
            quitBtn.onClick.AddListener(Application.Quit);
        }
    }
}