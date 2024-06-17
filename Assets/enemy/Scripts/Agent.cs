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

    private int index = 0;

    public float distanceCheck = 5f;

    private Rigidbody rb;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        if (index >= spawnPoints.Length) { index = 0; }
        transform.position = new Vector3(-4.077254f, 0.3058391f, -13.56139f);
        
       
    }
    void ResetTarget()
    {
        Debug.Log("Hier gekomen");
        target.position = spawnPoints[index++].position;
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
            SetReward(-0.005f);
        

        
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

            /*if (!hit.collider.CompareTag("Target"))
            {
                // Als de raycast een obstakel raakt, pas dan de bewegingsrichting aan
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, Vector3.Reflect(transform.forward, hit.normal), Mathf.PI, 0f);
                rb.velocity = newDirection * moveSpeed;
            }*/
        }

        // Reset rotation in the x and z axes
        Quaternion newRotation = Quaternion.Euler(0f, rb.rotation.eulerAngles.y, 0f);
       rb.MoveRotation(newRotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Target")) {
            ResetTarget();
            SetReward(1f);
            Debug.Log("Reward 1");
            EndEpisode();
        }
    }
}
