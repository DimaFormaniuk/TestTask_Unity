using Data;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoadSceneByName : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private Scenes _scenes;

        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            _stateMachine.Enter<LoadSceneState, string>(_scenes.GetDescription());
        }
    }
}