using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float vel;
    Vector3 target;
    NavMeshAgent agent;
    public int damage;
    public float health;

    public void Assing(float _vel, int _damage,float _health)
    {
        vel = _vel;
        damage = _damage;
        health = _health;
        agent.speed = vel;

    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        //target= target - (new Vector3(1, 0, 1) - transform.position);
    }

    private void FixedUpdate()
    {
        agent.SetDestination(target - (new Vector3(3, 0, 3)-transform.position));
    }
}
