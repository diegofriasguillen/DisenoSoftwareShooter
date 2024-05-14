using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float vel;
    public GameObject target;
    public Transform Pistol;
    public GameObject bullet;
    public EnemyPool pool;
    public Slider slider;

    public int damage;
    public float health;

    NavMeshAgent agent;
    [SerializeField] float distancia;
    [SerializeField] float velAtt;
    [SerializeField] float frecuencia = 1.5f;
    public bool atacando;

    private bool canShoot = true;
    private float shootCooldown = 1.5f;

    public void Assing(float _vel, int _damage, float _health, EnemyPool _pool)
    {
        vel = _vel;
        damage = _damage;
        health = _health;
        agent.speed = _vel;
        pool = _pool;
        
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        target = Player;
    }
    private void Start()
    {
        StartCoroutine(atacar());
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = health;
    }

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        distancia = Vector3.Distance(transform.position, target.transform.position);
        if (Active())
        {
            agent.SetDestination(target.transform.position);
        }
      
       
        

    }

    void Att()
    {
        GameObject bull = Instantiate(bullet, Pistol.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            Vector3 direction = (Pistol.forward).normalized;
            bulletRigidbody.velocity = direction * velAtt;
        }
        Destroy(bull, 3f);
        StartCoroutine(ShootCooldown());
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    IEnumerator atacar()
    {
        if (Distance())
        {

            yield return new WaitForSeconds(frecuencia/2);
            yield return new WaitForSeconds(frecuencia/2);
            StartCoroutine(atacar());
        }
        else
        {
            Debug.Log("No hay suficiente distancia");
            yield return new WaitForSeconds(frecuencia);
            StartCoroutine(atacar());
        }
    }

    public void GetDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Daño Recibido por: " + gameObject.name);

        if (health <= 0)
        {
            PlayerManager.instance.AddHealth(15);
            Destroy(gameObject);
        }
        slider.value = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damage")
        {
            GetDamage(10f);
        }
    }

    bool Active()
    {
        if (pool.activo && target != null)  return true;
        else
        {
            return false;
        }
    }

    bool Distance()
    {
        return distancia < 6;
    }
}
