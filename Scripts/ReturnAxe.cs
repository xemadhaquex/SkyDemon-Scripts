using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnAxe : MonoBehaviour
{
    public GameObject axe;
    public GameObject thrownAxe;

    //public float launchVelocity = 700f;
    // Start is called before the first frame update
    public int spinSpeed=2;
    int i=0;
    GameObject Player;
    GameObject AxeHoldPoint;
    GameObject throwingAxe;

    public GameObject HoldingAxe;
    public float ReturnSpeed;

    void Start()
    {
        axe.transform.Rotate(new Vector3(0f,90f,0f));
        Player=GameObject.FindGameObjectWithTag("Player");
        AxeHoldPoint=GameObject.FindGameObjectWithTag("AxeHoldPoint");
        throwingAxe=GameObject.FindGameObjectWithTag("ThrowingAxe");
        Destroy(throwingAxe);

        Physics.IgnoreCollision(axe.GetComponent<Collider>(), thrownAxe.GetComponent<Collider>());
        Physics.IgnoreCollision(axe.GetComponent<Collider>(), Player.GetComponent<Collider>());
        Destroy(gameObject,0.8f);
        axe.GetComponent<Rigidbody>().velocity=(Player.transform.position-transform.position)*ReturnSpeed;
    
    }

    // Update is called once per frame
    void Update()
    {
        axe.transform.Rotate(new Vector3(0f,0f,5f));

        i=i-spinSpeed;

        
        
        
    }

    void OnTriggerEnter(Collider col){
        

        //Vector3 colPosition = col.transform.position;

        //Debug.Log("axe hit" + col.name + "on return");

        if( col.tag=="Player"){
 
            Destroy(gameObject);

        }


    }
    void OnDestroy(){
        
        Instantiate(HoldingAxe,AxeHoldPoint.transform);
        Player.GetComponent<Raycast_AxeThrow>().canThrowAxe=true;
        
        
    }
}
