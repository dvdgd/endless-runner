using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject[] carPrefabs;
    [SerializeField]
    private GameObject[] platformPrefabs;

    private Transform player;
    private float lastPlatformPositionZ = 0.0f;
    private Transform[] instantiatedPlatforms;
    private Transform currentPlatformFinalPoint;
    private Transform[] currentCarSpawnPoints;

    private int currentPlatformIndex = 0; // depois que o jogador passar pela primeira plataforma ela é reciclada.

    [SerializeField]
    private float platformSizeZ = 47.78f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        instantiatedPlatforms = InitializePlatforms().ToArray();

        currentPlatformFinalPoint = instantiatedPlatforms[currentPlatformIndex].GetComponent<PlatformElements>().FinalPoint;
        currentCarSpawnPoints = instantiatedPlatforms[currentPlatformIndex].GetComponent<PlatformElements>().CarSpawnPoints;
        InitializeCar();
    }

    private List<Transform> InitializePlatforms()
    {
        List<Transform> platformsInitialized = new();
        for (int i = 0; i < platformPrefabs.Length; i++)
        {
            float zPosition = i * platformSizeZ;
            Transform instatiatedPlatform = Instantiate(platformPrefabs[i], new Vector3(0, 0, zPosition), transform.rotation).transform;
            platformsInitialized.Add(instatiatedPlatform);
            lastPlatformPositionZ += platformSizeZ;
        }
        return platformsInitialized;
    }

    void Update()
    {
        if (CheckDistancePlayerAndPlatform())
        {
            RecyclePlatform(instantiatedPlatforms[currentPlatformIndex].gameObject);
            CarsController();
        }
    }

    [SerializeField]
    private int recyclePlatformByDistance = 10;
    private bool CheckDistancePlayerAndPlatform()
    {
        float distanceBetweenPlayerAndPlatform = player.position.z - currentPlatformFinalPoint.position.z;
        return distanceBetweenPlayerAndPlatform >= recyclePlatformByDistance;
    }

    private void RecyclePlatform(GameObject platform)
    {
        platform.transform.position = new Vector3(0, 0, lastPlatformPositionZ);
        currentPlatformIndex++;

        if (currentPlatformIndex == platformPrefabs.Length)
        {
            currentPlatformIndex = 0;
        }

        currentPlatformFinalPoint = instantiatedPlatforms[currentPlatformIndex].GetComponent<PlatformElements>().FinalPoint;
        lastPlatformPositionZ += platformSizeZ;
    }

    private List<GameObject> instantiadedCars = new();
    [SerializeField]
    private int maxInstantiadedCars;
    [SerializeField]
    private int minInstantiadedCars = 4;

    private void InitializeCar()
    {
        for (int i = 0; i < currentCarSpawnPoints.Length; i++)
        {
            int randomCarIndex = Random.Range(0, carPrefabs.Length);
            GameObject instantiadedCar = Instantiate(carPrefabs[randomCarIndex], currentCarSpawnPoints[i].transform.position, transform.rotation);
            instantiadedCars.Add(instantiadedCar);
        }
    }

    private void CarsController()
    {
        int spanwCarAtPlatform = currentPlatformIndex + 2;

        if (spanwCarAtPlatform >= instantiatedPlatforms.Length)
        {
            spanwCarAtPlatform = 0;
        }

        currentCarSpawnPoints = instantiatedPlatforms[spanwCarAtPlatform].GetComponent<PlatformElements>().CarSpawnPoints;

        if (instantiadedCars.Count < minInstantiadedCars)
        {
            int randomCarIndex = Random.Range(0, carPrefabs.Length);
            int randomSpawnPoint = Random.Range(0, currentCarSpawnPoints.Length);
            GameObject instantiadedCar = Instantiate(carPrefabs[randomCarIndex], currentCarSpawnPoints[randomSpawnPoint].transform.position, transform.rotation);
            instantiadedCars.Add(instantiadedCar);
        }
    }

    public void RemoveInstatiatedCar(GameObject car)
    {
        instantiadedCars.Remove(car);
    }
}
