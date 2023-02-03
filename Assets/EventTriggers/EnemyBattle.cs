using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    [SerializeField] private Collider2D _trigger;

    private SceneHandler _sceneHandler;

    private void Start()
    {
        _sceneHandler = FindObjectOfType<SceneHandler>();

        if (_trigger == null)
        {
            Debug.LogError("Enemy battle trigger is null, assign trigger in editor");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered battle trigger");
            _sceneHandler.EnemyBattleStart(Background.Test, new TestTransition());
            Destroy(this);
        }
    }
}