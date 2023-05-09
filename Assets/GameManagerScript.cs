using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private World.Area currentArea;

    [SerializeField] private GameObject enemyPref;
    
    [SerializeField] [Range(1,10)] private int areaLevel;

    [SerializeField] private Vector2 maxGridBound;

    [SerializeField] [Range(1,500)]  private int amountEncounters;
    // Start is called before the first frame update
    private void Start()
    {
        GenerateEncounters();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void GenerateEncounters()
    {
        for (var i = 0; i <= amountEncounters; i++)
        {
            Vector2 pos;
            pos.x = Random.Range(-maxGridBound.x, maxGridBound.x);
            pos.y = Random.Range(-maxGridBound.y, maxGridBound.y);

            var encounterSpot = Instantiate(enemyPref) as GameObject;
            encounterSpot.GetComponent<SpriteRenderer>().enabled = false;

            encounterSpot.transform.position = pos;
        }
    }
}
