using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneManagement))]
public class LevelSystem : MonoBehaviour
{
    [SerializeField] Level[] levels;
    SceneManagement sm;
    int index;

    void Start()
    {
        sm = GetComponent<SceneManagement>();
    }
    
    void Update()
    {
        
    }

    public void LoadNext()
    {
    }

    [System.Serializable]
    struct Level
    {
        public string Name;
    }
}
