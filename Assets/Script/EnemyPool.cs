using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] bool activo;
    [SerializeField] bool lanz;
    public List<GameObject> enemyPool;
    public Transform[] SpawnPoints;
    [Header("Variables Enemy")]
    public float health;
    public float velocity;
    public int damage;

    private void Update()
    {
        if (activo && !lanz)
        {
            lanz = true;
            StartCoroutine(spawn());

        }
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(1);
        if (enemyPool.Count >0)
        {
            GameObject EnemyTemp=Instantiate(enemyPool[0], SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)].position, new Quaternion(0, 0, 0, 0));
            EnemyTemp.GetComponent<Enemy>().Assing(velocity, damage,health);
            enemyPool.RemoveAt(0);

        StartCoroutine(spawn());
        }
        else
        {
            Debug.Log("Terminada la list");
        }
    }
}
