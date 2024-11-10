using EndlessEscapade.Utilities.Extensions;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace EndlessEscapade.Core.Graphics;

public sealed class InventoryGraphicsGlobalItem : GlobalItem
{
    private const float SELECTED_ITEM_SCALE = 1f;
    private const float UNSELECTED_ITEM_SCALE = 0.8f;

    public static readonly SoundStyle InventoryTickSound = new($"{nameof(EndlessEscapade)}/Assets/Sounds/Custom/InventoryTick") {
        Volume = 0.5f,
        PitchVariance = 0.2f
    };

    public override bool InstancePerEntity { get; } = true;

    public float Scale { get; set; }
    public float Rotation { get; set; }

    public Vector2 Position;
    public Vector2 OldMouseScreen;

    public bool Hovering;
    public bool OldHovering;

    public override void Load() {
        base.Load();

        On_ItemSlot.MouseHover_ItemArray_int_int += ItemSlot_MouseHover_Hook;
        On_ItemSlot.DrawItemIcon += ItemSlot_DrawItemIcon_Hook;
    }

    public override bool PreDrawInInventory(
        Item item,
        SpriteBatch spriteBatch,
        Vector2 position,
        Rectangle frame,
        Color drawColor,
        Color itemColor,
        Vector2 origin,
        float scale
    ) {
        return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }

    private static void ItemSlot_MouseHover_Hook(On_ItemSlot.orig_MouseHover_ItemArray_int_int orig, Item[] inv, int context, int slot) {
        orig(inv, context, slot);
    }

    private static float ItemSlot_DrawItemIcon_Hook(
        On_ItemSlot.orig_DrawItemIcon orig,
        Item item,
        int context,
        SpriteBatch spriteBatch,
        Vector2 screenPositionForItemCenter,
        float scale,
        float sizeLimit,
        Color environmentColor
    ) {
        var inventory = context == ItemSlot.Context.InventoryItem
            || context == ItemSlot.Context.InventoryAmmo
            || context == ItemSlot.Context.InventoryCoin
            || context == ItemSlot.Context.ChestItem
            || context == ItemSlot.Context.BankItem
            || context == ItemSlot.Context.VoidItem
            || context == ItemSlot.Context.MouseItem
            || context == ItemSlot.Context.TrashItem
            || context == ItemSlot.Context.HotbarItem
            || context == ItemSlot.Context.EquipArmor
            || context == ItemSlot.Context.EquipArmorVanity
            || context == ItemSlot.Context.EquipAccessory
            || context == ItemSlot.Context.EquipAccessoryVanity
            || context == ItemSlot.Context.EquipPet
            || context == ItemSlot.Context.EquipDye
            || context == ItemSlot.Context.EquipMiscDye
            || context == ItemSlot.Context.EquipLight
            || context == ItemSlot.Context.EquipMount
            || context == ItemSlot.Context.EquipGrapple
            || context == ItemSlot.Context.EquipMinecart;

        if (!inventory || !item.TryGetGlobalItem(out InventoryGraphicsGlobalItem graphics)) {
            return orig(item, context, spriteBatch, screenPositionForItemCenter, scale, sizeLimit, environmentColor);
        }

        var rectangle = new Rectangle(
            (int)screenPositionForItemCenter.X - 20,
            (int)screenPositionForItemCenter.Y - 20,
            40,
            40
        );

        graphics.Hovering = rectangle.Contains(Main.MouseScreen.ToPoint());

        if (graphics.Hovering && !graphics.OldHovering) {
            SoundEngine.PlaySound(in InventoryTickSound);
        }

        graphics.OldHovering = graphics.Hovering;

        if (context == ItemSlot.Context.MouseItem) {
            var velocity = Main.MouseScreen - graphics.OldMouseScreen;
            var rotation = velocity.X * 0.01f;

            graphics.OldMouseScreen = Main.MouseScreen;

            graphics.Rotation = MathHelper.Lerp(graphics.Rotation, rotation, 0.1f);

            var targetScale = SELECTED_ITEM_SCALE + (velocity.Length() * 0.005f);

            graphics.Scale = MathHelper.Lerp(graphics.Scale, targetScale, 0.1f);
        }
        else {
            graphics.Scale = MathHelper.SmoothStep(graphics.Scale, graphics.Hovering ? SELECTED_ITEM_SCALE : UNSELECTED_ITEM_SCALE, 0.5f);

            scale = graphics.Scale;
        }

        graphics.Position = Vector2.SmoothStep(graphics.Position, screenPositionForItemCenter, 0.5f);

        screenPositionForItemCenter = graphics.Position;

        if (context != ItemSlot.Context.MouseItem) {
            return orig(item, context, spriteBatch, screenPositionForItemCenter, scale, sizeLimit, environmentColor);
        }

        Main.GetItemDrawFrame(item.type, out var texture, out var frame);

        spriteBatch.Draw(
            texture,
            screenPositionForItemCenter,
            frame,
            Color.White,
            graphics.Rotation,
            frame.Size() / 2f,
            graphics.Scale,
            SpriteEffects.None,
            0f
        );

        return scale;
    }
}
