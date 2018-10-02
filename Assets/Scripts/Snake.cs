﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Snake : MonoBehaviour {

	Snake next;
	static public Action<String> hit;

	public void setNext(Snake IN)
	{
		next = IN;
	}

	public Snake getNext()
	{
		return next;
	}

	public void removeTail()
	{
	
		Destroy (this.gameObject);
	}

 void OnTriggerEnter(Collider other)
	{
		
		if(hit != null)
		{
			hit(other.tag);
		}
		if(other.tag == "Food")
		{
			Destroy(other.gameObject);

		}
	}


}
