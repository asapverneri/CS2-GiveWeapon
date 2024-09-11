using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace GiveWeapon
{
    public class GiveWeaponConfig : BasePluginConfig
    {

		[JsonPropertyName("AccessFlag")]
		public string AccessFlag { get; set; } = "@css/generic";

        [JsonPropertyName("Command")]
        public string Command { get; set; } = "css_weapon";

        [JsonPropertyName("DropActiveWeapon")]
        public bool DropActiveWeapon { get; set; } = false;

    }
}
