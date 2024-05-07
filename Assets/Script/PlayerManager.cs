using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int vida;
    public int damage;
    public int municion = 30;
    public int cargador=35;
    public GameObject bullet;
    public Transform mouse_position;
    public float bullet_damage;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        mouse_position.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            if (municion > 0)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bull = Instantiate(bullet,mouse_position.position ,new Quaternion(0,0,0,0),this.transform);
        bull.GetComponent<Rigidbody>().AddForce(mouse_position.forward * bullet_damage);
        Destroy(bull,3f);
        municion--;

    }

}
