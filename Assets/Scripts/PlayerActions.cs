using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using MultiplayerBasicExample;

public class PlayerActions : PlayerActionSet
{
    public PlayerAction LLeft;
    public PlayerAction LRight;
    public PlayerAction LUp;
    public PlayerAction LDown;
    public PlayerTwoAxisAction MovePlayer;

    public PlayerAction RLeft;
    public PlayerAction RRight;
    public PlayerAction RUp;
    public PlayerAction RDown;
    public PlayerTwoAxisAction RotateCamera;

    public PlayerAction Interact;
    public PlayerAction Run;
    public PlayerAction Crouch;
    public PlayerAction Pause;


    public PlayerActions()
    {
        LLeft = CreatePlayerAction("Move Left");
        LRight = CreatePlayerAction("Move Right");
        LUp= CreatePlayerAction("Move Up");
        LDown = CreatePlayerAction("Move Down");
        MovePlayer = CreateTwoAxisPlayerAction(LLeft, LRight, LDown, LUp);

        RLeft = CreatePlayerAction("Rotate Left");
        RRight = CreatePlayerAction("Rotate Right");
        RUp = CreatePlayerAction("Rotate Up");
        RDown = CreatePlayerAction("Rotate Down");
        RotateCamera = CreateTwoAxisPlayerAction(RLeft, RRight, RDown, RUp);

        Interact = CreatePlayerAction("Interact");
        Run = CreatePlayerAction("Run");
        Crouch = CreatePlayerAction("Crouch");
        Pause = CreatePlayerAction("Pause");
    }

    public static PlayerActions CreateWithAllBindings()
    {
        var actions = new PlayerActions();
        actions.LLeft.AddDefaultBinding(Key.A);
        actions.LRight.AddDefaultBinding(Key.D);
        actions.LUp.AddDefaultBinding(Key.W);
        actions.LDown.AddDefaultBinding(Key.S);
        
        actions.RLeft.AddDefaultBinding(Key.LeftArrow);
        actions.RRight.AddDefaultBinding(Key.RightArrow);
        actions.RUp.AddDefaultBinding(Key.UpArrow);
        actions.RDown.AddDefaultBinding(Key.DownArrow);
        actions.RLeft.AddDefaultBinding(Mouse.NegativeX);
        actions.RRight.AddDefaultBinding(Mouse.PositiveX);
        actions.RUp.AddDefaultBinding(Mouse.NegativeY);
        actions.RDown.AddDefaultBinding(Mouse.PositiveY);
        
        actions.Interact.AddDefaultBinding(Key.Space);
        actions.Run.AddDefaultBinding(Key.Shift);
        actions.Crouch.AddDefaultBinding(Key.Control);
        actions.Pause.AddDefaultBinding(Key.Return);
        actions.Pause.AddDefaultBinding(Key.Escape);


        actions.LLeft.AddDefaultBinding(InputControlType.LeftStickLeft);
        actions.LRight.AddDefaultBinding(InputControlType.LeftStickRight);
        actions.LUp.AddDefaultBinding(InputControlType.LeftStickUp);
        actions.LDown.AddDefaultBinding(InputControlType.LeftStickDown);
        actions.RLeft.AddDefaultBinding(InputControlType.RightStickLeft);
        actions.RRight.AddDefaultBinding(InputControlType.RightStickRight);
        actions.RUp.AddDefaultBinding(InputControlType.RightStickUp);
        actions.RDown.AddDefaultBinding(InputControlType.RightStickDown);


        actions.Interact.AddDefaultBinding(InputControlType.Action1);
        actions.Run.AddDefaultBinding(InputControlType.LeftBumper);
        actions.Crouch.AddDefaultBinding(InputControlType.Action2);
        actions.Pause.AddDefaultBinding(InputControlType.Start);

        return actions;
    }


}
