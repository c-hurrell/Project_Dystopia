using System.Collections.Generic;
using Enemy;
using SceneHandler;
using Stat_Classes;
using UnityEngine;

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
                var playerCharacter = Object.FindObjectOfType<Player_Character>();
                playerCharacter.CopyFrom(playerStatuses[0]);
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