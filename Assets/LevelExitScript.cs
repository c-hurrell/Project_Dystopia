using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string nextLevel = "Level2";
    //[SerializeField] private BoxCollider2D exitBox;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Player entered battle trigger");
        if (nextLevel == "Level2")
        {
            Vector3 level2Pos;
            level2Pos.x = 0;
            level2Pos.y = 0;
            var transform1 = other.transform;
            level2Pos.z = transform1.position.z;
            transform1.position = level2Pos;
        }
        else if (nextLevel == "Level3")
        {
            Vector3 level3Pos;
            level3Pos.x = 0;
            level3Pos.y = -23;
            var transform1 = other.transform;
            level3Pos.z = transform1.position.z;
            transform1.position = level3Pos;
        }
        else if (nextLevel == "EndScene")
        {
            Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        }
        else if (nextLevel == "Level1")
        {
            Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        }
        SceneManager.LoadScene(nextLevel);
    }
}
