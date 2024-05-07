using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float vel;
    public GameObject target;
    public GameObject bullet;
    public EnemyPool pool;

    public int damage;
    public float health;

    NavMeshAgent agent;
    [SerializeField] float distancia;
    [SerializeField] float velAtt;
    [SerializeField] float frecuencia = 1.5f;
    public bool atacando;

    public void Assing(float _vel, int _damage,float _health)
    {
        vel = _vel;
        damage = _damage;
        health = _health;
        agent.speed = _vel;

    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        target = Player;
    }
    private void Start()
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

    IEnumerator atacar()
    {
        if (Distance())
        {

            yield return new WaitForSeconds(frecuencia/2);
            GameObject temp_bullet = Instantiate(bullet,this.transform);
            Rigidbody rb = temp_bullet.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * velAtt * 30);
            
            Destroy(temp_bullet, 3.75f);
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

    void GetDamage()
    {
        health -= PlayerManager.instance.damage;
        Debug.Log("Daño Recibido por: " + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damage")
        {
            GetDamage();
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
