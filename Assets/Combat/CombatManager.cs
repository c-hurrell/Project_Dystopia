using Enemy;
using SceneHandler;
using UnityEditor;
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

        public static void EndBattle(EndBattleStatus status = EndBattleStatus.Normal)
        {
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