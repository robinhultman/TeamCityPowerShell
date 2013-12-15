using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace TeamCityPowerShell.CmdLets
{
    [RunInstaller(true)]
    public class SnapinInstaller : CustomPSSnapIn
    {
        public override string Name
        {
            get { return "TeamCityPowerShell"; }
        }

        public override string Vendor
        {
            get { return "Robin Hultman"; }
        }

        public override string Description
        {
            get { return "TeamCityPowerShell is a set of commandlets to interface the TeamCity REST API."; }
        }

        private Collection<CmdletConfigurationEntry> _cmdlets;

        public override Collection<CmdletConfigurationEntry> Cmdlets
        {
            get
            {
                if (_cmdlets == null)
                {
                    _cmdlets = new Collection<CmdletConfigurationEntry>
                    {
                        new CmdletConfigurationEntry("Get-BuildConfigurations", typeof (GetBuildConfigurationsCommand),
                            "TeamCityPowerShell.CmdLets.dll-help.xml"),
                        new CmdletConfigurationEntry("Get-Projects", typeof (GetProjectsCommand),
                            "TeamCityPowerShell.CmdLets.dll-help.xml"),
                        new CmdletConfigurationEntry("Get-BuildTags", typeof (GetTagsCommand),
                            "TeamCityPowerShell.CmdLets.dll-help.xml"),
                        new CmdletConfigurationEntry("Save-BuildArtifacts", typeof (SaveBuildArtifactsCommand),
                            "TeamCityPowerShell.CmdLets.dll-help.xml"),
                        new CmdletConfigurationEntry("Select-BuildServer", typeof (SelectBuildServerCommand),
                            "TeamCityPowerShell.CmdLets.dll-help.xml"),
                        new CmdletConfigurationEntry("Select-Project", typeof (SelectProjectCommand),
                            "TeamCityPowerShell.CmdLets.dll-help.xml")
                    };
                }

                return _cmdlets;
            }
        }
    }
}