using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets;


public class UiManager : MonoBehaviour
{
  CustomMenu mMenu = new CustomMenu(new Vector2(80, 40))
	{ 
		{ "CreateTile",CreateTile},
		{ "asd2",() => { print("Menu item 2 has been pressed.");} },
	};

  public const int cDistance = 4;

  static Camera MainCamera { get { return Camera.main; } }

  // Use this for initialization
  void Start()
  {
  }

  static void CreateTile()
  {
    var wTile = GameObject.CreatePrimitive(PrimitiveType.Plane);

    wTile.transform.localScale = new Vector3(30, 30, 30);
    wTile.GetComponent<Renderer>().material.color = new Color(0,0,1);

   //wTile.tag = Guid.NewGuid().ToString();
    wTile.transform.position = MainCamera.transform.position + MainCamera.transform.forward * 20;
    wTile.transform.rotation = new Quaternion(0.0f, MainCamera.transform.rotation.y, 0.0f, MainCamera.transform.rotation.w);
    
    ContextMenu.CreateComponent(wTile, MainCamera);

    MoveInfrontOfCamera(wTile, MainCamera, 20);
  }


  void OnGUI()
  {
    mMenu.DrawMenu(-Screen.width/30,50,MainCamera);
  }


  /// <summary>
  /// Helper method for translating objects relative to a camera's position.
  /// </summary>
  /// <param name="gameObject">Object to be Moved.</param>
  /// <param name="camera">The camera that the Movment will be relative to.</param>
  /// <param name="distanceVector">Distance to be moved</param>
  static void MoveRelativeToCamera(GameObject gameObject, Camera camera, Vector3 distanceVector)
  {

    if (gameObject != null)
    {
      gameObject.transform.position = camera.transform.position + distanceVector;
      gameObject.transform.rotation = new Quaternion(0.0f, camera.transform.rotation.y, 0.0f, camera.transform.rotation.w);
    }
  }

  /// <summary>
  ///  Helper method for translating objects forward relative to a camera's position.
  /// </summary>
  /// <param name="gameObject">Object to be Moved.</param>
  /// <param name="camera">The camera that the Movment will be relative to.</param>
  /// <param name="distance">Distance to be moved</param>
  static void MoveInfrontOfCamera(GameObject gameObject, Camera camera, int distance)
  {
    MoveRelativeToCamera(gameObject, camera, camera.transform.forward * distance);
  }

  // Update is called once per frame
  private void Update()
  {
  }
}
