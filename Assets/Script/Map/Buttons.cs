﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
	public sceneManager scene;
	public GameObject fadeOut;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartButtonClick(){
		scene.sceneChange("Tutorial");
		// fadeOut.SetActive(true);
		// StartCoroutine(wait(0.5f));
		
	}

	IEnumerator wait(float time){
		yield return new WaitForSecondsRealtime(time);
		SceneManager.LoadScene("Tutorial");
	}
}
