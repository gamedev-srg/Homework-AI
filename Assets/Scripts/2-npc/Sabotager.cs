using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sabotager : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    [SerializeField] float rotationSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
      
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.position);
        FaceDestination();
    }
    private void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        //transform.rotation = lookRotation; // Immediate rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); // Gradual rotation
    }
}
