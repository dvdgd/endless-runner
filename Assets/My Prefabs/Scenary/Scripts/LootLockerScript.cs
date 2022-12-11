using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class LootLockerScript : MonoBehaviour
{
    public int ID;
    public TextMeshProUGUI[] players;

    private void Awake()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
                Debug.Log("Conectado");
            else
                Debug.Log("Erro na conexão");
        });
    }

    public void EnviarPlacar(string name, int score)
    {
        LootLockerSDKManager.SubmitScore(name, score, ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Enviado");
                MostrarPlacar();
            }
            else
                Debug.Log("Erro no envio");
        });
    }

    public void MostrarPlacar()
    {
        int maxPlayers = players.Length;

        LootLockerSDKManager.GetScoreList(ID, maxPlayers, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Erro na placar");
                return;
            }


            LootLockerLeaderboardMember[] leaderboards = response.items;
            for (int i = 0; i < players.Length; i++)
            {
                if (i < leaderboards.Length)
                {
                    players[i].text = leaderboards[i].rank + " " + leaderboards[i].member_id + " - " + leaderboards[i].score;
                }
                else
                {
                    players[i].text = (i + 1).ToString() + " sem rank";
                }
            }
        });
    }
}


