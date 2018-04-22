using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePrefabs;

    private GameObject Player;
    private float spawnZ = 0.0f;

    //lenght of the tile
    private float tileLength = 36.0f;
    public int amountOfTiles = 2;
    private float safe = 7; // to wait for deletion , not to delete the road we are currently on
    private int lastRoadPrefabIndex;

    private List<GameObject> activeTiles;

	// Use this for initialization
	void Start () {

        // create 5 instances of every type of road for PoolManager so it can reuse them later
        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            PoolManager.instance.CreatePool(tilePrefabs[i], 4);
        }              

        activeTiles = new List<GameObject>();
        Player = GameObject.FindGameObjectWithTag("Player");

        for (int i=0; i < amountOfTiles; i++)
        {
            //to spawn 1 normal bridge
            if (i < 1)
            {
                SpawnTile(0);
            }
            SpawnTile();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
        // generate tiles as the players goes forward
        if (Player.transform.position.z - safe > (spawnZ - amountOfTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }

	}

    private void SpawnTile (int prefabIndex = -1)
    {
        GameObject tile;
        if (prefabIndex == -1)
        {
            //tile = Instantiate(tilePrefabs[RandomRoadPrefabIndex()]);
            tile = PoolManager.instance.ReuseObject(tilePrefabs[RandomRoadPrefabIndex()],
                tilePrefabs[RandomRoadPrefabIndex()].transform.position, tilePrefabs[RandomRoadPrefabIndex()].transform.rotation);
        }
        else
        {
            //the first prefab to be the normal road 
            //tile = Instantiate(tilePrefabs[prefabIndex]);
            tile = PoolManager.instance.ReuseObject(tilePrefabs[prefabIndex],
               tilePrefabs[prefabIndex].transform.position, tilePrefabs[prefabIndex].transform.rotation);
        }        
        tile.transform.SetParent(this.gameObject.transform); // to child to tile manager for better organization
        tile.transform.position = Vector3.forward * spawnZ;
        tile.transform.position = new Vector3(tile.transform.position.x, -6.21f, tile.transform.position.z);
        spawnZ += tileLength;

        activeTiles.Add(tile); // add the generated gameObject to the Active list so we can track it and destroy it when needed
    }

    private void DeleteTile()
    {
        //Destroy(activeTiles[0]); // don`t destroy object when pooling objects
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
    }

   
    private int RandomRoadPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
        {
            return 0;
        }

        //make sure that random does`t spawn 2 times the same prefab
        int randomIndex = lastRoadPrefabIndex;
        while (randomIndex == lastRoadPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastRoadPrefabIndex = randomIndex;
        return randomIndex;        
    }

}
