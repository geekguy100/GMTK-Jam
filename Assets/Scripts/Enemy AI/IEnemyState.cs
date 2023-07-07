namespace EnemyAI
{
    public interface IEnemyState : IState
    {
        void OnPursued();
        void OnHit(object sender);
        string GetStateName();
    }

    public interface IState
    {
        void OnStateEnter();
        void OnStateExit();
    }
}