using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    bool isRewinding = false;
	public bool isActive = false;

    public float recordTime = 5f;

    List<PointInTime> pointsInTime;
	public Movement Movement;

	public Animator animator;
	public Animation animation;

	//Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			isActive = true;
			if (isActive)
			{
				StartRewind();
				isActive = false;
			}
			else
			{
				StopRewind();
			}

		}
		Debug.Log(animator.GetCurrentAnimatorStateInfo(0).speed);
    }
	void FixedUpdate()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
			transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
			//rb.useGravity = false;
			animator.SetFloat("Speed", -1);
			animator.speed = 1f;
			
			animation.Blend("2");
			animation.Play("Run_N");
		}
		else
		{
			StopRewind();
		}

	}

	void Record()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}
		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, animator));
		//pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));

		//rb.useGravity = true;
	}

	public void StartRewind()
	{
		isRewinding = true;
		//rb.isKinematic = true;
	}

	public void StopRewind()
	{
		isRewinding = false;
		//rb.isKinematic = false;
	}
}
