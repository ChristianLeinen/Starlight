using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Starlight.Core
{
    [Flags]
    public enum ModifierKeyState
    {
        None = 0,
        LeftPressed = 0x01,
        RightPressed = 0x02,
        BothPressed = LeftPressed | RightPressed
    }

    public static class SessionContext
    {
        #region general graphics related stuff, provided by game, consider read-only!
        public static GraphicsDeviceManager GraphicsDeviceManager;
        public static int FpsUpdate;
        public static int FpsDraw;
        #endregion

        #region general input related stuff, provided by input system, consider read-only!
        public static bool AutoRecenter;
        public static Point CursorPosition;
        public static ModifierKeyState ShiftKeyState;
        public static ModifierKeyState AltKeyState;
        public static ModifierKeyState ControlKeyState;
        #endregion

        public static ICamera ActiveCamera;

        // variable data, free to read and modify for all
        public static IDictionary<string, object> Properties { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// Uses <see cref="IDictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>;
        /// returns <c>default(T)</c> if the key is not present.
        /// </summary>
        public static T Get<T>(string key)
        {
            if (SessionContext.Properties.TryGetValue(key, out object value) && value is T)
            {
                return (T)value;
            }
            return default;
        }
    }
}
