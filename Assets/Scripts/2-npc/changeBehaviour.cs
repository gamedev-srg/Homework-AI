using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBehaviour : MonoBehaviour
{
    [SerializeField] GameObject enemyToAffect;
    [SerializeField] Type type;
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
            Debug.Log("here");
            enemyToAffect.GetComponent<EnemyController3>().role = (EnemyController3.Role)type;   
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
