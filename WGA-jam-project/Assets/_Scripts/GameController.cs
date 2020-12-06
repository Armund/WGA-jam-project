using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float nextSpawnTime;

    public GameObject activeGameField;

    public GameObject[] gameField;

    [SerializeField] private GameObject energyPrefab;

    [SerializeField] private float spawnDelay;

    [SerializeField] private int maxSpheres;

    [SerializeField] private Player player;

    private Vector4 bounds;

    public int currentSpheres;

    private float sizeX;

    private float sizeZ;

    private Vector3 center;

    private float stepFromWalls = 2.0f;

    public int score;

    protected void Start()
    {
        
    }

    public void OnEnable()
    {
        activeGameField = gameField[0];
        recalculateBounds();
        locatePlayer();

        score = 0;

        nextSpawnTime = Time.time + spawnDelay;
        // Draw bounds
        /*CreateEnergySphere(center - new Vector3(sizeX, 0, sizeZ) + Vector3.up);
        CreateEnergySphere(center - new Vector3(sizeX, 0, -sizeZ) + Vector3.up);
        CreateEnergySphere(center - new Vector3(-sizeX, 0, sizeZ) + Vector3.up);
        CreateEnergySphere(center - new Vector3(-sizeX, 0, -sizeZ) + Vector3.up);*/
    }

    public void AddScore(int addScore)
    {
        score += addScore;
    }

    public int GetScore()
    {
        return score;
    }

    protected void Update()
    {
        locatePlayer(); // Yes I am calling a function with quite a lot of calculations in Update, which is executed every frame, but there is not very much time left.

        if (ShouldSpawn())
            CreateEnergySphere(GenerateRandomPosition());
    }

    public void CreateEnergySphere(Vector3 position)
    {
        if (currentSpheres < maxSpheres)
        {
            nextSpawnTime = Time.time + spawnDelay;
            Instantiate(energyPrefab, position, transform.rotation);
            currentSpheres += 1;
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        return (center - new Vector3(Random.Range(-sizeX, sizeX), 0, Random.Range(-sizeZ, sizeZ)) + Vector3.up);
    }

    private bool ShouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }

    private void recalculateBounds()
    {
        center = activeGameField.GetComponent<MeshRenderer>().bounds.center;

        sizeX = activeGameField.GetComponent<MeshRenderer>().bounds.extents.x - stepFromWalls;

        sizeZ = activeGameField.GetComponent<MeshRenderer>().bounds.extents.z - stepFromWalls;

        bounds.Set(center.x - sizeX, center.x + sizeX, center.z - sizeZ, center.z + sizeZ);
    }

    private void locatePlayer()
    {
        if ((player.transform.position.x > bounds.x && player.transform.position.x < bounds.y && player.transform.position.z > bounds.z && player.transform.position.z < bounds.w) == false)
            for (int i = 0; i < gameField.Length; ++i)
            {
                Vector3 tempCenter = gameField[i].GetComponent<MeshRenderer>().bounds.center;
                float tempSizeX = gameField[i].GetComponent<MeshRenderer>().bounds.extents.x - stepFromWalls;
                float tempSizeZ = gameField[i].GetComponent<MeshRenderer>().bounds.extents.z - stepFromWalls;
                Vector4 tempBounds = new Vector4(tempCenter.x - tempSizeX, tempCenter.x + tempSizeX, tempCenter.z - tempSizeZ, tempCenter.z + tempSizeZ);
                if (player.transform.position.x > tempBounds.x && player.transform.position.x < tempBounds.y && player.transform.position.z > tempBounds.z && player.transform.position.z < tempBounds.w)
                {
                    activeGameField = gameField[i];
                    recalculateBounds();
                    return;
                }
            }            
    }
}
