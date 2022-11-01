using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool isLevelCompleted;

    void Start()
    {
        finishSound = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !isLevelCompleted)
        {
            finishSound.Play();
            Invoke("CompleteLevel", 2f);
            isLevelCompleted = true;
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
