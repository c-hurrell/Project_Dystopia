using SceneHandler;
using UnityEngine;

namespace EventTriggers
{
    public class EnemyBattle : MonoBehaviour
    {
        [SerializeField] private Collider2D trigger;
        

        private void Start()
        {
            if (trigger == null)
            {
                Debug.LogError("Enemy battle trigger is null, assign trigger in editor");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered battle trigger");
                GlobalSceneHandler.LoadScene(Scene.EnemyBattle);
                Destroy(this);
            }
        }
    }
}