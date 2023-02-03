using System;
using System.Collections;
using UnityEngine;

namespace SceneHandler
{
    /// <summary>
    /// Base for transitioning scenes
    /// </summary>
    /// <typeparam name="TPauseType"></typeparam>
    public abstract class TransitionScene<TPauseType> : SceneBase<TPauseType>, IDisposable
        where TPauseType : SceneObject
    {
        protected abstract ISceneTransition LoadTransition { get; }
        protected abstract ISceneTransition UnloadTransition { get; }

        private Coroutine _loadTransition;
        private Coroutine _loadTransitionEffect;
        private AsyncOperation _loadOperation;

        protected TransitionScene()
        {
            Instantiate(this);
            DontDestroyOnLoad(this);
        }

        private void ReleaseUnmanagedResources()
        {
            Destroy(this);
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~TransitionScene()
        {
            ReleaseUnmanagedResources();
        }

        public override AsyncOperation LoadAsync(bool overwrite = true)
        {
            Transition(LoadTransition);
            return null;
        }

        public override AsyncOperation UnloadAsync()
        {
            Transition(UnloadTransition);
            return null;
        }

        private void Transition(ISceneTransition transition)
        {
            // we cancel the effect if it's already running
            if (_loadTransition != null)
            {
                StopCoroutine(_loadTransition);
            }

            if (_loadTransitionEffect != null)
            {
                StopCoroutine(_loadTransitionEffect);
            }

            StartCoroutine(transition.Coroutine());
            _loadTransition = StartCoroutine(SceneTransition(transition));
        }

        private static IEnumerator SceneTransition(ISceneTransition transition)
        {
            yield return new WaitUntil(() => transition.IsDone);
        }
    }

    public interface ISceneTransition
    {
        IEnumerator Coroutine();
        bool IsDone { get; }
    }
}