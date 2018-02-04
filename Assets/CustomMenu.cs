using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EventSystems;
using UnityEngine;

namespace Assets
{
  public class CustomMenu : Dictionary<string, Action>
  {
    private Vector2 mMenuItemSize;
    private bool mMenuCreated = false;
    private bool mVisible = false;
    public bool Visible
    {
      get { return mVisible; }
      set
      {
        mVisible = value;
        Updatevisibility();
      }
    }

    private void Updatevisibility()
    {
      foreach (var wButton in mButtons)
      {
        wButton.GetComponent<Renderer>().enabled = mVisible;
      }
    }

    public CustomMenu(Vector2 itemSize)
    {
      mMenuItemSize = itemSize;
    }

    private List<GameObject> mButtons = new List<GameObject>();

    public void DrawMenu(int x = 10, int y = 100, Component component = null)
    {
      if (!mMenuCreated)
        CreateMenuBar(x, y, component);
    }

    private void CreateMenuBar(int x = 10, int y = 100, Component component = null)
    {
      var wKeys = Keys.ToList();
      var wComponent = component ?? Camera.main;
      for (var wIndex = 1; wIndex <= Count; wIndex++)
      {
        var wButton = CreateMenuButton(wKeys[wIndex - 1]);
        if (component != null)
        {
          var wCamera = component as Camera;
          if (wCamera != null)
          {

            wButton.transform.Translate(
              (-Screen.width / 2 + x),
              wCamera.transform.position.y - (y * wIndex),
              0, wComponent.transform);
            wButton.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
          }
          else
          {
            var wRenderer = component as Renderer;
            if (wRenderer != null)
            {
              var wBounds = wRenderer.bounds;

              wButton.transform.Translate(
                (-wBounds.size.x / 2) + x, 0,
               wBounds.size.z - (y * wIndex), wComponent.transform);
            }
            wButton.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
          }

        }
        mButtons.Add(wButton);
      }
      mMenuCreated = true;
      Visible = true;
    }

    /*private void Redraw(int x = 10, int y = 100, Component component = null)
    {
      for (var wIndex = 1; wIndex <= mButtons.Count; wIndex++)
      {
        if (component != null)
        {
          var wButton = mButtons[wIndex - 1];
          var wCamera = component as Camera;
          if (wCamera != null)
          {
            wButton.transform.Translate(
              (-(Screen.width / 2) + x),
              wCamera.transform.position.y - (y * wIndex),
              0);
            wButton.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
          }
        }
      }
    }*/

    GameObject CreateMenuButton(string key)
    {
      Action wAction;
      TryGetValue(key, out wAction);
      if (wAction == null) return null;

      var wButton = GameObject.CreatePrimitive(PrimitiveType.Plane);
      wButton.transform.localScale = new Vector3(mMenuItemSize.x / 10, 1, mMenuItemSize.y / 10);
      wButton.name = key;


      var wTextObject = new GameObject(key + "_Text");
      var wText = wTextObject.AddComponent<TextMesh>();
      wText.transform.Rotate(new Vector3(90, 0, 0));

      var wTextSize = wText.GetComponent<Renderer>().bounds.size;
      var wButtonSize = wButton.GetComponent<Renderer>().bounds.size;

      int wSize;
      for (wSize = 100; wTextSize.x < wButtonSize.x && wSize < 150; wSize += 10)
      { }

      wText.fontSize = wSize;
      wText.fontStyle = FontStyle.Bold;
      wText.anchor = TextAnchor.MiddleCenter;
      wText.color = new Color(0, 0, 0);
      wText.text = key;

      wTextObject.transform.parent = wButton.transform;

      MouseHandler.CreateComponent(wButton, wAction);

      return wButton;
    }

  }
}
