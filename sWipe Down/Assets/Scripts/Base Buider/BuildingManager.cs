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

    private BuildingSO currentBuilding = null;

    [SerializeField] GameManager gameManager = null;
    [SerializeField] MouseManager mouseManager = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        InitializeGame();
    }

    public void InitializeGame()
    {
        buildingDictionary = new Dictionary<string, GameObject>();

        GameObject go = Instantiate(baseBuilding.buildingPrefab, new Vector3(0.5f, 0.0f, 0.5f), Quaternion.identity);
        buildingDictionary.Add("0 0", go);

        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    public void InstantiateBuilding(Vector3 position)
    {
        int x = (int)(position.x - 0.5f);
        int y = (int)(position.z - 0.5f);

        string key = x.ToString() + " " + y.ToString();

        Debug.Log("Building");

        if (CheckLimits(x, y) && !buildingDictionary.ContainsKey(key))
        {
            GameObject go = Instantiate(currentBuilding.buildingPrefab, position, Quaternion.Euler(0, -90, 0));
            TowerBehaviour tb = go.GetComponent<TowerBehaviour>();
            if (tb != null) { tb.key = key; Debug.Log(key); }
            buildingDictionary.Add(key, go);
            gameManager.SpendCurrency(currentBuilding.buildingCost);

            if (gameManager.Currency < currentBuilding.buildingCost) ChangeCurrentlySelectedBuilding(-1);
        }
    }
    
    private bool CheckLimits(int x, int y)
    {
        if (x >= -2 && x <= 2 && y >= -2 && y <= 2)
            return false;

        return true;
    }

    public void RemoveBuildingFromDictionary(string key)
    {
        buildingDictionary.Remove(key);
    }

    public void ChangeCurrentlySelectedBuilding(int index)
    {
        if (index < 0 || index >= buildingTypes.Count)
        {
            if (mouseManager.objectFollow != null) Destroy(mouseManager.objectFollow);
            currentBuilding = null;
            mouseManager.objectFollow = null;
        }
        else
        {
            Destroy(mouseManager.objectFollow);
            currentBuilding = buildingTypes[index];
            mouseManager.objectFollow = Instantiate(currentBuilding.buildingPrefab);
            mouseManager.objectFollow.tag = "Untagged";
            if (index == 0) {
                mouseManager.objectFollow.GetComponent<TowerBehaviour>().enabled = false;
                mouseManager.objectFollow.GetComponentInChildren<DetectEnemy>().enabled = false;
                mouseManager.objectFollow.GetComponentInChildren<SphereCollider>().enabled = false;
                mouseManager.objectFollow.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
