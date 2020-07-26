﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSinglton<GameManager>
{

    public int score = 0;
    public int health = 3;
    private bool _isDead = false;
    [SerializeField]
    private List<GameObject> _playerHurt = new List<GameObject>();
    private bool _isChangeTimeDone = false;
    [SerializeField]
    private int _scoreChange;

    private void Update()
    {
        if (health == 0 && _isDead == false)
        {
            playerDie();
        }

        if(_isDead == true)
        {
            reloadScene();
        }

        if(score >= _scoreChange && _isChangeTimeDone == false)
        {
            _scoreChange += 50;
            SpawnManager.Instance.ChangeSpawnTime();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ChangeTimeBool()
    {
        _isChangeTimeDone = true;
    }

    private void reloadScene()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void playerDie()
    {
        _isDead = true;
        UIManager.Instance.showGameOver();
        SpawnManager.Instance.IsPlayerDead();
        Destroy(GameObject.Find("Player"));
        AudioManager.Instance.ExplosionPlay();
    }

    public void increaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UIManager.Instance.scoreTextUpdate();
    }

    public void PlayerHurt()
    {
        bool isDone = false;
        do
        {
            int i = Random.Range(0, 2);
            if (_playerHurt[i].activeInHierarchy == false)
            {
                _playerHurt[i].SetActive(true);
                isDone = true;
                break;
            }
            else if(health == 0)
            {
                isDone = true;
            }
        } while (isDone == false);
    }
}