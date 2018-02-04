using System.Linq;
using Assets;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{

  private CustomMenu mMenu;

  public ContextMenu()
  {
    mMenu = new CustomMenu(new Vector2(100, 40))
    { 
      { "Remove Tile", () =>
      {
        mMenu.Keys.ToList().ForEach(item => Destroy(GameObject.Find(item)));
        Destroy(this.gameObject);
        
      }},
      { "Move Tile",() => { print("Menu item 2 has been pressed.");} },
    };
  }


  public static ContextMenu CreateComponent(GameObject gameObject, Camera parameter)
  {
    var wContextMenu = gameObject.AddComponent<ContextMenu>();
    return wContextMenu;
  }

  private void OnMouseDown()
  {
    mMenu.DrawMenu(-80,50,gameObject.GetComponent<Renderer>());
  }
}

