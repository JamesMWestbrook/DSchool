using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public class MenuActions : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerOneAxisAction Horizontal;

    public PlayerAction Up;
    public PlayerAction Down;
    public PlayerOneAxisAction Vertical;

    public PlayerAction Confirm;
    public PlayerAction Present;

    public PlayerAction KeySwitch;


    public MenuActions()
    {
         Left = CreatePlayerAction("Move left");
         Right = CreatePlayerAction("move right");
         Horizontal = CreateOneAxisPlayerAction(Left, Right);

         Up = CreatePlayerAction("Move up");
         Down = CreatePlayerAction("Move down");
         Vertical = CreateOneAxisPlayerAction(Down, Up);

         Confirm = CreatePlayerAction("Confirm");
         Present = CreatePlayerAction("Present item");

        KeySwitch = CreatePlayerAction("Switch to/from Key items");
    }

    public static MenuActions CreateWithAllBindings()
    {
        var actions = new MenuActions();

        actions.Left.AddDefaultBinding(Key.LeftArrow);
        actions.Left.AddDefaultBinding(Key.A);
        actions.Right.AddDefaultBinding(Key.RightArrow);
        actions.Right.AddDefaultBinding(Key.D);

        actions.Up.AddDefaultBinding(Key.UpArrow);
        actions.Up.AddDefaultBinding(Key.W);
        actions.Down.AddDefaultBinding(Key.DownArrow);
        actions.Down.AddDefaultBinding(Key.S);

        actions.Confirm.AddDefaultBinding(Key.Space);
        actions.Present.AddDefaultBinding(Key.Return);

        actions.KeySwitch.AddDefaultBinding(Key.CapsLock);

        //controller
        actions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
        actions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
        actions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
        actions.Down.AddDefaultBinding(InputControlType.LeftStickDown);


        actions.Confirm.AddDefaultBinding(InputControlType.Action1);
        actions.Present.AddDefaultBinding(InputControlType.Action4);
        actions.KeySwitch.AddDefaultBinding(InputControlType.RightBumper);

        return actions;
    }


}
