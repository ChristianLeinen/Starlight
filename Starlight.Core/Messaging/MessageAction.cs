namespace Starlight.Core.Messaging
{
    // pre-defined message actions supported by engine and subsystems
    public static class MessageAction
    {
        // CORE/ENGINE
        public const string ToggleMenu = "ToggleMenu";
        public const string Quit = "Quit";

        // INPUT-SYSTEM
        // change input mapping: Message<string>
        public const string InputMapping = "InputMapping";

        // general keystrokes: Message<Keys>
        public const string KeyboardKey = "Keyboard.Key"; // up and down message
        public const string KeyDown = "KeyDown";
        public const string KeyUp = "KeyUp";

        #region MouseInput
        // from mouse
        public const string MouseX = "Mouse.X"; // relative position, int
        public const string MouseY = "Mouse.Y"; // relative position, int
        // current position is also stored in session context
        public const string MousePosition = "Mouse.Position";   // absolute position, Point, only sent when not Auto-Recentering!

        public const string MouseLeftButton = "Mouse.LeftButton";
        public const string MouseMiddleButton = "Mouse.MiddleButton";
        public const string MouseRightButton = "Mouse.RightButton";
        public const string MouseXButton1 = "Mouse.XButton1";
        public const string MouseXButton2 = "Mouse.XButton2";

        // absolute scroll wheel values (int, multiple of 120)
        public const string MouseScrollWheelValue = "Mouse.ScrollWheelValue";
        public const string MouseHorizontalScrollWheelValue = "Mouse.HorizontalScrollWheelValue";
        // relative scroll wheel values, normalized (int [-1|1])
        public const string MouseScrollWheel = "Mouse.ScrollWheel";
        public const string MouseHorizontalScrollWheel = "Mouse.HorizontalScrollWheel";
        #endregion

        // to mouse
        // will re-center the cursor, usefull for 1st or 3rd person camera
        // current state is stored in session context
        public const string MouseAutoRecenter = "Mouse.AutoRecenter";   // bool

        // joystick
        public const string JoystickButton = "Joystick.Button";
        public const string JoystickAxis = "Joystick.Axis";
        public const string JoystickHat = "Joystick.Hat";

        // gamepad
        public const string GamePadButton = "GamePad.Button";
        public const string GamePadTriggerL = "GamePad.TriggerL";
        public const string GamePadTriggerR = "GamePad.TriggerR";
        public const string GamePadThumbStickL = "GamePad.ThumbStickL";
        public const string GamePadThumbStickR = "GamePad.ThumbStickR";
        public const string GamePadDPad = "GamePad.DPad";

        // AUDIO-SYSTEM
        // play a song async: Message<string> (name of song in Content library)
        public const string PlayMusic = "PlayMusic";
        public const string StopMusic = "StopMusic";
        public const string PauseMusic = "PauseMusic";
        public const string ResumeMusic = "ResumeMusic";
        // events when state/active song changes in music player
        public const string MediaStateChanged = "MediaPlayer.MediaStateChanged";
        public const string ActiveSongChanged = "MediaPlayer.ActiveSongChanged";
    }
}
