using System.Collections.Generic;
using Enemy;
using SceneHandler;
using World;

namespace Combat
{
    public static class CombatManager
    {
        public static EncounterData CurrentEncounter { get; private set; }
        public static bool IsInCombat { get; private set; }

        public static void StartCombat(EncounterData encounterData)
        {
            IsInCombat = true;
            CurrentEncounter = encounterData;
            GlobalSceneHandler.LoadScene(Scene.EnemyBattle);
        }

        public static void EndBattle(EndBattleStatus status, List<PlayerBattleStatus> playerStatuses)
        {
            if (status == EndBattleStatus.Normal)
            {
                for (var i = 0; i < ProgressionStatus.PartyMembers.Count; i++)
                {
                    var partyStatus = ProgressionStatus.PartyMembers[i];
                    var playerStatus = playerStatuses[i];

                    partyStatus.CopyFrom(playerStatus);
                }
            }

            IsInCombat = false;
            CurrentEncounter = null;
            GlobalSceneHandler.UnloadScene(Scene.EnemyBattle);

            if (status == EndBattleStatus.GameOver)
            {
                GlobalSceneHandler.LoadScene(Scene.GameOver);
            }
        }
    }

    public enum EndBattleStatus
    {
        Normal,
        GameOver
    }
}