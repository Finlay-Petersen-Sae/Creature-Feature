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
        if (FindObjectOfType<BootStrap>().IsLoadWorld)
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

        for (int i = 0; i < serialization.CatStatsList.Count; i++)
        {
            for (int l = 0; l < serialization.CatLoc.Count; l++)
            {
                var cat = Instantiate(GetComponent<PathFinding>().Test_Cat, new Vector3(serialization.CatLoc[l].x, serialization.CatLoc[l].y, serialization.CatLoc[l].z), Quaternion.identity);
                var destinationset = cat.GetComponent<Character>();
                cat.GetComponent<CatStats>().curHealth = serialization.CatStatsList[i].curHealth;
                cat.GetComponent<CatStats>().curThirst = serialization.CatStatsList[i].curThirst;
                cat.GetComponent<CatStats>().curHunger = serialization.CatStatsList[i].curHunger;
                cat.GetComponent<CatStats>().curCleansliness = serialization.CatStatsList[i].curCleansliness;
                cat.GetComponent<CatStats>().maxHealth = serialization.CatStatsList[i].maxHealth;
                cat.GetComponent<CatStats>().maxHunger = serialization.CatStatsList[i].maxHunger;
                cat.GetComponent<CatStats>().maxThirst = serialization.CatStatsList[i].maxThirst;
                cat.GetComponent<CatStats>().maxCleansliness = serialization.CatStatsList[i].maxCleansliness;
                cat.GetComponent<CatStats>().catName = serialization.CatStatsList[i].catName;
                CatsObj.Add(cat);
                curCatAmount++;
            }
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
        serialization.Perlinsample = PerlinSample;
        serialization.RnumberPerlin = RnumberPerlin;
        foreach (var item in FindObjectOfType<PathDataManager>().CatsObj)
        {
            var CatStatsSaveData = new CatStatsData();
            CatStatsSaveData.maxHealth = item.GetComponent<CatStats>().maxHealth;
            CatStatsSaveData.maxThirst = item.GetComponent<CatStats>().maxThirst;
            CatStatsSaveData.maxHunger = item.GetComponent<CatStats>().maxHunger;
            CatStatsSaveData.maxCleansliness = item.GetComponent<CatStats>().maxCleansliness;
            CatStatsSaveData.curHealth = item.GetComponent<CatStats>().curHealth;
            CatStatsSaveData.curThirst = item.GetComponent<CatStats>().curThirst;
            CatStatsSaveData.curHunger = item.GetComponent<CatStats>().curHunger;
            CatStatsSaveData.curCleansliness = item.GetComponent<CatStats>().curCleansliness;
            CatStatsSaveData.catName = item.GetComponent<CatStats>().catName;

            serialization.CatStatsList.Add(CatStatsSaveData);
            var position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z);
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
