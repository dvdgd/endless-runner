using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCarController : MonoBehaviour
{
    public float velocidade;

    private float initalYPosition;
    private GameObject gameController;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;

        gameController = GameObject.FindWithTag("GameController");
        velocidade = Random.Range(15, 20);
        initalYPosition = this.gameObject.transform.position.y;
    }

    void FixedUpdate()
    {
        MoveCar();
        CheckIfIsToBeDestroyed();
    }

    private void MoveCar()
    {
        gameObject.transform.position += Time.deltaTime * velocidade * Vector3.forward;
    }

    private void CheckIfIsToBeDestroyed()
    {
        float errorMargin = 1f;
        float currentYPosition = this.gameObject.transform.position.y + errorMargin;

        if (currentYPosition < initalYPosition)
        {
            gameController.GetComponent<SpawnPlatform>().RemoveInstatiatedCar(gameObject);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            return;
        }

        GameObject otherCar = other.gameObject;
        if (otherCar == null || !otherCar.CompareTag("CarBot"))
        {
            return;
        }

        float otherCarVelocity = otherCar.GetComponent<BotCarController>().velocidade;
        float otherDistance = other.transform.position.z;
        float thisPosition = gameObject.transform.position.z;

        if (otherDistance < thisPosition)
        {
            this.velocidade = otherCarVelocity * 1.2f;
        }
    }
}
