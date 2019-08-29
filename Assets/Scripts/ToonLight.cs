using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonLight : MonoBehaviour
{
    private Light lOight = null;

    private void OnEnable()
    {
        lOight = GetComponent<Light>();
    }

    void Update()
    {
        Shader.SetGlobalVector("_ToonLightDirection", -this.transform.forward);
    }
}
