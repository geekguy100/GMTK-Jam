namespace EnemyAI
{
    public class BackAwayState : EnemyStateBase
    {
        public override void OnStateEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit()
        {
            throw new System.NotImplementedException();
        }
        
        public override void OnPursued()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStunned()
        {
            throw new System.NotImplementedException();
        }
        
        public override void OnHit(object sender)
        {
        }

        public override string GetStateName()
        {
            return nameof(BackAwayState);
        }
    }
}