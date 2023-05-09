using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string nextLevel = "Level2";
    private void OnCollisionEnter()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
