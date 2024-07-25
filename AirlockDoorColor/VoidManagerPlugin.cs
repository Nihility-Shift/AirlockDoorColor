using VoidManager.MPModChecks;

namespace AirlockDoorColor
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Client;

        public override string Author => "18107, Dragon";

        public override string Description => "Sets airlock button color for open doors to white";
    }
}
