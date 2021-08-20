using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

public class DarcyJobSystem : MonoBehaviour
{
    [SerializeField]
    bool useJobs;

    [SerializeField]
    Transform pfLight;

    List<Lights> lightList;

    public class Lights 
    {
        public Transform transform;
        public float moveY;
    }

    void Start()
    {
        lightList = new List<Lights>();
        for(int i = 0; i < 3000; i++)
        {
            Transform lightTransform = Instantiate(pfLight, new Vector3(UnityEngine.Random.Range(-40f, 40f), UnityEngine.Random.Range(-40f, 40f)), Quaternion.identity);
            lightList.Add(new Lights
            {
                transform = lightTransform,
                moveY = UnityEngine.Random.Range(20f, 25f)
            });
        }
    }

    void Update()
    {
        float startTime = Time.realtimeSinceStartup;

        if(useJobs)
        {
            NativeArray<float3> positionArray = new NativeArray<float3>(lightList.Count, Allocator.TempJob);
            NativeArray<float> moveYArray = new NativeArray<float>(lightList.Count, Allocator.TempJob);

            for(int i = 0; i < lightList.Count; i++)
            {
                positionArray[i] = lightList[i].transform.position;
                moveYArray[i] = lightList[i].moveY;
            }

            ReallyToughParallelJob reallyToughParallelJob = new ReallyToughParallelJob
            {
                deltaTime = Time.deltaTime,
                positionArray = positionArray,
                moveYArray = moveYArray
            };

            JobHandle jobHandle = reallyToughParallelJob.Schedule(lightList.Count, 100);
            jobHandle.Complete();

            for(int i = 0; i < lightList.Count; i++)
            {
                lightList[i].transform.position = positionArray[i];
                lightList[i].moveY = moveYArray[i];
            }

            positionArray.Dispose();
            moveYArray.Dispose();
        }
        else
        {
            foreach (var l in lightList)
            {
                l.transform.position += new Vector3(0, l.moveY * Time.deltaTime);

                if (l.transform.position.y > 40f)
                {
                    l.moveY = -math.abs(l.moveY);
                }

                if (l.transform.position.y < -40f)
                {
                    l.moveY = math.abs(l.moveY);
                }

                float value = 0f;
                for (int i = 0; i < 1000; i++)
                {
                    value = math.exp10(math.sqrt(value));
                }
            }
        }

        //if(!useJobs)
        //{
        //    for(int i = 0; i < 10; i++)
        //    {
        //        ReallyToughTask();
        //    }
        //}
        //else
        //{
        //    NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);
        //    for (int i = 0; i < 10; i++)
        //    {
        //        JobHandle jobHandle = ReallyToughTaskJob();
        //        jobHandleList.Add(jobHandle);
        //    }
        //    JobHandle.CompleteAll(jobHandleList);
        //    jobHandleList.Dispose();
        //}

        Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
    }

    void ReallyToughTask()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }

    JobHandle ReallyToughTaskJob()
    {
        ReallyToughJob job = new ReallyToughJob();
        return job.Schedule();
    }
}

[BurstCompile]
public struct ReallyToughJob : IJob
{
    public void Execute()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}

[BurstCompile]
public struct ReallyToughParallelJob : IJobParallelFor
{
    public NativeArray<float> moveYArray;
    public NativeArray<float3> positionArray;
    public float deltaTime;

    public void Execute(int index)
    {
        positionArray[index] += new float3(0, moveYArray[index] * deltaTime, 0f);

        if (positionArray[index].y > 40f)
        {
            moveYArray[index] = -math.abs(moveYArray[index]);
        }

        if (positionArray[index].y < -40f)
        {
            moveYArray[index] = math.abs(moveYArray[index]);
        }

        float value = 0f;
        for (int i = 0; i < 1000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}

