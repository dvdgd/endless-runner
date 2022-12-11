using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private Pontuacao Pontuacao;
    private GameObject gameController;
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        Pontuacao = gameController.GetComponent<Pontuacao>();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CarBot"))
        {
            Pontuacao.dangerousDriving();
        }
    }
}
