using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableObject : GrabbableObject
{
    private Rigidbody body;

    private Transform socket;

    private GameObject socketGameObject;

    [SerializeField] private Transform ghostInitTransform;
    private Vector3 ghostInitTransformPosition;
    private Quaternion ghostInitTransformRotation;

    private bool socketFull = false;
    private int planterValue = 0;
    private int ghostNumber = 0;
    [HideInInspector] public bool ghostMatchPlanter = false;

    private void Start()
    {
        body = GetComponent<Rigidbody>();

        ghostInitTransformPosition = ghostInitTransform.position;
        ghostInitTransformRotation = ghostInitTransform.rotation;


        if (CompareTag("ghostS"))
        {
            ghostNumber = 1;
        }
        else if (CompareTag("ghostM"))
        {
            ghostNumber = 2;
        }
        else if (CompareTag("ghostL"))
        {
            ghostNumber = 3;
        }
        else
        {
            ghostNumber = 100;
        }
    }

    public override void OnGrabStart(XRHand hand)
    {
        if (GameManager.instance.canGrabGhosts)
        {
            base.OnGrabStart(hand);
           
            if (socketFull == true)
            {
                // Reset values
                planterValue = 0;
                ghostMatchPlanter = false;

                socketFull = false;
            }
        }

    }

    public override void OnGrabEnd()
    {
        base.OnGrabEnd();

        if(socket != null)
        {
            body.useGravity = false;
            body.isKinematic = true;

            transform.position = socket.position;
            transform.rotation = socket.rotation;

            socketFull = true;

            // Get the value of the planter
            planterValue = socketGameObject.GetComponentInParent<Planter>().planterNumber;

            // Analyze if ghost matches planter
            if (planterValue == ghostNumber)
            {
                ghostMatchPlanter = true;
            }
            else
            {
                ghostMatchPlanter = false;
            }

        }
        else
        {
            transform.position = ghostInitTransformPosition;
            transform.rotation = ghostInitTransformRotation;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("socket"))
        {
            socket = other.transform;
            socketGameObject = other.gameObject;
            socketGameObject.tag = "usedSocket";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("usedSocket"))
        {
            socketGameObject.tag = "socket";
            socket = null;
            socketGameObject = null;
        }
    }
}
