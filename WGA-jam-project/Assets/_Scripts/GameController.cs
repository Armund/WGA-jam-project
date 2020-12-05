using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float nextSpawnTime;

    [SerializeField] private GameObject gameField;

    [SerializeField] private GameObject energyPrefab;

    [SerializeField] private float spawnDelay;

    [SerializeField] private int maxSpheres;

    private int currentSpheres;

    private float sizeX;

    private float sizeZ;

    private Vector3 center;

    private float stepFromWalls = 5.0f;

    protected void Start()
    {
        
    }

    public void OnEnable()
    {
        center = gameField.GetComponent<MeshRenderer>().bounds.center;

        sizeX = gameField.GetComponent<MeshRenderer>().bounds.extents.x - stepFromWalls;

        sizeZ = gameField.GetComponent<MeshRenderer>().bounds.extents.z - stepFromWalls;

        nextSpawnTime = Time.time + spawnDelay;
        // Draw bounds
        /*CreateEnergySphere(center - new Vector3(sizeX, 0, sizeZ) + Vector3.up);
        CreateEnergySphere(center - new Vector3(sizeX, 0, -sizeZ) + Vector3.up);
        CreateEnergySphere(center - new Vector3(-sizeX, 0, sizeZ) + Vector3.up);
        CreateEnergySphere(center - new Vector3(-sizeX, 0, -sizeZ) + Vector3.up);*/
    }

    protected void Update()
    {
        if (ShouldSpawn() && (currentSpheres < maxSpheres))
            CreateEnergySphere(GenerateRandomPosition());
    }

    private void CreateEnergySphere(Vector3 position)
    {
        nextSpawnTime = Time.time + spawnDelay;
        Instantiate(energyPrefab, position, transform.rotation);
        currentSpheres += 1;
    }

    private Vector3 GenerateRandomPosition()
    {
        return (center - new Vector3(Random.Range(-sizeX, sizeX), 0, Random.Range(-sizeZ, sizeZ)) + Vector3.up);
    }

    private bool ShouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }
}
