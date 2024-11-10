using EndlessEscapade.Core.Configuration;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace EndlessEscapade.Core.Graphics;

[Autoload(Side = ModSide.Client)]
public sealed class InventoryGraphicsGlobalItem : GlobalItem
{
    private const float SELECTED_ITEM_SCALE = 1f;
    private const float UNSELECTED_ITEM_SCALE = 0.8f;

    public static readonly SoundStyle InventoryTickSound = new($"{nameof(EndlessEscapade)}/Assets/Sounds/Custom/InventoryTick") {
        Volume = 0.5f,
        PitchVariance = 0.2f
    };

    private float inventoryDrawScale;
    private float inventoryDrawRotation;

    private Vector2 inventoryDrawPosition;

    // TODO: Maybe store this somewhere else for general use? Same for Main.MouseWorld.
    private Vector2 oldMouseScreen;

    public override bool InstancePerEntity { get; } = true;

    public override void Load() {
        base.Load();

        On_ItemSlot.DrawItemIcon += ItemSlot_DrawItemIcon_Hook;
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
        // TODO: Find a way to make this configurable.
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

        if (!inventory
            || !item.TryGetGlobalItem(out InventoryGraphicsGlobalItem graphics)
            || !ClientConfiguration.Instance.EnableInventoryItemEffects) {
            return orig(item, context, spriteBatch, screenPositionForItemCenter, scale, sizeLimit, environmentColor);
        }

        var rectangle = new Rectangle(
            (int)screenPositionForItemCenter.X - 20,
            (int)screenPositionForItemCenter.Y - 20,
            40,
            40
        );

        var hovering = rectangle.Contains(Main.MouseScreen.ToPoint());

        if (context == ItemSlot.Context.MouseItem) {
            var velocity = Main.MouseScreen - graphics.oldMouseScreen;
            var rotation = velocity.X * 0.01f;

            graphics.oldMouseScreen = Main.MouseScreen;

            graphics.inventoryDrawRotation = MathHelper.Lerp(graphics.inventoryDrawRotation, rotation, 0.1f);

            var targetScale = SELECTED_ITEM_SCALE + (velocity.Length() * 0.005f);

            graphics.inventoryDrawScale = MathHelper.Lerp(graphics.inventoryDrawScale, targetScale, 0.1f);
        }
        else {
            graphics.inventoryDrawScale = MathHelper.SmoothStep(graphics.inventoryDrawScale, hovering ? SELECTED_ITEM_SCALE : UNSELECTED_ITEM_SCALE, 0.5f);

            scale = graphics.inventoryDrawScale;
        }

        graphics.inventoryDrawPosition = Vector2.SmoothStep(graphics.inventoryDrawPosition, screenPositionForItemCenter, 0.5f);

        screenPositionForItemCenter = graphics.inventoryDrawPosition;

        if (context != ItemSlot.Context.MouseItem) {
            return orig(item, context, spriteBatch, screenPositionForItemCenter, scale, sizeLimit, environmentColor);
        }

        Main.GetItemDrawFrame(item.type, out var texture, out var frame);

        spriteBatch.Draw(
            texture,
            screenPositionForItemCenter,
            frame,
            environmentColor,
            graphics.inventoryDrawRotation,
            frame.Size() / 2f,
            graphics.inventoryDrawScale,
            SpriteEffects.None,
            0f
        );

        return scale;
    }
}
