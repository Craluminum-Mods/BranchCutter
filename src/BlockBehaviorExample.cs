using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace BranchCutter;

public class BlockBehaviorDropLeaves : BlockBehavior
{
    public BlockBehaviorDropLeaves(Block block) : base(block) { }

    public override ItemStack[] GetDrops(IWorldAccessor world, BlockPos pos, IPlayer byPlayer, ref float dropChanceMultiplier, ref EnumHandling handling)
    {
        handling = EnumHandling.PreventDefault;

        var slot = byPlayer?.InventoryManager?.ActiveHotbarSlot;
        var toolMode = slot?.Itemstack?.Collectible?.GetToolMode(slot, byPlayer, byPlayer?.CurrentBlockSelection);

        if (IsShears(slot) && toolMode == 1 && block.BlockMaterial == EnumBlockMaterial.Leaves)
        {
            return new[] { new ItemStack(block) };
        }

        return base.GetDrops(world, pos, byPlayer, ref dropChanceMultiplier, ref handling);
    }

    bool IsShears(ItemSlot slot) => slot?.Itemstack?.Collectible is ItemShears;
}