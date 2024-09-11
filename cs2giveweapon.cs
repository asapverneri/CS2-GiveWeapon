using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Logging;

namespace GiveWeapon;


public class GiveWeapon : BasePlugin, IPluginConfig<GiveWeaponConfig>
{
    public override string ModuleName => "GiveWeapon";
    public override string ModuleDescription => "Give weapon by command";
    public override string ModuleAuthor => "verneri";
    public override string ModuleVersion => "1.0";

    public GiveWeaponConfig Config { get; set; } = new();

    public void OnConfigParsed(GiveWeaponConfig config)
	{
        Config = config;
    }

    public override void Load(bool hotReload)
    {
        Logger.LogInformation($"Loaded (version {ModuleVersion})");
        AddCommand($"{Config.Command}", "give weapon", WeaponCommand);
    }

    public void WeaponCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (!AdminManager.PlayerHasPermissions(player, Config.AccessFlag))
        {
            command.ReplyToCommand($"{Localizer["no.access", Config.AccessFlag]}");
            return;
        }

        if (player != null)
        {
            string inputWeaponName = command.ArgByIndex(1)?.ToLower();
            string? matchedWeapon = null;

            
            foreach (var weapon in WeaponList)
            {
                if (weapon.Key.ToLower().Contains(inputWeaponName))
                {
                    matchedWeapon = weapon.Value;
                    break;
                }
            }

            if (matchedWeapon != null)
            {
                if (Config.DropActiveWeapon) {
                    player.DropActiveWeapon();
                }
                player.GiveNamedItem(matchedWeapon);
                command.ReplyToCommand($"{Localizer["weapon.given", matchedWeapon]}");
            }
            else
            {
                command.ReplyToCommand($"{Localizer["weapon.invalid", inputWeaponName]}");
            }
        }
    }

    private static readonly Dictionary<string, string> WeaponList = new Dictionary<string, string>
    {
        { "m4a4", "weapon_m4a1" },
        { "m4a1", "weapon_m4a1_silencer" },
        { "famas", "weapon_famas" },
        { "aug", "weapon_aug" },
        { "ak47", "weapon_ak47" },
        { "galil", "weapon_galilar" },
        { "sg556", "weapon_sg556" },
        { "scar20", "weapon_scar20" },
        { "awp", "weapon_awp" },
        { "ssg08", "weapon_ssg08" },
        { "g3sg1", "weapon_g3sg1" },
        { "mp9", "weapon_mp9" },
        { "mp7", "weapon_mp7" },
        { "mp5sd", "weapon_mp5sd" },
        { "ump45", "weapon_ump45" },
        { "p90", "weapon_p90" },
        { "bizon", "weapon_bizon" },
        { "mac10", "weapon_mac10" },
        { "usp", "weapon_usp_silencer" },
        { "p2000", "weapon_hkp2000" },
        { "glock", "weapon_glock" },
        { "elite", "weapon_elite" },
        { "p250", "weapon_p250" },
        { "fiveseven", "weapon_fiveseven" },
        { "cz75a", "weapon_cz75a" },
        { "tec9", "weapon_tec9" },
        { "revolver", "weapon_revolver" },
        { "deagle", "weapon_deagle" },
        { "nova", "weapon_nova" },
        { "xm1014", "weapon_xm1014" },
        { "mag7", "weapon_mag7" },
        { "sawedoff", "weapon_sawedoff" },
        { "m249", "weapon_m249" },
        { "negev", "weapon_negev" },
        { "taser", "weapon_taser" },
        { "zeus", "weapon_taser" }
    };
}