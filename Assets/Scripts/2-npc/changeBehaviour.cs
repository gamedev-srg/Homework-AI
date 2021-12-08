using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBehaviour : MonoBehaviour
{
    [SerializeField] GameObject enemyToAffect; //Who the change will apply to
    [SerializeField] Type type; //the type of changes possible
    enum Type 
    {
        chaser,brave,coward,sabotager
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyToAffect.GetComponent<EnemyController3>().role = (EnemyController3.Role)type; //casting from this class's enum to the Controller's Enum 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
