using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Humble.Events
{
    public class EventManager
    {

        class ObjContainer
        {
            public ObjContainer(IClickable cl, bool hovered = false, bool clicked = false) { Obj = cl; IsHoveredOn = hovered; HasBeenClicked = clicked; }
            public IClickable Obj;
            public bool IsHoveredOn;
            public bool HasBeenClicked;

            public void ToggleHover(bool h) { IsHoveredOn = h; }
            public void ToggleClicked(bool c) { HasBeenClicked = c; }

        }

        // TODO toggle event manager enable on/off
        bool _enabled;
        Rectangle _mousePosition;
        List<ObjContainer> _clickableObjects;

        public EventManager(bool enabled = true)
        {
            _mousePosition = new Rectangle(0, 0, 1, 1);
            _enabled = enabled;
            _clickableObjects = new List<ObjContainer>();
        }

        public bool RegisterClickable(IClickable c)
        {
            foreach (ObjContainer oc in _clickableObjects)
                if (oc.Obj == c)
                    return false;
            _clickableObjects.Add(new ObjContainer(c));
            return true;
        }

        public bool UnregisterClickable(IClickable c)
        {
            foreach (ObjContainer oc in _clickableObjects)
                if (oc.Obj == c)
                {
                    _clickableObjects.Remove(oc);
                    return true;
                }
            return false;
        }

        public void ProcessEvents()
        {
            if (!_enabled)
                return;

            _mousePosition.X = Mouse.GetState().X;
            _mousePosition.Y = Mouse.GetState().Y;
            bool mousedown = (Mouse.GetState().LeftButton == ButtonState.Pressed);

            for (int i = 0; i < _clickableObjects.Count; i++ )
            {
                ObjContainer cl = _clickableObjects[i];
                bool collision = cl.Obj.getClickableArea().Intersects(_mousePosition);
                if (cl.IsHoveredOn && !collision) // Case : the mouse moved out of the clickable area
                {
                    cl.Obj.OnHoverOut();
                    cl.ToggleHover(false);
                }
                else if (!cl.IsHoveredOn && collision) // Case : the mouse moved in the clickable area
                {
                    cl.Obj.OnHoverIn();
                    cl.ToggleHover(true);
                    cl.ToString();
                }
                if (!cl.HasBeenClicked && collision && mousedown) // Case : the mouse button was pressed on the clickable area
                {
                    cl.Obj.OnClickDown();
                    cl.ToggleClicked(true);
                }
                else if (!mousedown && cl.HasBeenClicked && collision) // Case : the mouse button was released in the clickable area
                {
                    cl.Obj.OnClick();
                    cl.Obj.OnClickUp();
                    cl.ToggleClicked(false);
                }
                else if (!mousedown && cl.HasBeenClicked) // Case : the mouse button was released outside of the clickable area
                {
                    cl.Obj.OnClickUp();
                    cl.ToggleClicked(false);
                }
            }

        }

    }
}
