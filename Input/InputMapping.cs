using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Starlight.Input
{
    [DataContract]
    public class InputMapping
    {
        [DataMember(IsRequired = true)]
        public IList<InputLayout> Layouts { get; set; }
    }

    [DataContract]
    public class InputLayout
    {
        [DataMember(IsRequired = true, Order = 0)]
        public string Name { get; set; }

        [DataMember(IsRequired = true, Order = 1)]
        public IList<InputCommand> Commands { get; set; }

        public override string ToString() => $"{Name}, Commands: {Commands?.Count}";
    }

    [DataContract]
    public class InputCommand
    {
        [DataMember(IsRequired = true, Order = 0)]
        public string Trigger { get; set; }

        [DataMember(IsRequired = true, Order = 1)]
        public string Action { get; set; }

        // for buttons/keys, means that only keydown/buttonpressed events are sent w/o params
        // otherwise up/release events are also sent and the param is a bool reflecting the state
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public bool IsToggle { get; set; }

        // for joysticks/gamepads only
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public int DeviceIndex { get; set; }

        // for joystick buttons/axes/hats
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public int TriggerIndex { get; set; }

        //[DataMember(EmitDefaultValue = false, Order = 3)]
        //public IDictionary<string, object> Conditions { get; set; }

        //[DataMember(EmitDefaultValue = false, Order = 4)]
        //public IDictionary<string, object> Params { get; set; }

        //public override string ToString() => $"{Trigger} {(IsToggle ? "(toggle)" : "")} ({Conditions?.Count ?? 0} conditions) -> {Action} ({Params?.Count ?? 0} parameters)";
        public override string ToString() => $"{Trigger} {(IsToggle ? "(toggle)" : "")} -> {Action}";
    }
}
