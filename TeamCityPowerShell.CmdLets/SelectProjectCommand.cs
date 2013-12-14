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
            if (ApiHelper.Instance.Projects.Contains(Name))
            {
                ApiHelper.Instance.SelectedProject = Name;
                WriteObject(string.Format("{0} is now selected", Name));
            }
            else
            {
                var gpc = new GetProjectsCommand();
                foreach (var test in gpc.Invoke())
                {
                    
                }
                if (ApiHelper.Instance.Projects.Contains(Name))
                {
                    ApiHelper.Instance.SelectedProject = Name;
                    WriteObject(string.Format("{0} is now selected", Name));
                }
                else
                {
                    WriteObject(string.Format("{0} does not exist", Name));
                }
            }
        }
    }
}