using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float health = 100f;

    public GameObject deadPrefab;

    public virtual void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Dead(deadPrefab);
            Destroy(gameObject);
        }
    }

    public virtual void Dead(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
