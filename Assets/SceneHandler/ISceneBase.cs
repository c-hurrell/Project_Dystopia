using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHandler
{
    public interface ISceneBase
    {
        public string SceneName { get; }
        public void Pause();
        public void Resume();
        public void TempUnload();
        public void LoadAsync(bool overwrite = true);
        public void UnloadAsync();
    }

    /// <summary>
    /// Base class for a possible scene
    /// </summary>
    public abstract class SceneBase<TPauseType> : ISceneBase where TPauseType : SceneObject
    {
        public abstract string SceneName { get; }

        public void Pause()
        {
            var pauseObjects = Object.FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.Pause();
            }
        }

        public void Resume()
        {
            var pauseObjects = Object.FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.Resume();
            }
        }

        public void TempUnload()
        {
            var pauseObjects = Object.FindObjectsOfType<TPauseType>();

            foreach (var pauseObject in pauseObjects)
            {
                pauseObject.TempUnload();
            }
        }

        public void LoadAsync(bool overwrite = true)
        {
            SceneManager.LoadSceneAsync(SceneName, overwrite ? LoadSceneMode.Single : LoadSceneMode.Additive);
        }

        public void UnloadAsync()
        {
            SceneManager.UnloadSceneAsync(SceneName);
        }
    }
}