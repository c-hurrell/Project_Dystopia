using System.Collections;
using SceneHandler;
using UnityEngine;

namespace Scenes.SceneTypes
{
    public class EnemyBattleScene : SceneBase
    {
        public override string SceneName => "EnemyBattle";

        public override IEnumerator Load()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Enemy battle scene loaded");
        }
    }
}