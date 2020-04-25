using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Serialization
{
    public Serialization()
    {
    }

    private static Serialization _instance;

    public static Serialization GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Serialization();
        }
        return _instance;
    }
    public List<float> Perlinsample = new List<float>();
    public List<SaveableVector> CatLoc = new List<SaveableVector>();
    public List<float> RnumberPerlin = new List<float>();
    public List<CatStatsData> CatStatsList = new List<CatStatsData>();


    public void Load()
    {
        Serialization data = SaveSystem.LoadData();

        Perlinsample = data.Perlinsample;
        CatLoc = data.CatLoc;
        RnumberPerlin = data.RnumberPerlin;
        CatStatsList = data.CatStatsList;
    }
}
