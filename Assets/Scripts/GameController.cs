using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public TrailController trailController;
    public List<Bird> Birds;
    public List<Enemy> Enemies;

    private Bird _shotBird;
    public BoxCollider2D tapCollider;

    private bool _isGameEnded = false;
    public Canvas UICanvas;
    private UIController UIControl;

    private void Start()
    {
        UIControl = UICanvas.GetComponent<UIController>();

        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        tapCollider.enabled = false;
        slingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    public void ChangeBird()
    {
        if (_isGameEnded) return;

        Birds.RemoveAt(0);
        if (Birds.Count <= 0 && Enemies.Count > 0)
        {
            Debug.Log("Game over.");
            UIControl.EndGame("LEVEL FAILED", false);
        }

        if(tapCollider != null) tapCollider.enabled = false;

        if(Birds.Count > 0)
        {
            slingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(Enemies.Count <= 0)
        {
            _isGameEnded = true;
            Debug.Log("Game end.");
            UIControl.EndGame("LEVEL CLEARED", true);
        }
    }

    public void AssignTrail(Bird bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    private void OnMouseDown()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}
