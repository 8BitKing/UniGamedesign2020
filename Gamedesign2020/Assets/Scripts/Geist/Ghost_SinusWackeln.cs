using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_SinusWackeln : MonoBehaviour
{

    private double timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        timer += 1;

        transform.Translate(new Vector2(0, 0.05f) * Mathf.Sin(((float)timer) / 100) * Time.deltaTime);
    }
}
