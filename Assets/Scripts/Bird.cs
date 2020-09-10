using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown, HitSomething }
    public Rigidbody2D Rigidbody;
    public CircleCollider2D Collider;

    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<Bird> OnBirdShot = delegate { };

    private BirdState _state;

    public BirdState State { get { return _state; } }

    private float _minVelocity = .05f;
    private bool _flagDestroy = false;

    private void Start()
    {
        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Collider.enabled = false;
        _state = BirdState.Idle;
    }

    private void FixedUpdate()
    {
        //velocity berubah ketika dilempar
        if (_state == BirdState.Idle && Rigidbody.velocity.sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }

        //setelah dilempar
        if ((_state == BirdState.Thrown || _state == BirdState.HitSomething) && Rigidbody.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            _flagDestroy = true;
            StartCoroutine(DestroyAfter(2f));
        }
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 _target, GameObject _parent)
    {
        gameObject.transform.SetParent(_parent.transform);
        gameObject.transform.position = _target;
    }

    public void Shoot(Vector2 _velocity, float _distance, float _speed)
    {
        Collider.enabled = true;
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody.velocity = _velocity * _speed * _distance;
        OnBirdShot(this);
    }

    private void OnDestroy()
    {
        if (_state == BirdState.Thrown || _state == BirdState.HitSomething)
            OnBirdDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit();
    }

    public virtual void OnHit()
    {
        _state = BirdState.HitSomething;
    }

    public virtual void OnTap() //agar dapat dilakukan override
    {
        //Do Nothing
    }
}
