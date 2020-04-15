using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    private IEnumerator RanSpawn;
    public GameObject humanObj;
    // Start is called before the first frame update
    void Start()
    {
        humanObj = this.transform.GetChild(0).gameObject;
        StartCoroutine(TurnOnandOff());
    }

    private IEnumerator TurnOnandOff()
    {
        while (true)
        {
           //Debug.Log("coroutine active");
            yield return new WaitForSecondsRealtime(Random.Range(5, 15));
            if(humanObj.activeSelf)
            {
                humanObj.SetActive(false);
                //Debug.Log("human set to inactive");
            }
            else if (!humanObj.activeSelf)
            {
                humanObj.SetActive(true);
               //Debug.Log("human set to active");
            }
        }
    }
}
