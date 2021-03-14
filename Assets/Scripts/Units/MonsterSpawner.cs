using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    bool spawn = false;
    bool stop = false;
    int spawned = 0;
    int maxSpawned = 10;
    Board board;
    IEnumerator spawnLoop;

    public List<Monster> spawnedMonsters = new List<Monster> ();

    public void Initialize (Board board, int maxSpawned) {
        this.board = board;
        this.maxSpawned = maxSpawned;
    }

    public void ResetAllPath () {
        foreach (var monster in spawnedMonsters) {
            monster.ResetPath ();
        }
    }

    // Update is called once per frame
    void Update () {
        if (!spawn) {
            spawnLoop = randomSpawn (1);
            StartCoroutine (spawnLoop);
            spawn = true;
        }

        if (!stop && spawned >= maxSpawned) {
            StopCoroutine (spawnLoop);
            stop = true;
        }
    }

    IEnumerator randomSpawn (float time) {
        while (stop == false) {
            yield return new WaitForSeconds (time);
            spawned++;

            int index = Random.Range (0, 3);
            spawnedMonsters.Add (board.SpawnMonster (index));
        }
    }
}
