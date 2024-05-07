using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MyAgent : Agent
{
    public Transform target;
    public float moveSpeed = 7f;
    public float turnSpeed = 10f;
    public LayerMask obstacleLayer;
    public float raycastDistance = 1f;
    public Transform[] spawnPoints;

    public float distanceCheck = 5f;

    private int currentSpawnPointIndex;
    private Rigidbody rb;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        // currentSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        transform.position = new Vector3(14f, 3f, -13.5f);
        
        // ResetTarget();
    }
    void ResetTarget()
    {
        // Kies willekeurig een nieuw spawnpoint voor het target
        int newSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        target.position = spawnPoints[newSpawnPointIndex].position;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float rotateY = actions.ContinuousActions[1];

        // Move the agent forward in the direction it is facing
        Vector3 move = transform.forward * moveX * moveSpeed;
        rb.velocity = move;

        // Draai de agent
        Vector3 rotation = new Vector3(0f, rotateY, 0f);
        Quaternion deltaRotation = Quaternion.Euler(rotation * turnSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < distanceCheck)
        {
            Debug.Log("In de buurt");
            SetReward(0.1f);
        } else
        {
            SetReward(-0.05f);
        }

        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continiousActionsOut = actionsOut.ContinuousActions;
        continiousActionsOut[1] = Input.GetAxisRaw("Horizontal");
        continiousActionsOut[0] = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        // Voer raycasts uit om obstakels te detecteren
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, obstacleLayer))
        {
            // Als de raycast een obstakel raakt, pas dan de bewegingsrichting aan
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, Vector3.Reflect(transform.forward, hit.normal), Mathf.PI, 0f);
            rb.velocity = newDirection * moveSpeed;
        }

        // Reset rotation in the x and z axes
        Quaternion newRotation = Quaternion.Euler(0f, rb.rotation.eulerAngles.y, 0f);
        rb.MoveRotation(newRotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Target")) {
            SetReward(1f);
            Debug.Log("Reward 1");
            EndEpisode();
           // ResetTarget();
        }
    }
}
