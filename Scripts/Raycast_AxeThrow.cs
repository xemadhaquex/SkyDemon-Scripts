using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_AxeThrow : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;    
    public GameObject axeAttach;
    [SerializeField] private GameObject lowerArmGO;
    private Animator anim;
    

    public float axeThrowForce=10f;
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private GameObject returnAxePrefab;
    [SerializeField] private GameObject Cam;           
    private Camera fpsCam;                                        
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    private Transform axeAttachTransform;
    private Vector3 fakeHitPosition;
    public Vector3 axeLandingPos;
    public bool canThrowAxe=true;
    public bool canReturnAxe=false;
    public int throwsRemaining=15;


    void Start () 
    {
        laserLine = GetComponent<LineRenderer>();
        
        anim=lowerArmGO.GetComponent<Animator>();

        //throwsRemaining=10;

        gunAudio = GetComponent<AudioSource>();

        fpsCam = Cam.GetComponent<Camera>();
        axeAttachTransform = axeAttach.transform;
    }


    void Update () 
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire) 
        {
            if(canThrowAxe==false || (throwsRemaining>0)==false){
                return;
            }
            throwsRemaining=throwsRemaining-1;
            nextFire = Time.time + fireRate;

            StartCoroutine (ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition (0, axeAttachTransform.position);

            GameObject axe= Instantiate(axePrefab,axeAttachTransform.position, Quaternion.identity);
            //axe.transform.position=axeAttachTransform.position;

            /*if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                axeLandingPos=hit.point;
                laserLine.SetPosition (1, axeLandingPos);
                
                axe.GetComponent<Rigidbody>().velocity=axeThrowForce*(axeLandingPos - transform.position).normalized;
                axe.transform.rotation=Quaternion.LookRotation(axe.GetComponent<Rigidbody>().velocity);

                canThrowAxe=false;
                canReturnAxe=true;


                /*ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                if (health != null)
                {
                    health.Damage (gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce (-hit.normal * hitForce);
                }*/
            //}
            //else
            //{
                //Debug.Log("no raycast hit");
            axeLandingPos= (rayOrigin + (fpsCam.transform.forward * weaponRange));
            laserLine.SetPosition (1, axeLandingPos);
            axe.GetComponent<Rigidbody>().velocity=axeThrowForce*(fpsCam.transform.forward);
            axe.transform.rotation=Quaternion.LookRotation(axe.GetComponent<Rigidbody>().velocity);

            canThrowAxe=false;
            canReturnAxe=true;


            //}
        }
        if (Input.GetButtonDown("Fire2") && Time.time > nextFire) 
        {
            if(canReturnAxe==false){
                return;
            }

            //Destroy(axe);
            nextFire = Time.time + fireRate;

            StartCoroutine (ShotEffect());

            Vector3 rayOrigin = axeAttachTransform.position;

            //RaycastHit hit;

            laserLine.SetPosition (0, axeLandingPos);

            GameObject returnAxe= Instantiate(returnAxePrefab,axeLandingPos, Quaternion.identity);
            

            laserLine.SetPosition (1, axeAttachTransform.position);
                
            returnAxe.GetComponent<Rigidbody>().velocity=axeThrowForce*2*(axeAttachTransform.position - axeLandingPos).normalized;
            returnAxe.transform.rotation=Quaternion.LookRotation(returnAxe.GetComponent<Rigidbody>().velocity);
            canReturnAxe=false;

            if(throwsRemaining==0){

                gameObject.GetComponent<PlayerMovement>().OutOfAxes();

            }
            
        }
    }


    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();
        anim.Play("ThrowAxeState");

        //laserLine.enabled = true;

        yield return shotDuration;

        //laserLine.enabled = false;
        anim.Play("Idle");
    }
}
