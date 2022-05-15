using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LizardEnemyController1 : MonoBehaviour
{
    public Animator anim;
    public float lookRadius=20f;
    public float stoppingDistance=5f;
    public Material damagedMat;
    public ParticleSystem deathParticles;
    GameObject Player;
    public GameObject numEnemyTracker;
    Transform target;
    NavMeshAgent agent;
    public int health=10;
    [SerializeField] private GameObject arrow;
    float enemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        Player=GameObject.FindGameObjectWithTag("Player");
        //numEnemyTracker=GameObject.FindGameObjectWithTag("EnemyNumberTracker");
        target=Player.transform;
        anim=GetComponent<Animator>();

        enemySpeed=agent.velocity.magnitude;
        
    }
    
    // Update is called once per frame p
    void Update()
    {
        enemySpeed=agent.velocity.magnitude;
        //anim.SetFloat("Speed", );
        Debug.Log(enemySpeed);

        float distance =Vector3.Distance(target.position,transform.position);
        /*if( distance<=lookRadius)
        {*/
            
            FaceTarget();
            if(distance<stoppingDistance){
                //agent.ResetPath();
                //agent.SetDestination(target.position);
                agent.speed=0;
                anim.SetFloat("Speed", 0f);
                ShootTarget();
            }

            else if(distance >= stoppingDistance)
            {
                Debug.Log("distance >= stoppingDistance");

                FaceTarget();//face player
                ShootTarget();//Attack player
                //agent.SetDestination(target.position);
                agent.speed=10;
                anim.SetFloat("Speed", 6f);
                
            }
            
        //}

        if(health<=0){
            Destroy(gameObject);
        }

    }
    private bool canShoot=true;
    void ShootTarget()
    {
        if (canShoot==false){
            return;
        }
        anim.SetBool("IsShooting",true);
        canShoot=false;
        StartCoroutine(Shooting());


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
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }

private bool hasCollide = false;
    void OnTriggerEnter(Collider col){
        if(hasCollide==false){

            if(col.name=="E_ThrowingAxe(Clone)" || col.name=="E_ReturnAxe(Clone)"){
            health=health-5;
            GetComponent<Renderer>().material=damagedMat;
            StartCoroutine(hasCollided());
            }

        }

        Debug.Log(col.name);
    }
    void OnDestroy(){
        ParticleSystem psEnemy=Instantiate(deathParticles,gameObject.transform.position, Quaternion.Euler(-90f,0f,0f));
        var em=psEnemy.GetComponent<ParticleSystem>().emission;
        em.enabled=true;
        //Instantiate(deathParticles,gameObject.transform) as GameObject;
        Debug.Log("Enemy death particles");
        
        numEnemyTracker.GetComponent<NumberOfEnemyTracker>().numberOfEnemies=numEnemyTracker.GetComponent<NumberOfEnemyTracker>().numberOfEnemies-1;


    }
    private IEnumerator hasCollided()
    {
        //gunAudio.Play();

        yield return new WaitForSeconds(1f);

        hasCollide = false;
    }
    private IEnumerator Shooting(){
        GameObject thrownArrow=Instantiate(arrow, transform.position, Quaternion.identity);
        thrownArrow.GetComponent<Rigidbody>().AddForce(transform.forward);

        yield return new WaitForSeconds(2.5f);
        anim.SetBool("IsShooting",false);
        canShoot=true;

    }
}
