using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    static public BuildingManager instance = null;

    // Buildings
    [SerializeField] BuildingSO baseBuilding = null;
    [SerializeField] List<BuildingSO> buildingTypes = null;
    private Dictionary<string, GameObject> buildingDictionary = null;
    public int currentBuildingType = -1;
    [SerializeField] Transform objectPoolParent = null;
    private List<ObjectPool> objectPools = new List<ObjectPool>();

    private BuildingSO currentBuilding = null;

    [SerializeField] GameManager gameManager = null;
    [SerializeField] MouseManager mouseManager = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        InitializeGame();
    }

    public void InitializeGame()
    {
        buildingDictionary = new Dictionary<string, GameObject>();

        GameObject go = Instantiate(baseBuilding.buildingPrefab, new Vector3(0.5f, 0.0f, 0.5f), Quaternion.identity);

        for (int x = -2; x <= 0; x++)
            for (int y = -2; y <= 0; y++)
                buildingDictionary.Add(x.ToString() + " " + y.ToString(), go);

        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        CreateBuildingPools();
    }

    private void CreateBuildingPools()
    {
        for (int i = 0; i < buildingTypes.Count; i++)
        {
            GameObject GO = Instantiate(new GameObject(), objectPoolParent);
            GO.name = "Building Pool " + i.ToString();

            ObjectPool OP = GO.AddComponent<ObjectPool>();
            OP.SetUpPool(buildingTypes[i].buildingPrefab, buildingTypes[i].initialPoolSize);
            OP.InstantiateInitialObjects();
            objectPools.Add(OP);

            List<GameObject> list = OP.GetAVailableObjectList();

            foreach (GameObject gameObject in list)
            {
                TowerBehaviour TB = gameObject.GetComponent<TowerBehaviour>();
                if (TB != null) TB.SetTowerValue(buildingTypes[i].buildingCost);
            }
        }
    }

    public void InstantiateBuilding(Vector3 position)
    {
        if (currentBuilding == null) return;

        int x = (int)(position.x - 0.5f);
        int y = (int)(position.z - 0.5f);

        string key = x.ToString() + " " + y.ToString();

        if (CheckLimits(x, y) && !buildingDictionary.ContainsKey(key))
        {
            //GameObject go = Instantiate(currentBuilding.buildingPrefab, new Vector3(position.x, 0.0f, position.z), Quaternion.identity);
            GameObject GO = objectPools[currentBuildingType].GetObject();
            GO.transform.position = new Vector3(position.x, 0.0f, position.z);
            GO.tag = "Building";

            TowerBehaviour TB = GO.GetComponent<TowerBehaviour>();
            if (TB != null) TB.key = key;
            TB.SetBuildingTexture();
            buildingDictionary.Add(key, GO);
            gameManager.SpendCurrency(currentBuilding.buildingCost);

            if (gameManager.Currency < currentBuilding.buildingCost) ChangeCurrentlySelectedBuilding(-1);
        }
    }
    
    public bool CheckIfTileOccupied(Vector3 position)
    {
        int x = (int)(position.x - 0.5f);
        int y = (int)(position.z - 0.5f);

        string key = x.ToString() + " " + y.ToString();

        return buildingDictionary.ContainsKey(key);
    }

    public bool CheckLimits(int x, int y)
    {
        Debug.Log(x.ToString() + " " + y.ToString());

        if (x >= -2 && x <= 0 && y >= -2 && y <= 0)
            return false;

        return true;
    }

    public void RemoveBuildingFromDictionary(string key)
    {
        buildingDictionary.Remove(key);
    }

    public void ChangeCurrentlySelectedBuilding(int index)
    {
        if (index < 0 || index >= buildingTypes.Count || index == currentBuildingType)
        {
            currentBuildingType = -1;
            if (mouseManager.objectFollow != null)
                mouseManager.objectFollow.GetComponent<PooledObject>().ReturnObject();
            currentBuilding = null;
            mouseManager.objectFollow = null;
        }
        else
        {
            currentBuildingType = index;

            if (mouseManager.objectFollow != null)
                mouseManager.objectFollow.GetComponent<PooledObject>().ReturnObject();

            currentBuilding = buildingTypes[index];
            mouseManager.objectFollow = objectPools[index].GetObject();
            mouseManager.objectFollow.tag = "Untagged";
            mouseManager.objectFollow.GetComponent<TowerBehaviour>().SetTransparentTexture();

            DisableTowerFollowScripts();
        }
    }

    public void DisableTowerFollowScripts()
    {
        TowerBehaviour TB = mouseManager.objectFollow.GetComponent<TowerBehaviour>();
        if (TB != null) TB.enabled = false;

        DetectEnemy DE = mouseManager.objectFollow.GetComponentInChildren<DetectEnemy>();
        if (DE != null) DE.enabled = false;

        SphereCollider SC = mouseManager.objectFollow.GetComponentInChildren<SphereCollider>();
        if (SC != null) SC.enabled = false;
        
        Collider collider = mouseManager.objectFollow.GetComponent<Collider>();
        if (collider != null) collider.enabled = false;
    }
}
