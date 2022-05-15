using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float lookRadius=20f;
    public float stoppingDistance=5f;

    GameObject Player;
    Transform target;
    [SerializeField] private GameObject laser;

    private AudioSource audioSource;

    public float laserXOffset = 0.25f;
    public float laserYOffset = 1.6f;
    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("Player");
        //numEnemyTracker=GameObject.FindGameObjectWithTag("EnemyNumberTracker");
        target=Player.transform;
        audioSource=GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance =Vector3.Distance(Player.transform.position,transform.position);
        if(distance<=stoppingDistance){
            Debug.Log("turret in range");
            FaceTarget();
            ShootTarget();

        }

        
    }

    private bool canShoot=true;
    void ShootTarget()
    {
        if (canShoot==false){
            return;
        }

        audioSource.Play();
        
        GameObject shotLaser=Instantiate(laser, new Vector3(transform.position.x+laserXOffset ,(transform.position.y + laserYOffset), transform.position.z), transform.rotation);
        //thrownArrow.GetComponent<Rigidbody>().AddForce(transform.forward);

        //anim.Play("Throw");

        canShoot=false;
        StartCoroutine(Shooting());


    }

    void FaceTarget()
    {

        Vector3 direction = (Player.transform.position-transform.position).normalized;
        Quaternion lookRotation= Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation= Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*150f);
    }

    private IEnumerator Shooting(){

        yield return new WaitForSeconds(.5f);
        //anim.SetBool("IsShooting",false);
        canShoot=true;

    }

    void OnDrawGizmosSelected(){
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}
