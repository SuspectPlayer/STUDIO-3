using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is being adapted from a Tutorial made by GameDevChef
//Here is the link (https://www.youtube.com/watch?v=mBVarJm3Tgk&ab_channel=GameDevChef)

public class SingleFlock : MonoBehaviour
{
    [Header("SpawnVariables")]
    [SerializeField] private FlockMember unitPrefab; // The prefab of the object you wish to have as the member of the flock (standard)

    [SerializeField] private int flockSize; // How many flock members are allowed to exist
    [SerializeField] private Vector3 spawnArea; // The space that flock members may exist within

    [Header("Speed Variables")]
    //Minimum Speed
    [Range(0, 10)]
    [SerializeField] private float minSpeed;
    public float minSpeedProp { get { return minSpeed; } }

    //Maximum Speed
    [Range(0, 10)]
    [SerializeField] private float maxSpeed;
    public float maxSpeedProp { get { return maxSpeed; } }

    [Header("Detection")]
    //Cohesion
    [Range(0, 10)]
    [SerializeField] private float cohesionDist;
    public float cohesionDistProp { get { return cohesionDist; } }

    //Avoidance
    [Range(0, 10)]
    [SerializeField] private float avoDist;
    public float avoDistProp { get { return avoDist; } }

    //Alignment
    [Range(0, 10)]
    [SerializeField] private float alignDist;
    public float alignDistProp { get { return alignDist; } }

    //Bounds
    [Range(0, 100)]
    [SerializeField] private float boundsDist;
    public float boundsDistProp { get { return boundsDist; } }

    //Obstacle
    [Range(0, 10)]
    [SerializeField] private float obstDist;
    public float obstDistProp { get { return obstDist; } }

    [Header("Behavior Weighting")]
    //Cohesion
    [Range(0, 10)]
    [SerializeField] private float cohesionWeight;
    public float cohesionWeightProp { get { return cohesionWeight; } }

    //Avoidance
    [Range(0, 10)]
    [SerializeField] private float avoWeight;
    public float avoWeightProp { get { return avoWeight; } }

    //Alignment
    [Range(0, 10)]
    [SerializeField] private float alignWeight;
    public float alignWeightProp { get { return alignWeight; } }

    //Bounds
    [Range(0, 10)]
    [SerializeField] private float boundsWeight;
    public float boundsWeightProp { get { return boundsWeight; } }

    //Obstacle
    [Range(0, 100)]
    [SerializeField] private float obstWeight;
    public float obstWeightProp { get { return obstWeight; } }

    public FlockMember[] Units { get; set; }

    [Header("Optional Variables: Fish Variety")]
    public bool uniqueFish;

    [SerializeField] private FlockMember unitPrefabAlt;  //
    [SerializeField] private FlockMember unitPrefabAltTwo; // The prefab of the object you wish to have as the member of the flock (additional)
    [SerializeField] private FlockMember unitPrefabAltThree; //

    [Header("Variety Weighting")]
    [Tooltip("Dictates upper limit of spawn randomiser. i.e. a value of 0.75 means that the 1st prefab will appear 25 times in a flock of 100")] [SerializeField] [Range(0f, 1f)] private float upperQuart = 0.75f;
    [Tooltip("Dictates middle limit of spawn randomiser. i.e. a value of 0.5, upper limit of 0.75, and lower limit of 0.25 means that the 2nd prefab will appear 25 times, and the 3rd prefab will appear 25 times in a flock of 100")] [SerializeField] [Range(0f, 1f)] private float midQuart = 0.5f;
    [Tooltip("Dictates lower limit of spawn randomiser. i.e. a value of 0.25 means that the 4th prefab will appear 25 times in a flock of 100")] [SerializeField] [Range(0f, 1f)] private float lowerQuart = 0.25f;

    [Header("Optional Variables: Flock Leader")]
    public bool beingLed;
    public GameObject pathLeader;

    private void Start()
    {
        FlockInit();
    }

    private void Update()
    {
        for (int i = 0; i < Units.Length; i++)
        {
            Units[i].Movement();
        }
    }

