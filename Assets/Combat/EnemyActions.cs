using System.Collections.Generic;
using Utils;

namespace Combat
{
    public abstract class EnemyAction
    {
        public abstract void Execute(IEnumerable<EnemyBattleStatus> enemyStatuses,
            IEnumerable<PlayerBattleStatus> playerStatuses, EnemyBattleStatus enemyStatus);
    }

    public class EnemyAttackAction : EnemyAction
    {
        public override void Execute(IEnumerable<EnemyBattleStatus> enemyStatuses,
            IEnumerable<PlayerBattleStatus> playerStatuses, EnemyBattleStatus enemyStatus)
        {
            var playerStatus = playerStatuses.RandomElement();
            playerStatus.health -= enemyStatus.attack;
        }
    }
}