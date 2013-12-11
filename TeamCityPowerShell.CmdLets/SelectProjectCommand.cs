using System.Management.Automation;

namespace TeamCityPowerShell.CmdLets
{
    [Cmdlet(VerbsCommon.Select, "Project")]
    public class SelectProjectCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string Project { get; set; }

        protected override void ProcessRecord()
        {
            ApiHelper.Instance.SelectedProject = Project;
        }
    }
}