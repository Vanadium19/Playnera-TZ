using Game.Common;
using R3;

namespace Game.GameSystems
{
    public class GameStateScheduler : IGameStateScheduler
    {
        private readonly ReactiveProperty<GameState> _currentState = new(GameState.CalmState);

        public ReadOnlyReactiveProperty<GameState> StateObservable => _currentState;
        public GameState CurrentState => _currentState.CurrentValue;

        public void ChangeState(GameState state)
        {
            _currentState.Value = state;
        }
    }
}