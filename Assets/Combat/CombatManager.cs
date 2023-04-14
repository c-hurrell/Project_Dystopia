using Enemy;
using SceneHandler;

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

        public static void EndBattle()
        {
            IsInCombat = false;
            CurrentEncounter = null;
            GlobalSceneHandler.UnloadScene(Scene.EnemyBattle);
        }
    }
}