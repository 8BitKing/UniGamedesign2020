using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DruckplatteController : MonoBehaviour
{
    public GameObject[] Trigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MOVEABLE")
        {
            foreach (GameObject obj in Trigger)
            {
                var trigger = obj.GetComponent<PowerTrigger>();
                trigger.active = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MOVEABLE")
        {
            foreach (GameObject obj in Trigger)
            {
                var trigger = obj.GetComponent<PowerTrigger>();
                trigger.active = true;
            }
        }

    }


}

