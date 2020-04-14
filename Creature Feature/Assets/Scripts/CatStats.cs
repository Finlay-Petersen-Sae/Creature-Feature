using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStats : MonoBehaviour
{
    public int maxHealth, maxHunger, maxThirst, Ferocity, Sturdiness, maxCleansliness;
    public int curHealth, curHunger, curThirst, curCleansliness;
    public string catName;
    //public GameObject closestfood;
    //Insert an identifier of some sort for personality
    private void Start()
    {
        var PDM = FindObjectOfType<PathDataManager>();
        maxHealth = Random.Range(10, 20);
        maxHunger = 100;
        maxThirst = 100;
        maxCleansliness = 50;
        catName = PDM.catNames[Random.Range(0, PDM.catNames.Count)];
        //ClosestFood();
    }

    //public void ClosestFood()
    //{
    //    //TODO MAKE ONE FOR WATER TOO
    //    float dist = 1000000f;
    //    GameObject idealfood = null;
    //    var PDM = FindObjectOfType<PathDataManager>();
    //    foreach (var item in PDM.FoodObj)
    //    {
    //        if (Vector3.Distance(transform.position, item.transform.position) < dist)
    //        {

    //            idealfood = item;
    //            dist = Vector3.Distance(transform.position, item.transform.position);
    //        }
    //    }
    //    closestfood = idealfood;
    //}
}
