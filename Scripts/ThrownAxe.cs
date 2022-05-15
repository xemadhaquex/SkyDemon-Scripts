using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownAxe : MonoBehaviour
{
    public GameObject axe;
    public GameObject ReturnAxe;
    GameObject Player;
    //public float launchVelocity = 700f;
    // Start is called before the first frame update
    public int spinSpeed=2;
    GameObject[] HoldingAxe;

    int i=0;

    void Start()
    {
        axe.transform.Rotate(new Vector3(0f,90f,0f));
        Player=GameObject.FindGameObjectWithTag("Player");
        HoldingAxe=GameObject.FindGameObjectsWithTag("HoldingAxe");
        
        foreach(GameObject axes in HoldingAxe){
            Destroy(axes);
        }
        
        Destroy(gameObject, 5);
    
    }

    // Update is called once per frame
    void Update()
    {
        //axe.transform.rotation= Quaternion.Euler(new Vector3(0f,0f,0f)); 
        axe.transform.Rotate(new Vector3(0f,0f,2f)); 

        i=i+spinSpeed;
        
        
    }

    void OnTriggerEnter(Collider col){

        //Vector3 colPosition = col.transform.position;

        Debug.Log("axe hit" + col.name);
        //Instantiate(ReturnAxe,colPosition,Quaternion.identity);

        //Destroy(gameObject);


    }
    void OnCollisionEnter(Collision col){
        
        //Debug.Log("axe collision");

        Vector3 colPosition = col.contacts[0].point;

        //Debug.Log("axe hit" + col.contacts[0].name);
        
        Player.GetComponent<Raycast_AxeThrow>().axeLandingPos=colPosition;;

        //Destroy(gameObject);


    }

    void OnDestroy(){
        Player.GetComponent<Raycast_AxeThrow>().axeLandingPos=transform.position;
        //Instantiate(ReturnAxe,gameObject.transform);
    }

    
}
