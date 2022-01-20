using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSpawnPoint : MonoBehaviour
{
    public static List<PaddleSpawnPoint> spawnPoints;


    private void Awake() {
        if(spawnPoints != null) return;

        spawnPoints = new List<PaddleSpawnPoint>();
    }

    private void OnEnable() {
        spawnPoints.Add(this);
    }

    private void OnDisable() {
        spawnPoints.Remove(this);
    }

}
