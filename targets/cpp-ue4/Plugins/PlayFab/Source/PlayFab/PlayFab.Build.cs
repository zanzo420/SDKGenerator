// #define PF_UNREAL_OLD_4_16_TO_4_17

using UnrealBuildTool;

public class PlayFab : ModuleRules
{
    public PlayFab(ReadOnlyTargetRules Target) : base(Target)
    {
        PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

        PublicIncludePaths.AddRange(new string[] {
            "PlayFab/Public"
        });

        PrivateIncludePaths.AddRange(new string[] {
            "PlayFab/Private"
        });

        PublicDependencyModuleNames.AddRange(new string[]{
            "Core",
            "CoreUObject",
            "HTTP",
            "Json"
        });

#if PF_UNREAL_OLD_4_16_TO_4_17
        if (UEBuildConfiguration.bBuildEditor == true)
#else
        if (Target.bBuildEditor == true)
#endif
        {
            PrivateDependencyModuleNames.AddRange(new string[] {
                "Settings"
            });
        }
    }
}
