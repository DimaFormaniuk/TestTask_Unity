using UnityEngine;

namespace Game.UI
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private GameObject _win;
        [SerializeField] private GameObject _lose;

        public void ShowWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.Win:
                    _win.gameObject.SetActive(true);
                    break;
                case WindowType.Lose:
                    _lose.gameObject.SetActive(true);
                    break;
            }
        }
    }

    public enum WindowType
    {
        Win,
        Lose
    }
}
