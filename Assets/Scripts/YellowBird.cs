using UnityEngine;

public class YellowBird : Bird
{
    [SerializeField] public float _boostForce = 100f;
    public bool _hasBoost = false;

    public GameObject boostPrefab;

    public void Boost()
    {
        if(State == BirdState.Thrown && !_hasBoost)
        {
            Instantiate(boostPrefab, transform.position, Quaternion.identity);
            Rigidbody.AddForce(Rigidbody.velocity * _boostForce);
            _hasBoost = true;
        }
    }

    public override void OnTap()
    {
        Boost();
    }
}
