﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {

	public GameObject o_Player;
	public spawn generator;
	public DataBase s_Database;
	public CameraFollow s_CameraFollow = null;
	SavePoint theMostCloseSavePoint;
	void Start () {
		
	}
	
	void Update () {
		theMostCloseSavePoint = s_Database.theMostCloseSavePoint.GetComponent<SavePoint>();

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.tag == "Player")
		{
			FindObjectOfType<AudioManager> ().play ("Dead");
			s_CameraFollow.StartCoroutine(s_CameraFollow.CameraShake(0.15f,0.4f));
			generator.ThrowCube(new Vector2 (0,0));
			generator.DestroyCube();
			StartCoroutine(wait(1f));
		}
		if(col.tag =="FallDownBlock")
		{
			col.transform.position = new Vector3(col.transform.position.x,col.transform.position.y+20,col.transform.position.z);
		}
	}
	IEnumerator wait(float time)
	{
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSecondsRealtime(time);
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		o_Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		o_Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		o_Player.transform.position = s_Database.SavePoint;
		if(theMostCloseSavePoint!=null)
		{
			s_Database.maxCube = theMostCloseSavePoint.maxCube;
			s_Database.UsedCube = theMostCloseSavePoint.UsedCube;
			s_Database.AchievementCount =theMostCloseSavePoint.AchievementCount;
			s_Database.DeathCount +=1;
			for(int i =0;i<s_Database.AchievementCheck.Length;i++)
			{
				s_Database.AchievementCheck[i] = theMostCloseSavePoint.AchievementCheck[i];
				if(theMostCloseSavePoint.AchievementCheck[i])
				{
					s_Database.Achievement[i].SetActive(true);
					s_Database.Achievement[i].GetComponent<Achievement>().Reset();
				}
			}
		}
	}
}
