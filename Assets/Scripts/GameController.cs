using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public List<Birds> Birds;
    public List<Enemy> Enemies;
    private Birds _shotBird;
    public TrailController TrailController;

    //Yellow Bird Boost

    public BoxCollider2D TapCollider;

    //Game Berakhir
    private bool _isGameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        SlingShooter.InitiateBird(Birds[0]);

        //Delegate burung baru pada ketapel
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        //Yellow Bird
        TapCollider.enabled = false;
        _shotBird = Birds[0];
    }
    public void AssignTrail(Birds bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
       
        //Yellow Bord
        TapCollider.enabled = true;
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
            SlingShooter.InitiateBird(Birds[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }
    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}
