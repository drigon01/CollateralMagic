using System;
using UnityEngine;

namespace Assets
{
  class MouseHandler : MonoBehaviour
  {
    private Action mouseDownAction;

    public static MouseHandler CreateComponent(GameObject gameObject, Action mouseDownAction)
    {
      var wMouseHandler = gameObject.AddComponent<MouseHandler>();
      wMouseHandler.mouseDownAction = mouseDownAction;
      return wMouseHandler;
    }

    void OnMouseDown()
    {
      mouseDownAction();
    }

  }
}
