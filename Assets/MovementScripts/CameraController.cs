using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime;
//using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject player;
    // The Target lowest points it can go to
    [SerializeField] Vector2 minBound;
    // The highest point it can go to
    [SerializeField] Vector2 maxBound;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.z = -10;

        playerPos.x = Mathf.Clamp(playerPos.x, minBound.x, maxBound.x);
        playerPos.y = Mathf.Clamp(playerPos.y, minBound.y, maxBound.y);


        camera.transform.position = playerPos;
    }
}
