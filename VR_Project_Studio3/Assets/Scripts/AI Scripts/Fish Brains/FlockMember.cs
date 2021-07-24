using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is being adapted from a Tutorial made by GameDevChef
//Here is the link (https://www.youtube.com/watch?v=mBVarJm3Tgk&ab_channel=GameDevChef)

public class FlockMember : MonoBehaviour
{
    [SerializeField] private float unitFOV;                         // Each unit's "FOV"
    [SerializeField] private float smoothTime;
    [SerializeField] private LayerMask obstMask;
    [SerializeField] private Vector3[] obstAvoDir;

    private SingleFlock flockAssigned;                                 // Referencing the Main Flock

    private List<FlockMember> cohesionPeers = new List<FlockMember>(); // List of neighbours within field of view for cohesion
    private List<FlockMember> avoPeers = new List<FlockMember>(); // List of neighbours within field of view for avoidance
    private List<FlockMember> alignPeers = new List<FlockMember>(); // List of neighbours within field of view for alignment
    private Vector3 currentVelocity;
    private Vector3 storedDir;
    private float speed;

    public Transform thisTrans { get; set; }

    private void Awake()
    {
        thisTrans = transform;
    }

    public void Assign(SingleFlock flock) //Assigns Flock
    {
        flockAssigned = flock;
    }

    public void SpeedInit(float speed) //Initialises starting speed for units
    {
        this.speed = speed;
    }

    public void Movement()//Dictates the movement of units in the flock
    {
        GetPeers();
        GetSpeedViaPeers();

        Vector3 cohesionVec = GetCohesionVec() * flockAssigned.cohesionWeightProp; //The unit's cohesion vector
        Vector3 avoVec = GetAvoVec() * flockAssigned.avoWeightProp; //The unit's avoidance vector
        Vector3 alignVec = GetAlignVec() * flockAssigned.alignWeightProp; //The unit's alignment vector
        Vector3 boundsVec = GetBoundsVec() * flockAssigned.boundsWeightProp; //The unit's movement bounds
        Vector3 obstVec = GetObstVec() * flockAssigned.obstWeightProp; //The unit's obstacle avoidance vector

        Vector3 moveVec = cohesionVec + avoVec + alignVec + boundsVec + obstVec;
        moveVec = Vector3.SmoothDamp(thisTrans.forward, moveVec, ref currentVelocity, smoothTime);
        moveVec = moveVec.normalized * speed;
        thisTrans.forward = moveVec;
        thisTrans.position += moveVec * Time.deltaTime;
    }

    
    private void GetSpeedViaPeers() // Calculates speed of fish based on the motion of neighboring fish
    {
        if (cohesionPeers.Count == 0) // Ensures that fish without neighbors will still move
            return;

        speed = 0;
        for (int i = 0; i < cohesionPeers.Count; i++)
        {
            speed += cohesionPeers[i].speed;
        }
        speed /= cohesionPeers.Count;
        speed = Mathf.Clamp(speed, flockAssigned.minSpeedProp, flockAssigned.maxSpeedProp);
    }

    private void GetPeers() //Locates neighbouring entities of flock within specified field of view
    {
        cohesionPeers.Clear();
        alignPeers.Clear();
        avoPeers.Clear();
        FlockMember[] units = flockAssigned.Units;
        for(int i = 0; i < units.Length; i++)
        {
            FlockMember thisPeer = units[i];
            if (thisPeer != this)
            {
                float peerDisSqr = Vector3.SqrMagnitude(thisPeer.transform.position - transform.position);
                if (peerDisSqr <= flockAssigned.cohesionDistProp * flockAssigned.cohesionDistProp)
                {
                    cohesionPeers.Add(thisPeer);
                }
                if (peerDisSqr <= flockAssigned.avoDistProp * flockAssigned.avoDistProp)
                {
                    avoPeers.Add(thisPeer);
                }
                if (peerDisSqr <= flockAssigned.alignDistProp * flockAssigned.alignDistProp)
                {
                    alignPeers.Add(thisPeer);
                }
            }
        }
    }

