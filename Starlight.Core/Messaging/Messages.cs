using System;

namespace Starlight.Core.Messaging
{
    /// <summary>
    /// Base class for all events that occur in the Starlight engine.
    /// </summary>
    public class ActionEventArgs : EventArgs
    {
        public string Action { get; }
        public ActionEventArgs(string action)
        {
            this.Action = action;
        }

        public override string ToString() => $"Message '{this.Action}'";
    }

    public class ActionEventArgs<T> : ActionEventArgs
    {
        public T Info { get; }
        public ActionEventArgs(string action, T info) : base(action)
        {
            this.Info = info;
        }

        public override string ToString() => $"Message '{this.Action}', Info: {this.Info}";
    }

    /// <summary>
    /// Base class for all input devices that support multiple instances, currently Joysticks and Gamepads.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeviceEventArgs<T> : ActionEventArgs<T>
    {
        public int DeviceIndex { get; }
        public DeviceEventArgs(string action, int deviceIndex, T info) : base(action, info)
        {
            this.DeviceIndex = deviceIndex;
        }

        public override string ToString() => $"Message '{this.Action}', Device: {this.DeviceIndex}, Info: {this.Info}";
    }

}
