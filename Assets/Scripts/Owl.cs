using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Bird
{
    public float _radius = 1f;
    public float _explodeForce = 100;
    public float _damage = 15f;
    public bool _hasExplode = false;

    public LayerMask layerHit;
    public GameObject explosionPrefab;

    private IEnumerator Explode(float seconds)
    {
        if(!_hasExplode)
        {
            _hasExplode = true;

            SpriteRenderer _renderer = GetComponent<SpriteRenderer>();

            StartCoroutine(ColorLerp(_renderer, seconds * 1.5f));

            yield return new WaitForSeconds(seconds);

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _radius, layerHit);

            foreach (Collider2D obj in objects)
            {
                Vector2 direction = obj.transform.position - transform.position;
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

                if(rb != null)
                    rb.AddForce(direction * _explodeForce);

                Obstacle obs = obj.GetComponent<Obstacle>();

                if (obs != null)
                {
                    float proximity = (transform.position - obj.transform.position).magnitude;
                    float effect = 1 - (proximity / _radius);
                    effect = Mathf.Clamp(effect, .3f, 1f);
                    //Debug.Log((_damage * effect);
                    obs.Damage(_damage * effect);
                }
            }

            Destroy(gameObject);
        }
    }

    IEnumerator ColorLerp(SpriteRenderer _renderer, float seconds)
    {
        for(float t = 0.01f; t < seconds; t += 0.1f)
        {
            _renderer.material.color = Color.Lerp(Color.white, Color.red, t / seconds);
            yield return null;
        }
    }

    public override void OnHit()
    {
        base.OnHit();
        StartCoroutine(Explode(2f));
    }

    public override void OnTap()
    {
        StartCoroutine(Explode(.1f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
