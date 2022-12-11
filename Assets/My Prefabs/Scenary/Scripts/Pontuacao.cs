using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    private float timeCount;
    private float playerPontuation;
    public Text ScoreExibition_txt;
    public GameObject PrometeoCar;
    private float carSpeed_pontuation;
    private PrometeoCarController PrometeoCarController;

    void Start()
    {
        PrometeoCarController = PrometeoCar.GetComponent<PrometeoCarController>();
    }

    void Update()
    {
        //GETTING THE CarSpeed VARIABLE FROM ANOTHER GAME OBJECT
        carSpeed_pontuation = PrometeoCarController.carSpeed;


        //TIME INCREMENT
        timeCount += Time.deltaTime;

        //1° pontuação: baseada em tempo: ganha 1 ponto a cada segundo acima de 100km/h
        if(carSpeed_pontuation >= 100)
        {
            playerPontuation += Time.deltaTime;
        }


        ScoreExibition_txt.text = playerPontuation.ToString("F0");
    }

    public void dangerousDriving()
    {
        playerPontuation += 100;
    }
}
