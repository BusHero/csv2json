using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);
    
    [Solution(GenerateProjects = true, SuppressBuildProjectCheck = true)] readonly Solution Solution = null!;
    
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Restore => d => d
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    // ReSharper disable once UnusedMember.Local
    Target Clean => d => d
        .Before(Restore)
        .Executes(() =>
        {
            DotNetClean(s => s
                .SetProject(Solution));
        });

    Target Compile => d => d
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration));
        });
}
