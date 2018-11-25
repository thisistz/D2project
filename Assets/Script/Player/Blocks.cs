﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour {
	float speed = 50.0f;
	float amount = 0.08f;
	private GameObject [] child;
	public bool destory = false;

    private Rigidbody2D rb;
	bool GroundChecked = false;
	void Start () {
        child = GameObject.FindGameObjectsWithTag("generated");
        rb =gameObject.GetComponent<Rigidbody2D>();
        foreach (GameObject cube in child)
        {
            cube.GetComponent<BoxCollider2D>().size = new Vector2(1.25f,1.26f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(destory)
		{
			foreach(GameObject cube in child)
			{
				if(cube!=null){
					GameObject DisappearSmoke =  Instantiate(Resources.Load("DisappearSmoke"), cube.transform) as GameObject;
					DisappearSmoke.transform.position = cube.transform.position;
					DisappearSmoke.transform.parent =null;
				}
				
			}
			Destroy(this.gameObject);
			destory = false;
		}
		if (Input.GetKey (KeyCode.Z) && rb.velocity.y ==0) {
			transform.position = new Vector2(this.transform.position.x+Mathf.Sin (Time.time * speed) * amount,transform.position.y);
			foreach(GameObject cube in child)
			{
				if(cube!=null)
				cube.layer = 0;
			}
		}
		else if(Input.GetKeyUp(KeyCode.Z) && child[0].layer==0)
		{
			foreach(GameObject cube in child)
			{
				if(cube!=null)
				cube.layer = 9;
			}
		}
        if (this.GetComponent<Rigidbody2D>().velocity.y == 0 ||GroundChecked )
        {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
			if(!GroundChecked)
			{
				for(int i=0; i<child.Length;i++)
				{
					if(child[i]!=null)
					{
						if(child[i].GetComponent<SpawningBlocks>().Downwallchecker)
						{
							GroundChecked = true;
							break;
						}
					}
					
				}
			}


		}
	}
}
