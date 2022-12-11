using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject MenuPanel;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CarBot"))
        {
            GameOverPanel.SetActive(true);
            MenuPanel.SetActive(false);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuGameController>().PauseGame();
            return;
        }
    }
}
