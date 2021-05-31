using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class LevelGenerator : MonoBehaviour {
    // Variables :
    public GameObject player;
    public Transform levelPart_Start;
    public Transform[] levelParts;
    private int lastRandomLevel, newRandomLevel;

    private float playerDistanceBeforeSpawnNewLevelPart = 75f;
    private Vector3 lastEndPosition;

    private float gridGraphPositionY = 69f;

    void Awake() {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        lastRandomLevel = 0;

        // SpawnLevelPart on start gameplay :
        // int startingLevelPartsSpawn = 5;
        // for (int i = 0; i < startingLevelPartsSpawn; i++) {
        //     SpawnLevelPart();
        // }
    }

    void Update() {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < playerDistanceBeforeSpawnNewLevelPart) {
            SpawnLevelPart();
        }
    }
    
    // Spawn a new LevelPart :
    private void SpawnLevelPart() {
        // Disable the double :
        while (lastRandomLevel == newRandomLevel) {
            newRandomLevel = Random.Range(0, levelParts.Length);
        }

        Transform randomLevelPart = levelParts[newRandomLevel];
        Transform lastLevelPartTransform = SpawnLevelPart(randomLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        lastRandomLevel = newRandomLevel;

        // Update the Grid Graph :
        var gridGraph = AstarPath.active.data.gridGraph;

        gridGraphPositionY -= 25f;
        gridGraph.center = new Vector3(0, gridGraphPositionY, 0);

        AstarPath.active.Scan();
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition) {
        return Instantiate(levelPart, spawnPosition, Quaternion.identity);
    }
}
