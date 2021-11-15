using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;
	[SerializeField] private float smooth= 5.0f;
	[SerializeField] private Vector3 offset = new Vector3(0, 2, -5);

	void  FixedUpdate (){
		transform.position = Vector3.Lerp (transform.position,
											 target.position + offset,
											 Time.deltaTime * smooth);
	}       
}
