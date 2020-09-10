using UnityEngine;
using UnityEngine.Events;

public class Enemy : Obstacle
{
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    private bool _isHit = false;

    private void OnDestroy()
    {
        if (_isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (collision.gameObject.tag == "Bird")
        {
            _isHit = true;
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            _isHit = true;
            Damage(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10);
        }
    }
}
