using Enemy;
using SceneHandler;

namespace Combat
{
    public static class CombatManager
    {
        public static EncounterData CurrentEncounter { get; private set; }

        public static void StartCombat(EncounterData encounterData)
        {
            CurrentEncounter = encounterData;
            GlobalSceneHandler.LoadScene(Scene.EnemyBattle);
        }
    }
}