using UnityEngine;
using System.Collections;

public class ChangeObjectAlpha : MonoBehaviour {

	
	void Start () {
	
	}
	
	void Update () {
		this.GetComponent<MeshRenderer>().material.color -= new Color(0,0,0, Time.deltaTime);
	}
}
