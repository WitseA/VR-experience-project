using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SceneManagement;

public class MyAgent : Agent
{
    public Transform target;
    public float moveSpeed = 7f;
    public float turnSpeed = 10f;
    public LayerMask obstacleLayer;
    public float raycastDistance = 1f;
  

    private int index = 0;

    public float distanceCheck = 5f;

    private Rigidbody rb;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
      if(transform != null) { transform.position = new Vector3(-14.965f, -1.382f, -1.59f); }
       
        
       
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        if (transform != null) { sensor.AddObservation(transform.position); }
        if( target != null){ sensor.AddObservation(target.position); }
        
            
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float rotateY = actions.ContinuousActions[1];

        // Move the agent forward in the direction it is facing
        if (transform != null)
        {
            Vector3 move = transform.forward * moveX * moveSpeed;
            rb.velocity = move;
        }
      

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
       

        // Reset rotation in the x and z axes
        Quaternion newRotation = Quaternion.Euler(0f, rb.rotation.eulerAngles.y, 0f);
       rb.MoveRotation(newRotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) {
            SetReward(1f);
            Debug.Log("Reward 1");
            EndEpisode();
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}
