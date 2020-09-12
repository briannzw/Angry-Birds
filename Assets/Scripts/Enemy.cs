using UnityEngine;
using UnityEngine.Events;

public class Enemy : Obstacle
{
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    private void OnDestroy()
    {
        OnEnemyDestroyed(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (collision.gameObject.tag == "Bird")
        {
            Dead(deadPrefab);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            Damage(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10);
        }
    }
}
