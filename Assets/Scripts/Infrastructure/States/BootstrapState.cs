using Data;
using Infrastructure.Services;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Scenes.Initial.GetDescription(), EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadSceneState, string>(Scenes.Main.GetDescription());
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
        }
    }
}