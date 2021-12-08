using UnityEngine;
using UnityEngine.AI;


/**
 * This component represents an NPC that patrols randomly between targets.
 * The targets are all the objects with a Target component inside a given folder.
 */
[RequireComponent(typeof(NavMeshAgent))]
public class Patroller: MonoBehaviour {
    [Tooltip("Minimum time to wait at target between running to the next target")]
    [SerializeField] private float minWaitAtTarget = 7f;

    [Tooltip("Maximum time to wait at target between running to the next target")]
    [SerializeField] private float maxWaitAtTarget = 15f;


    [Tooltip("A game object whose children have a Target component. Each child represents a target.")]
    [SerializeField] private Transform targetFolder = null;
    [SerializeField] private GameObject player;
    private Target[] allTargets = null;

    [Header("For debugging")]
    [SerializeField] private Target currentTarget = null;
    [SerializeField] private float timeToWaitAtTarget = 0.5f;
    private int mode = 0;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float rotationSpeed = 5f;


    public void changeMode(int mode)
    { //setting patrol mode
        this.mode = mode;
    }
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        allTargets = targetFolder.GetComponentsInChildren<Target>(false); // false = get components in active children only
        currentTarget = allTargets[Random.Range(0, allTargets.Length - 1)];
        //Debug.Log("Found " + allTargets.Length + " active targets.");
        SelectNewTarget(mode);
    }

    private void SelectNewTarget(int mode)
    {
        switch (mode)
        {
            case 1: //1 is brave in the Enum class
                //find the closet target to the player and go there.
                float min_distance = float.MaxValue;
                foreach (Target target in allTargets)
                {
                    float distance = Vector3.Distance(player.transform.position, target.transform.position);
                    if (distance < min_distance)
                    {
                        min_distance = distance;
                        currentTarget = target;
                    }
                }
                navMeshAgent.SetDestination(currentTarget.transform.position);
                FaceDestination();
                break;

            case 2: //2 is coward
                //find the farthest target from the player and go there.
                float max_distance = float.MinValue;
                foreach (Target target in allTargets)
                {
                    float distance = Vector3.Distance(player.transform.position, target.transform.position);
                    if (distance > max_distance)
                    {
                        max_distance = distance;
                        currentTarget = target;
                    }
                }
                navMeshAgent.SetDestination(currentTarget.transform.position);
                FaceDestination();
                break;

            default: //simple patroller default
                currentTarget = allTargets[Random.Range(0, allTargets.Length - 1)];
                Debug.Log("New target: " + currentTarget.name);
                navMeshAgent.SetDestination(currentTarget.transform.position);
                FaceDestination();
                //if (animator) animator.SetBool("Run", true);
                break;
        }
    }


        private void Update() {
           // we are at the target
            //if (animator) animator.SetBool("Run", false);
            timeToWaitAtTarget -= Time.deltaTime;
            if (timeToWaitAtTarget <= 0)
            {
            SelectNewTarget(mode);
            }
        
    }





    private void FaceDestination() {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        //transform.rotation = lookRotation; // Immediate rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); // Gradual rotation
    }


}
