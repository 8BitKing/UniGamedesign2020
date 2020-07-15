using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using PathCreation;
using System;
using System.IO;
using System.IO.IsolatedStorage;

public class EnemyStateWalking : IState
{
    private KindControllerRaycast target;

    //initialisieren der Path variablen
    private PathCreator[] pathCreator;
    public EndOfPathInstruction end;
    float dstTravelled;
    private float speed;
    private int sign = 1;
    private bool[] patrol;
    private bool endOfPath = false;

    private EnemyController owner;
    private Animator animator;
    private Rigidbody2D rigidbody;

    private Vector2 movement;
    
    private Vector3 goal;
    private Vector2 direction;
    private Vector2 goal2D;
    
    private GridDebug gridObject;
    private int visionRange;
    public int currpath=0;


    public EnemyStateWalking(EnemyController owner)
    {

        this.owner = owner;
        this.pathCreator = owner.pathCreator;
        this.animator = owner.animator;
        this.rigidbody = owner.rb;
        this.speed = owner.speed;
        this.movement = owner.movement;
        
        this.gridObject = owner.gridObject;
        this.visionRange = owner.visionRange;
        this.patrol = owner.patrol;

        this.target = owner.target;

    }
    public void stateInit()
    {
        this.animator.Play("WalkAnimations", -1, 0);
        this.owner.rb.bodyType = RigidbodyType2D.Dynamic;
        owner.isOnPath = true;
        owner.speed = 1.5f;
        //----------------------------------------------------------------------------------------------WICHTIG
        //initialisieren der Pfade für den editor, sonst spackt alles
        for(int i = 0; i < pathCreator.Length; i++)
        {
            pathCreator[i].InitializeEditorData(true);
        }
        
    }


    public void stateExit()
    {
        owner.isOnPath = false;
        owner.speed = 0.5f;
    }


    public void stateUpdate()
    {
        
        
        //wenn kind in visionRange switch zu follow
        if ((target.gameObject.transform.position-owner.gameObject.transform.position).magnitude<visionRange*0.32f)
        {
            owner.stateMachine.ChangeState(new EnemyStateFollow(owner));
        }
        //patrol ist bool ob es ein patroullie weg ist oder nicht wenn ja dann:
        if (patrol[currpath])
        {
            //wenn man am ende vom pfad ankommt oder auf dem rückweg am anfang des pfades ankommt dann
            if (pathCreator[currpath].path.length - 0.12f <= dstTravelled || (dstTravelled < 0.12&&sign==-1))
            {
                //bewegungsaddition umdrehen
                sign *= -1;
                //pfadnormalen flippen
                pathCreator[currpath].bezierPath.FlipNormals = !pathCreator[currpath].bezierPath.FlipNormals;

            }
        }
        //ansonsten wenn keine patroullie, wenn am ende vom pfad angekommen:
        else if(pathCreator[currpath].path.length-0.05f <= dstTravelled)
        {
            //wenn es einen anschlusspfad gibt wechsele auf anschlusspfad und setze "pfadprogress" zurück auf 0
            if (currpath + 1 < pathCreator.Length)
            {
                currpath++;
                dstTravelled = 0;
            }
            //sonst setze indikatorvariable dass man am ende vom letzten pfad angekommen ist auf true
            else
            {
                endOfPath = true;

            }
        }
        //solange man noch nicht am ende des letzten pfades angekommen ist normale wegberechnung, rb positioniereung und animator kram
        if (endOfPath == false)
        {
            dstTravelled += speed * Time.deltaTime * sign;
            movement=((Vector2)pathCreator[currpath].path.GetPointAtDistance(dstTravelled, end)-owner.rb.position);

            owner.rb.MovePosition(owner.rb.position + movement);

            direction = Quaternion.Euler(0, 0, -90) * pathCreator[currpath].path.GetNormalAtDistance(dstTravelled);
            animator.SetFloat("hdir", direction.x);
            animator.SetFloat("vdir", direction.y);
        }
        //am ende des letzten pfades wechsel in idle state
        else
        {

            owner.stateMachine.ChangeState(new EnemyStateIdle(this.owner));


        }
        



    }


    public void stateFixedUpdtate()
    {
        
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
    }
}