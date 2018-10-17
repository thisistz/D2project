﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
	float speed = 50.0f;
	float amount = 0.08f;
	private GameObject [] child;
	public bool destory = false;
	//public Particle DisappearSmoke;
	// Use this for initialization
	void Start () {
		child = GameObject.FindGameObjectsWithTag ("generated");
	}
	
	// Update is called once per frame
	void Update () {
		if(destory)
		{
			foreach(GameObject cube in child)
			{
				GameObject DisappearSmoke =  Instantiate(Resources.Load("DisappearSmoke"), cube.transform) as GameObject;
				DisappearSmoke.transform.position = cube.transform.position;
				DisappearSmoke.transform.parent =null;
			}
			Destroy(this.gameObject);
			destory = false;
		}
		if (Input.GetKey (KeyCode.Z) && this.GetComponent<Rigidbody2D>().velocity.y ==0) {
			transform.position = new Vector2(this.transform.position.x+Mathf.Sin (Time.time * speed) * amount,transform.position.y);
		}
        if (this.GetComponent<Rigidbody2D>().velocity.y == 0 || this.GetComponent<Rigidbody2D>().velocity.y != 0)
        {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
        }

		
	}
}