using System.Collections;
using SceneHandler;
using Scenes.WorldObjectTypes;
using UnityEngine;

namespace Scenes.SceneTypes
{
    public class EnemyBattleScene : TransitionScene<EnemyBattleSceneObject>
    {
        public override string SceneName => "EnemyBattle";

        protected override ISceneTransition LoadTransition => new TestTransition();
        protected override ISceneTransition UnloadTransition => new TestTransition();
    }

    public class TestTransition : ISceneTransition
    {
        public IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(1f);
            IsDone = true;
        }

        public bool IsDone { get; private set; }
    }
}