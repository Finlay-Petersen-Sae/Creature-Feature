using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDataManager : MonoBehaviour
{


    public List<PathDataNode> pathDataList = new List<PathDataNode>();
    public List<GameObject> FoodObj = new List<GameObject>();
    public List<GameObject> WaterObj = new List<GameObject>();
    public List<GameObject> HumanObj = new List<GameObject>();
    public List<GameObject> CatsObj = new List<GameObject>();
    public List<float> PerlinSample = new List<float>();
    public List<float> RnumberPerlin = new List<float>();
    public int CatLimit, curCatAmount;
    public List<string> catNames = new List<string>();
    public Vector2Int WorldSize;

    public void Start()
    {
        var bootStrap = FindObjectOfType<BootStrap>();
        foreach (var item in GameObject.FindGameObjectsWithTag("Food"))
        {
            FoodObj.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Water"))
        {
            WaterObj.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Human"))
        {
            HumanObj.Add(item);
        }
        if (bootStrap.IsLoadWorld)
        {
            LoadCat();
        }

    }

    public void Update()
    {

        if(curCatAmount < CatLimit)
        {
            FindObjectOfType<PathFinding>().PathMake();
            curCatAmount++;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NukeWorld();
        }
    }

    public void LoadCat()
    {
        Serialization serialization = Serialization.GetInstance();
        curCatAmount = serialization.CatStatsList.Count;
        for (int i = 0; i < serialization.CatStatsList.Count; i++)
        {
                var cat = Instantiate(GetComponent<PathFinding>().Test_Cat, new Vector3(serialization.CatLoc[i].x, serialization.CatLoc[i].y, serialization.CatLoc[i].z), Quaternion.identity);
                var CatData = cat.GetComponent<CatStats>();
                CatData.curHealth = serialization.CatStatsList[i].curHealth;
                CatData.curThirst = serialization.CatStatsList[i].curThirst;
                CatData.curHunger = serialization.CatStatsList[i].curHunger;
                CatData.curCleansliness = serialization.CatStatsList[i].curCleansliness;
                CatData.maxHealth = serialization.CatStatsList[i].maxHealth;
                CatData.maxHunger = serialization.CatStatsList[i].maxHunger;
                CatData.maxThirst = serialization.CatStatsList[i].maxThirst;
                CatData.maxCleansliness = serialization.CatStatsList[i].maxCleansliness;
                CatData.catName = serialization.CatStatsList[i].catName;
                CatsObj.Add(cat);
                
        }
       
    }

    public PathDataNode GetNode(Vector2Int _checkLoc)
    {
        int index = _checkLoc.y * WorldSize.x + _checkLoc.x;

        if (_checkLoc.x < 0 || _checkLoc.y < 0 || _checkLoc.x >= WorldSize.x || _checkLoc.y >= WorldSize.y)
        {
            return null;
        }

        if (index < 0 || index > pathDataList.Count)
        Debug.Log(index + " " + _checkLoc + " " + WorldSize);

        return pathDataList[index];
    }
    // get node from location

    public void WriteToSerialization()
    {

        Serialization serialization = Serialization.GetInstance();
        serialization.Perlinsample.Clear();
        serialization.RnumberPerlin.Clear();
        serialization.CatLoc.Clear();
        serialization.CatStatsList.Clear();
        serialization.Perlinsample = PerlinSample;
        serialization.RnumberPerlin = RnumberPerlin;
        foreach (var item in FindObjectOfType<PathDataManager>().CatsObj)
        {
            var CatStatsSaveData = new CatStatsData();
            var Catstats = item.GetComponent<CatStats>();
            CatStatsSaveData.maxHealth = Catstats.maxHealth;
            CatStatsSaveData.maxThirst = Catstats.maxThirst;
            CatStatsSaveData.maxHunger = Catstats.maxHunger;
            CatStatsSaveData.maxCleansliness = Catstats.maxCleansliness;
            CatStatsSaveData.curHealth = Catstats.curHealth;
            CatStatsSaveData.curThirst = Catstats.curThirst;
            CatStatsSaveData.curHunger = Catstats.curHunger;
            CatStatsSaveData.curCleansliness = Catstats.curCleansliness;
            CatStatsSaveData.catName = Catstats.catName;

            serialization.CatStatsList.Add(CatStatsSaveData);
            var position = new SaveableVector(item.transform.position.x, item.transform.position.y, item.transform.position.z);
            serialization.CatLoc.Add(position);
        }
    }

    public PathFindingNode CreatePathNode(Vector3 _checkLoc)
    {
        Vector3Int.FloorToInt(_checkLoc);
        foreach (var node in pathDataList)
        {
            if (node.worldLocation == _checkLoc)
            {
                var pathNode = new PathFindingNode(node, 0, 0);
                pathNode.Node = node;
                return pathNode;
            }
        }
        return null;
    }
    //get closest node from a location

    public void NukeWorld()
    {
        for (int i = 0; i < CatsObj.Count; i++)
        {
            CatsObj.Remove(CatsObj[i]);
            curCatAmount--;
            Destroy(CatsObj[i]);
            i--;
        }

    }

}
