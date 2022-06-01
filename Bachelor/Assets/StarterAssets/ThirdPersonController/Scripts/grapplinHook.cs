using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class grapplinHook : MonoBehaviour
{
    public Camera cam;
	public RaycastHit hit;

    public LayerMask surfaces;
	public int maxDistance;
	
	public bool isMoving;
	public Vector3 location;
	
	public float speed = 15;
	public Transform hook;
	
	public ThirdPersonController TPC;
	public LineRenderer LR;

	private void Start()
	{
		LR.enabled = false;
	}

	void Update(){

		// Envoi du grappin
		if(Input.GetKey(KeyCode.A)){
			Grapple();
		}
		
        // Si le personnage vole, on l'envoie vers le point d'arrivée 
		if(isMoving)
        {
			MoveToSpot();
		}
		
        // Annulation / décrochage du grappin
		if(Input.GetKey(KeyCode.Space) && isMoving)
        {
            isMoving = false;
            TPC.CanMove = true;
			LR.enabled = false;
            gameObject.GetComponent< Rigidbody>().useGravity = true;
            TPC.Gravity = 2;
        }
		
		if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance, surfaces)){
			Vector3 positionJoueur = new Vector3(TPC.transform.position.x, TPC.transform.position.y, TPC.transform.position.z);
			LR.SetPosition(0, positionJoueur);
		}
	}
	
    // Lors de l'envoi du grappin
	public void Grapple(){
        // On créé un raycast de "maxDistance" unités depuis la caméra vers l'avant.
        // Si ce raycast touche quelque chose c'est que le grappin est utilisable
		if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistance, surfaces)){
            isMoving = true;
			location = hit.point;
			TPC.CanMove = false;
            //gameObject.GetComponent< Rigidbody>().useGravity = false;
            Physics.gravity = Vector3.zero;
			LR.enabled = true;
			Vector3 positionJoueur = new Vector3(TPC.transform.position.x, TPC.transform.position.y, TPC.transform.position.z);
			LR.SetPosition(0, positionJoueur);
			Debug.Log(positionJoueur);
		}

    }

	// Déplacement du joueur vers le point touché par le grappin
	public void MoveToSpot(){
		transform.position = Vector3.Lerp(transform.position, location, speed * Time.deltaTime / Vector3.Distance(transform.position, location));
		LR.SetPosition(1, location);
		
        // Si on est à  - de 1 unité(s) de la cible finale on décroche le grappin automatiquement
		if(Vector3.Distance(transform.position, location) < 2f){
            isMoving = false;
            TPC.CanMove = true;
			LR.enabled = false;
            gameObject.GetComponent< Rigidbody>().useGravity = true;
            TPC.Gravity = -100;
		}
	}
}