    void FlockInit() //Initializes and creates flock
    {
        Units = new FlockMember[flockSize];
        if (uniqueFish && unitPrefabAlt == null && unitPrefabAltTwo == null && unitPrefabAltThree == null)
        {
            Debug.LogWarning("No alternate fish assigned. Please assign alternate fish if you wish to use unique fish.");
            uniqueFish = false;
        }
        for (int i = 0; i < flockSize; i++)
        {
            float randScale = Random.Range(0.1f, 0.7f);
            Vector3 randVec = Random.insideUnitSphere;
            randVec = new Vector3(randVec.x * spawnArea.x, randVec.y * spawnArea.y, randVec.z * spawnArea.z);
            Vector3 spawnLoc = transform.position + randVec;
            Quaternion rotie = Quaternion.Euler(0, Random.Range(0, 360), 0);
            int num = UnitNum();

            if (uniqueFish)
            {
                int chosenIndex = Random.Range(0, flockSize);

                Debug.Log(chosenIndex);

                if (chosenIndex > flockSize * lowerQuart)
                {
                    if (unitPrefabAltThree != null)
                    {
                        Units[i] = Instantiate(unitPrefabAltThree, spawnLoc, rotie);
                        Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                    }
                    else
                    {
                        int newUnitNum = Random.Range(0, num);
                        if (newUnitNum == 0)
                        {
                            Units[i] = Instantiate(unitPrefab, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 1)
                        {
                            Units[i] = Instantiate(unitPrefabAlt, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 2)
                        {
                            Units[i] = Instantiate(unitPrefabAltTwo, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 3)
                        {
                            Units[i] = Instantiate(unitPrefabAltThree, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                    }
                    
                }
                else if (chosenIndex <= flockSize * lowerQuart && chosenIndex > flockSize * midQuart)
                {
                    if (unitPrefabAltTwo != null)
                    {
                        Units[i] = Instantiate(unitPrefabAltTwo, spawnLoc, rotie);
                        Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                    }
                    else
                    {
                        int newUnitNum = Random.Range(0, num);
                        if (newUnitNum == 0)
                        {
                            Units[i] = Instantiate(unitPrefab, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 1)
                        {
                            Units[i] = Instantiate(unitPrefabAlt, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 2)
                        {
                            Units[i] = Instantiate(unitPrefabAltTwo, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 3)
                        {
                            Units[i] = Instantiate(unitPrefabAltThree, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                    }
                }
                else if (chosenIndex <= flockSize * midQuart && chosenIndex > flockSize * upperQuart)
                {
                    if (unitPrefabAlt != null)
                    {
                        Units[i] = Instantiate(unitPrefabAlt, spawnLoc, rotie);
                        Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                    }
                    else
                    {
                        int newUnitNum = Random.Range(0, num);
                        if (newUnitNum == 0)
                        {
                            Units[i] = Instantiate(unitPrefab, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 1)
                        {
                            Units[i] = Instantiate(unitPrefabAlt, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 2)
                        {
                            Units[i] = Instantiate(unitPrefabAltTwo, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                        else if (newUnitNum == 3)
                        {
                            Units[i] = Instantiate(unitPrefabAltThree, spawnLoc, rotie);
                            Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                        }
                    }
                }
                else
                {
                    Units[i] = Instantiate(unitPrefab, spawnLoc, rotie);
                    Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
                }
            }
            else
            {
                Units[i] = Instantiate(unitPrefab, spawnLoc, rotie);
                Units[i].transform.localScale = new Vector3(randScale, randScale, randScale);
            }
            
            Units[i].Assign(this);
            Units[i].SpeedInit(Random.Range(minSpeed, maxSpeed));
        }
    }

    int UnitNum()
    {
        int num = 4;
        if (unitPrefabAltThree == null) num--;
        if (unitPrefabAltTwo == null) num--;
        if (unitPrefabAlt == null) num--;
        if (unitPrefab == null) num--;
        Debug.Log(num);
        return num;
    }


}
