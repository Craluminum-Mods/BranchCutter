using Vintagestory.API.Common;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

[assembly: ModInfo("Branch Cutter",
    Authors = new[] { "Craluminum2413" })]

namespace BranchCutter
{
    class Core : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            base.Start(api);

            api.RegisterBlockBehaviorClass("BranchCutter_DropLeaves", typeof(BlockBehaviorDropLeaves));
            api.RegisterCollectibleBehaviorClass("BranchCutter_DropLeaves", typeof(CollectibleBehaviorDropLeaves));
            api.World.Logger.Event("started 'Branch Cutter' mod");
        }

        public override void AssetsFinalize(ICoreAPI api)
        {
            foreach (var item in api.World.Items)
            {
                if (item is not ItemShears) continue;

                item.CollectibleBehaviors = item.CollectibleBehaviors.Append(new CollectibleBehaviorDropLeaves(item));
            }

            foreach (var block in api.World.Blocks)
            {
                if (block.BlockMaterial != EnumBlockMaterial.Leaves) continue;

                block.BlockBehaviors = block.BlockBehaviors.Append(new BlockBehaviorDropLeaves(block));
            }
        }
    }
}