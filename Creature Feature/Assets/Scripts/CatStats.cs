using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStats : MonoBehaviour
{
    public int maxHealth, maxHunger, maxThirst, maxCleansliness;
    public int curHealth, curHunger, curThirst, curCleansliness;
    public int Ferocity, Sturdiness, Survival, Friendliness;
    public bool LookingForHuman = false;
    public string catName;
    //public GameObject closestfood;
    //Insert an identifier of some sort for personality
    private void Start()
    {
        var PDM = FindObjectOfType<PathDataManager>();
        maxHealth = Random.Range(40, 60);
        maxHunger = 100;
        maxThirst = 100;
        maxCleansliness = 50;
        curHealth = maxHealth;
        curHunger = 0;
        curThirst = 0;
        curCleansliness = 0;
        catName = PDM.catNames[Random.Range(0, PDM.catNames.Count)];
        //ClosestFood();
        StartCoroutine(LowerStats());
    }

    private IEnumerator LowerStats()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2);
            if (curHunger > 75 || curThirst > 75 || curCleansliness > 45)
            {
                curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
                curHealth -= (Random.Range(1, 2));
            }
            if (curHunger <= 100)
            {
                curHunger = Mathf.Clamp(curHunger, 0, maxHunger);
                curHunger += Random.Range(1, 6);
            }
            if (curThirst <= 100)
            {
                curThirst = Mathf.Clamp(curThirst, 0, maxThirst);
                curThirst += Random.Range(1, 6);
            }
            if (curCleansliness <= 50)
            {
                curCleansliness = Mathf.Clamp(curCleansliness, 0, maxCleansliness);
                curCleansliness += Random.Range(1, 3);
            }
            if(curHealth <= 0)
            {
                Debug.Log("Dead cat");
                FindObjectOfType<PathDataManager>().CatsObj.Remove(this.gameObject);
                FindObjectOfType<PathDataManager>().curCatAmount--;
                Destroy(this.gameObject);
            }

            var lookforhuman = Random.Range(0, 100);
            if(lookforhuman >= 90 && !LookingForHuman)
            {
                LookingForHuman = true;
            }
            
        }
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
