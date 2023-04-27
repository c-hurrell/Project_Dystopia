using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Combat
{
    public enum PlayerActionType
    {
        Attack,
        Defend
    }

    public abstract class PlayerAction
    {
        public abstract void Execute(IEnumerable<EnemyBattleStatus> enemyStatuses,
            IEnumerable<PlayerBattleStatus> playerStatuses, PlayerBattleStatus playerStatus);
    }

    public class AttackAction : PlayerAction
    {
        private int TargetIndex { get; }

        public AttackAction(int targetIndex)
        {
            TargetIndex = targetIndex;
        }

        public override void Execute(IEnumerable<EnemyBattleStatus> enemyStatuses,
            IEnumerable<PlayerBattleStatus> playerStatuses, PlayerBattleStatus playerStatus)
        {
            var enemyStatus = enemyStatuses.ElementAt(TargetIndex);
            enemyStatus.TakeDamage(playerStatus.attack);

            Debug.Log("Player attacked enemy index " + TargetIndex);
        }
    }

    public class DefendAction : PlayerAction
    {
        public override void Execute(IEnumerable<EnemyBattleStatus> enemyStatuses,
            IEnumerable<PlayerBattleStatus> playerStatuses, PlayerBattleStatus playerStatus)
        {
            playerStatus.Defend();
        }
    }
}