    private Vector3 GetCohesionVec() // Returns normalised from all neighbors' average location
    {
        Vector3 cohesionVec = Vector3.zero;
        if (cohesionPeers.Count == 0)
        {
            return cohesionVec;
        }
        int seenPeers = 0;
        for (int i = 0; i < cohesionPeers.Count; i++)
        {
            if (CanSee(cohesionPeers[i].thisTrans.position))
            {
                seenPeers++;
                cohesionVec += cohesionPeers[i].thisTrans.position;
            }
        }
        if (seenPeers == 0)
            return cohesionVec;
        cohesionVec /= seenPeers;
        cohesionVec -= thisTrans.position;
        cohesionVec = cohesionVec.normalized;
        return cohesionVec;
    }

    private Vector3 GetAlignVec() // Returns normalised vector3 from all neighbors' average direction
    {
        Vector3 alignVec = thisTrans.forward;
        if (alignPeers.Count == 0)
        {
            return thisTrans.forward;
        }
        int seenPeers = 0;
        for (int i = 0; i < alignPeers.Count; i++)
        {
            if (CanSee(alignPeers[i].thisTrans.position))
            {
                seenPeers++;
                alignVec += alignPeers[i].thisTrans.position;
            }
        }

        alignVec /= seenPeers;
        alignVec = alignVec.normalized;
        return alignVec;
    }

    private Vector3 GetAvoVec() // Returns normalised vector3 from all neighbors' average direction away from this unit's direction
    {
        Vector3 avoVec = Vector3.zero;
        if (avoPeers.Count == 0)
        {
            return Vector3.zero;
        }
        int seenPeers = 0;
        for (int i = 0; i < avoPeers.Count; i++)
        {
            if (CanSee(avoPeers[i].thisTrans.position))
            {
                seenPeers++;
                avoVec += (thisTrans.position - avoPeers[i].thisTrans.position);
            }
        }

        avoVec /= seenPeers;
        avoVec = avoVec.normalized;
        return avoVec;
    }

    private Vector3 GetBoundsVec() // If unit gets too far from flock centre, will direct towards centre
    {
        Vector3 flockCentre;
        if (flockAssigned.beingLed == true)
        {
            flockCentre = flockAssigned.pathLeader.transform.position - thisTrans.position;
        }
        else
        {
            flockCentre = flockAssigned.transform.position - thisTrans.position;
        }

        bool withinBounds = flockCentre.magnitude >= (flockAssigned.boundsDistProp * 0.9f);
        return withinBounds ? flockCentre.normalized : Vector3.zero;
    }

    private Vector3 GetObstVec() //If obstacle is in front of unit, calls GetBestObstAvoVec() and returns result, if not, the returns Vector3.zero
    {
        Vector3 obstVec = Vector3.zero;
        RaycastHit hit;

        if(Physics.Raycast(thisTrans.position, thisTrans.forward, out hit, flockAssigned.obstDistProp, obstMask))
        {
            obstVec = GetBestObstAvoVec();
        }
        else
        {
            storedDir = Vector3.zero;
        }
        return obstVec;
    }

    private Vector3 GetBestObstAvoVec() // Casts four rays and finds the ray that either is the longest or doesnt hit, and returns that direction
    {
        if(storedDir != Vector3.zero)
        {
            RaycastHit hit;
            if (!Physics.Raycast(thisTrans.position, thisTrans.forward, out hit, flockAssigned.obstDistProp, obstMask, QueryTriggerInteraction.Collide))
            {
                return storedDir;
            }
        }
        float maxDist = int.MinValue;
        Vector3 selDir = Vector3.zero;
        for (int i = 0; i < obstAvoDir.Length; i++)
        {
            RaycastHit hit;
            Vector3 currDir = thisTrans.TransformDirection(obstAvoDir[i].normalized);
            if(Physics.Raycast(thisTrans.position, currDir, out hit, flockAssigned.obstDistProp, obstMask, QueryTriggerInteraction.Collide))
            {
                float currDist = (hit.point - thisTrans.position).sqrMagnitude;
                if (currDist > maxDist)
                {
                    maxDist = currDist;
                    selDir = currDir;
                }
            }
            else
            {
                selDir = currDir;
                storedDir = currDir.normalized;
                return selDir.normalized;
            }
        }
        return selDir.normalized;
    }

    private bool CanSee(Vector3 position) // Detects if a unit is seeing other units
    {
        return Vector3.Angle(thisTrans.forward, position - thisTrans.position) <= unitFOV;
    }
}
