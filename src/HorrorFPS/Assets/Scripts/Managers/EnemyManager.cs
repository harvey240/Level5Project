using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPointSet
    {
        public List<Transform> spawnPoints;
    }

    private string enemyTag = "Enemy";
    [SerializeField] public List<SpawnPointSet> spawnPointSets = new List<SpawnPointSet>();
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        

    }
}
