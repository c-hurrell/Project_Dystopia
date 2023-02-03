using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHandler
{
    public abstract class SceneBase
    {
        public abstract string SceneName { get; }
        public abstract void Pause();
        public abstract void Resume();
        public abstract void TempUnload();
        public abstract AsyncOperation LoadAsync(bool overwrite = true);
        public abstract void UnloadAsync();
    }

    /// <summary>
    /// Base class for a possible scene
    /// </summary>
    public abstract class SceneBase<TPauseType> : SceneBase where TPauseType : SceneObject
    {
        public override void Pause()
        {
            var pauseObjects = Object.FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.Pause();
            }
        }

        public override void Resume()
        {
            var pauseObjects = Object.FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.Resume();
            }
        }

        public override void TempUnload()
        {
            var pauseObjects = Object.FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.TempUnload();
            }
        }

        public override AsyncOperation LoadAsync(bool overwrite = true)
        {
            return SceneManager.LoadSceneAsync(SceneName, overwrite ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }

        public override void UnloadAsync()
        {
            SceneManager.UnloadSceneAsync(SceneName);
        }
    }
}