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

        private void Start()
        {
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
            _loadOperation = base.LoadAsync(overwrite);
            _loadOperation.allowSceneActivation = false;
            Transition(LoadTransition);
            return _loadOperation;
        }

        public override AsyncOperation UnloadAsync()
        {
            base.UnloadAsync();
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

        private IEnumerator SceneTransition(ISceneTransition transition)
        {
            yield return new WaitUntil(() => transition.IsDone);
            _loadOperation.allowSceneActivation = true;
        }
    }

    public interface ISceneTransition
    {
        IEnumerator Coroutine();
        bool IsDone { get; }
    }
}