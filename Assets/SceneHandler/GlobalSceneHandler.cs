using System;
using System.Collections.Generic;
using Scenes.SceneTypes;
using UnityEngine;

namespace SceneHandler
{
    public static class GlobalSceneHandler
    {
        /// <summary>
        /// List of all loaded scenes
        /// Used for unloading a scene safely
        /// </summary>
        private static readonly List<SceneBase> LoadedScenes = new();

        public static void LoadScene(Scene scene, bool overwrite = true)
        {
            var sceneInstance = NewScene(scene);

            if (overwrite)
            {
                LoadedScenes.Clear();
            }

            LoadedScenes.Add(sceneInstance);

            sceneInstance.LoadAsync(overwrite);
        }

        public static void UnloadScene(Scene scene)
        {
            var sceneInstance = NewScene(scene);

            var loadedScene = LoadedScenes.Find(x => x.SceneName == sceneInstance.SceneName);
            if (loadedScene == null)
            {
                Debug.LogError($"Scene {sceneInstance.SceneName} is not loaded");
                return;
            }

            loadedScene.UnloadAsync();
            LoadedScenes.Remove(loadedScene);
        }

        public static void PauseScene(Scene scene)
        {
            var sceneInstance = NewScene(scene);
            sceneInstance.Pause();
        }

        public static void TempUnloadScene(Scene scene)
        {
            var sceneInstance = NewScene(scene);
            sceneInstance.TempUnload();
        }

        public static void ResumeScene(Scene scene)
        {
            var sceneInstance = NewScene(scene);
            sceneInstance.Resume();
        }

        private static SceneBase NewScene(Scene scene)
        {
            return scene switch
            {
                Scene.TestWorld => new TestWorldScene(),
                Scene.EnemyBattle => new EnemyBattleScene(),
                _ => throw new NotImplementedException()
            };
        }
    }

    public enum Scene
    {
        TestWorld,
        EnemyBattle
    }
}