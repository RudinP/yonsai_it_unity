using UnityEngine;
using System.Collections; 

public class Move : MonoBehaviour
{
	public float MoveSpeed;
	Vector3 lookDirection;
		
	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftArrow)  ||
		    Input.GetKey (KeyCode.RightArrow) ||
		    Input.GetKey (KeyCode.UpArrow)    ||
		    Input.GetKey (KeyCode.DownArrow)) 
		{
			float zz = Input.GetAxisRaw ("Vertical");
			float xx = Input.GetAxisRaw ("Horizontal");	
			lookDirection = zz * Vector3.forward + xx * Vector3.right;		
			
			this.transform.rotation = Quaternion.LookRotation (lookDirection);
			this.transform.Translate (Vector3.forward * MoveSpeed * Time.deltaTime);	
		}
	}
	
}