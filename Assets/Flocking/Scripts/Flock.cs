using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public Transform assignedLeader;
    public FlockBehavior behavior;
    List<FlockAgent> agents = new List<FlockAgent>();

    [Range(10, 500)]
    public int startingCount = 250;
    const float agentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float getSquareAvoidanceRadius
    {
        get { return squareNeighborRadius; }
    }

    //fix y axis
    float yAxisValue = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        //assign leader to follow leader behavior
        //check if behavior is composite
        if(behavior is Composite composite)
        {
            //check which one of the behaviors in composite is the follow leader behavior
            foreach (var v in composite.behaviors)
            {
                //assign leader
                if(v is FollowLeader followLeader)
                {
                    followLeader.Leader = assignedLeader;
                }
            }
        }

        //create agents
        for (int i = 0; i < startingCount; i++)
        {
            Vector2 unitCirclePos = UnityEngine.Random.insideUnitCircle;
            Vector3 spawnPos = new Vector3(unitCirclePos.x, yAxisValue, unitCirclePos.y) * startingCount * agentDensity;

            FlockAgent newAgent = Instantiate(
                agentPrefab,
                spawnPos,
                Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach(Collider collider in contextColliders)
        {
            if(collider != agent.getAgentCollider)
            {
                context.Add(collider.transform);
            }
        }
        return context;
    }
}
