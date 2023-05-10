using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string nextLevel = "Level2";
    [SerializeField] private BoxCollider2D exitBox;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Player entered battle trigger");
        SceneManager.LoadScene(nextLevel);
    }
}
