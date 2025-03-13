using Game.Common;
using R3;

namespace Game.GameSystems
{
    public interface IGameStateScheduler
    {
        public ReadOnlyReactiveProperty<GameState> StateObservable { get; }
        public GameState CurrentState { get; }
        
        public void ChangeState(GameState state);
    }
}