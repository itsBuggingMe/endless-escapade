namespace EndlessEscapade.Content.Biomes;

public sealed class ShipyardBiome : ModBiome
{
    public override SceneEffectPriority Priority { get; } = SceneEffectPriority.BiomeHigh;

    public override int Music => MusicLoader.GetMusicSlot(Mod, $"Assets/Sounds/Music/Shipyard{(Main.dayTime ? "Day" : "Night")}");

    public override bool IsBiomeActive(Player player)
    {
        return player.ZoneBeach && player.position.X / 16f < Main.maxTilesX / 2f;
    }

    public override void OnInBiome(Player player)
    {
        base.OnInBiome(player);

        // TODO: Spawn Sailor's particles.
    }
}