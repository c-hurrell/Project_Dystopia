using Enemy;
using SceneHandler;

namespace Combat
{
    public static class CombatManager
    {
        public static EncounterData CurrentEncounter { get; private set; }
        private static Scene _previousScene;

        public static void StartCombat(EncounterData encounterData)
        {
            CurrentEncounter = encounterData;
            _previousScene = GlobalSceneHandler.CurrentScene;
            GlobalSceneHandler.LoadScene(Scene.EnemyBattle);
        }

        public static void EndBattle()
        {
            CurrentEncounter = null;
            GlobalSceneHandler.LoadScene(_previousScene);
        }
    }
}