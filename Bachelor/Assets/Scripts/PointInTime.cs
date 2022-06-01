using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{

	public Vector3 position;
	public Quaternion rotation;
	public Animator anim;

	public PointInTime(Vector3 _position, Quaternion _rotation, Animator _anim)
	{
		position = _position;
		rotation = _rotation;
		anim = _anim;
	}

}
