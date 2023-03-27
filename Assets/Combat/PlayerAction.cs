using System;

namespace Combat
{
    public abstract class PlayerAction
    {
        public abstract void Execute(EnemyBattleStatus[] enemyStatuses, PlayerBattleStatus[] playerStatuses);
    }

    public class AttackAction : PlayerAction
    {
        private Range TargetEnemyIndexes { get; }

        public AttackAction(Range targetEnemyIndexes)
        {
            TargetEnemyIndexes = targetEnemyIndexes;
        }

        public override void Execute(EnemyBattleStatus[] enemyStatuses, PlayerBattleStatus[] playerStatuses)
        {
        }
    }
}