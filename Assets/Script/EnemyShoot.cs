using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform player;
    public float shootingRange = 10f;
    public float bulletSpeed = 5f;
    public float bulletLifetime = 10f;
    public float delayDisparos = 1f; // Tiempo en segundos entre disparos
    private float tiempoDespuesDisparo = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        tiempoDespuesDisparo += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.position) <= shootingRange && tiempoDespuesDisparo >= delayDisparos)
        {
            ShootAtPlayer();
            tiempoDespuesDisparo = 0f;
        }
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }
}
