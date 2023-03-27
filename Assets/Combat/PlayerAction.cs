using System;
using System.Collections.Generic;

namespace Combat
{
    public abstract class PlayerAction
    {
        public abstract void Execute(IEnumerable<EnemyBattleStatus> enemyStatuses,
            IEnumerable<PlayerBattleStatus> playerStatuses);
    }

    public class AttackAction : PlayerAction
    {
        private Range TargetEnemyIndexes { get; }

        public AttackAction(Range targetEnemyIndexes)
        {
            TargetEnemyIndexes = targetEnemyIndexes;
        }

        public override void Execute(IEnumerable<EnemyBattleStatus> enemyStatuses,
            IEnumerable<PlayerBattleStatus> playerStatuses)
        {
        }
    }
}