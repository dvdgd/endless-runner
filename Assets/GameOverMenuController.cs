using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    public TextMeshProUGUI suaPontuacao;
    public GameObject nomeInput;
    private LootLockerScript lootLocker;

    private int score;

    void Awake()
    {
        var scoreText = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pontuacao>().ScoreExibition_txt.text;
        lootLocker = GameObject.FindGameObjectWithTag("GameController").GetComponent<LootLockerScript>();

        score = int.Parse(scoreText); 
        suaPontuacao.text += " " + scoreText;

        lootLocker.MostrarPlacar();
    }

    public void Enviar()
    {
        string nome = nomeInput.GetComponent<TMP_InputField>().text;
        if (nome.Length <= 0)
        {
            return;
        }

        lootLocker.EnviarPlacar(nome, score);
    }
}
