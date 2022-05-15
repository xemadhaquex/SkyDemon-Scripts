using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Controller : MonoBehaviour
{

    public float lookRadius=20f;
    public float stoppingDistance=5f;
    public Material damagedMat;
    public ParticleSystem deathParticles;
    GameObject Player;
    public GameObject numEnemyTracker;
    Transform target;
    NavMeshAgent agent;
    public int health=10;

    // Start is called before the first frame update
    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        Player=GameObject.FindGameObjectWithTag("Player");
        //numEnemyTracker=GameObject.FindGameObjectWithTag("EnemyNumberTracker");
        target=Player.transform;
        
    }

    // Update is called once per frame p
    void Update()
    {
        float distance =Vector3.Distance(target.position,transform.position);
        if( distance<=lookRadius){
            agent.SetDestination(target.position);

            if(distance <= stoppingDistance){
                //Attack player
                FaceTarget();//face player
            }
        }

        if(health<=0){
            
            ParticleSystem psEnemy=Instantiate(deathParticles,gameObject.transform.position, Quaternion.Euler(-90f,0f,0f));
            var em=psEnemy.GetComponent<ParticleSystem>().emission;
            em.enabled=true;
            //Instantiate(deathParticles,gameObject.transform) as GameObject;
            Debug.Log("Enemy death particles");
        
            numEnemyTracker.GetComponent<NumberOfEnemyTracker>().numberOfEnemies=numEnemyTracker.GetComponent<NumberOfEnemyTracker>().numberOfEnemies-1;
            numEnemyTracker.GetComponent<NumberOfEnemyTracker>().numEnemiesKilled=numEnemyTracker.GetComponent<NumberOfEnemyTracker>().numEnemiesKilled+1;

            Destroy(gameObject);
        }
    }

    void FaceTarget()
    {

        Vector3 direction = (target.position-transform.position).normalized;
        Quaternion lookRotation= Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation= Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

private bool hasCollide = false;
    void OnTriggerEnter(Collider col){
        if(hasCollide==false){

            if(col.name=="E_ThrowingAxe(Clone)" || col.name=="E_ReturnAxe(Clone)"){
            health=health-5;
            GetComponent<Renderer>().material=damagedMat;
            hasCollide=true;
            StartCoroutine(hasCollided());

            }

        }

        //Debug.Log(col.name);
    }
    /*void OnDestroy(){
        ParticleSystem psEnemy=Instantiate(deathParticles,gameObject.transform.position, Quaternion.Euler(-90f,0f,0f));
        var em=psEnemy.GetComponent<ParticleSystem>().emission;
        em.enabled=true;
        //Instantiate(deathParticles,gameObject.transform) as GameObject;
        Debug.Log("Enemy death particles");
        
        numEnemyTracker.GetComponent<NumberOfEnemyTracker>().numberOfEnemies=numEnemyTracker.GetComponent<NumberOfEnemyTracker>().numberOfEnemies-1;


    }*/
    private IEnumerator hasCollided()
    {
        //gunAudio.Play();

        yield return new WaitForSeconds(1f);
        if (gameObject!=null){
            hasCollide = false;

        }

        
    }
}
