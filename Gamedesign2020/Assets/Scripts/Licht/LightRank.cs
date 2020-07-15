using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightRank :MonoBehaviour
{
    public bool flicker = false;
    public float flickerRate = 1;
    private float flickerTimer;
    private float flickerMult = 1;
    public int lightrank = 0;
    [NonSerialized]
    public float influence = 1;
    [NonSerialized]
    public float intensity;

    private void Start()
    {
        intensity = gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity;
    }

    private void Update()
    {
        var light = gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        flickerTimer -= 1 * Time.deltaTime;

        if (flicker && flickerTimer < 0)
        {
            flickerMult = UnityEngine.Random.Range(0.1f, 1.2f);
            flickerTimer = flickerRate;
        }
        

        light.intensity = intensity * influence * flickerMult;

    }
}
