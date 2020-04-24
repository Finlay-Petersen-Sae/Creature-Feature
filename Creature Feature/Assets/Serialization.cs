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
    public List<Vector3> CatLoc = new List<Vector3>();
    public List<float> RnumberPerlin = new List<float>();
    public List<CatStatsData> CatStatsList = new List<CatStatsData>();

    public void Awake()
    {
        Save();
    }

    public void Save()
    {


    }

    public void Load()
    {
        Serialization data = SaveSystem.LoadData();

        Perlinsample = data.Perlinsample;
        CatLoc = data.CatLoc;
        RnumberPerlin = data.RnumberPerlin;
        CatStatsList = data.CatStatsList;
    }
}
