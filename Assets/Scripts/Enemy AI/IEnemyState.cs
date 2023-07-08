namespace EnemyAI
{
    public interface IEnemyState : IState
    {
        void OnPursued();
        void OnHit(DamageData damageData);
        string GetStateName();
    }

    public interface IState
    {
        void OnStateEnter();
        void OnStateExit();
    }
}