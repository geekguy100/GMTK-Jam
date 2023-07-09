/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

namespace EnemyAI
{
    public class IdleState : EnemyStateBase
    {
        public override void OnPursued()
        {
        }

        public override string GetStateName()
        {
            return nameof(IdleState);
        }
    }
}