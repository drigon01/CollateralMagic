using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UiManager : MonoBehaviour {

	Dictionary<string, Action> mMenu = new Dictionary<string, Action> 
	{ 
		{ "CreateTile",CreateTile},
		{ "asd2",() => { print("Menu item 2 has been pressed.");} },
	};

	Vector2 cMenuItemSize = new Vector2(80,40);

	// Use this for initialization
	void Start () {
		//Camera.main.orthographic = true;
		Camera.main.transform.Rotate (new Vector3(90, 0, 0)); 
		Camera.main.transform.Translate (new Vector3 (0, 400, 0));
	}
			

	void OnGUI()
	{
		CreateMenuBar();
	}

	static void CreateTile()
	{
		var wCube = GameObject.CreatePrimitive (PrimitiveType.Cube);

		wCube.tag = Guid.NewGuid().ToString();
	}

	void CreateMenuBar()
	{
		for(var wIndex=0; wIndex < mMenu.Count; wIndex++)
		{
			var wKey=mMenu.Keys.ToList()[wIndex];
			if (GUI.Button(new Rect (new Vector2 (10, 10 + wIndex * cMenuItemSize.y), cMenuItemSize), mMenu.Keys.ToList()[wIndex]))
				mMenu[wKey]();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

}
