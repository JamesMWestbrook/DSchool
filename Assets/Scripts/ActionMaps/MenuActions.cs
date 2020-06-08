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

        //controller
        actions.Left.AddDefaultBinding(InputControlType.DPadLeft);
        actions.Right.AddDefaultBinding(InputControlType.DPadRight);

        actions.Up.AddDefaultBinding(InputControlType.DPadUp);
        actions.Down.AddDefaultBinding(InputControlType.DPadDown);

        actions.Confirm.AddDefaultBinding(InputControlType.Action1);
        actions.Present.AddDefaultBinding(InputControlType.Action4);

        return actions;
    }


}
