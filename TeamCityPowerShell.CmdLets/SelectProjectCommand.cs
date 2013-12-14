using System.Management.Automation;

namespace TeamCityPowerShell.CmdLets
{
    [Cmdlet(VerbsCommon.Select, "Project")]
    public class SelectProjectCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("N")]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            ApiHelper.Instance.SelectedProject = Name;
        }
    }
}