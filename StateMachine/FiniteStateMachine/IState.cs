namespace Com.PI.StateMachine {
    public interface IState {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}