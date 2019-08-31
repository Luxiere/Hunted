using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    Save save;
    string json;

    public void Save()
    {
        save = new Save();
        save.won = true;
        json = JsonUtility.ToJson(save);
    }

    public bool Load()
    {
        if (JsonUtility.FromJson<Save>(json))
        {
            save = JsonUtility.FromJson<Save>(json);
            return save.won;
        }
        else
        {
            return false;
        }
    }
}
