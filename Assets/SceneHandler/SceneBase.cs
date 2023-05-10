using System.Collections;

namespace SceneHandler
{
    public abstract class SceneBase
    {
        /// <summary>
        /// Scene name to load
        /// </summary>
        public abstract string SceneName { get; }

        /// <summary>
        /// If the scene should be loaded additively
        /// </summary>
        public virtual bool Additive => false;

        /// <summary>
        /// Coroutine called when the scene is about to load
        /// </summary>
        public virtual IEnumerator Load()
        {
            yield return null;
        }
    }
}