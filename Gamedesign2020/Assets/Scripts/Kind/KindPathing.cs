using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class KindPathing : MonoBehaviour
{
    public float speed = 2;
    
    public PathCreator pathCreator;
    
    float distanceTravelled;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        

        movement.x = -(pathCreator.path.GetNormalAtDistance(distanceTravelled).x) ;
        movement.y = (pathCreator.path.GetNormalAtDistance(distanceTravelled).y) ;
        movement.Normalize();
        movement =  Quaternion.Euler(0, 0, 90)* movement ;

        animator.SetFloat("hSpeed", movement.x);
        animator.SetFloat("vSpeed", movement.y);
        animator.SetFloat("speed", movement.magnitude);
        
    }

    void FixedUpdate()
    {
        
    }
}
