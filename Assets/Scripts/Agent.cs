using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MyAgent : Agent
{
    public Transform target;
    public float moveSpeed = 1f;


    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(14f, 3f, -13.5f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        Vector3 move = new Vector3(moveX, 0f, moveZ);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continiousActionsOut = actionsOut.ContinuousActions;
        continiousActionsOut[0] = Input.GetAxisRaw("Horizontal");
        continiousActionsOut[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Target")) {
            SetReward(1f);
            Debug.Log("Reward 1");
            EndEpisode();
        }
    }
}
