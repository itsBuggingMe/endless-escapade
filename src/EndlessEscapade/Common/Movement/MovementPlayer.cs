using EndlessEscapade.Common.Ambience;

namespace EndlessEscapade.Common.Movement;

// The game doesn't set the player's old velocity, so we have to do it ourselves.
public sealed class MovementPlayer : ModPlayer
{
    public override void PostUpdate() {
        base.PostUpdate();

        Player.oldVelocity = Player.velocity;

    }
}
