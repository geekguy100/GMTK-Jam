/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

namespace EnemyAI
{
    public abstract class RngCheck : CheckState
    {
        protected override bool CheckPass()
        {
            float passingPercent = PreparePassingPercent();
            return GetRandomPercent() >= (1 - passingPercent);
        }

        protected abstract float PreparePassingPercent();
        protected abstract float GetRandomPercent();
    }
}