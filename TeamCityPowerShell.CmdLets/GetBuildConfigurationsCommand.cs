using System;
using System.Management.Automation;
using System.Xml;

namespace TeamCityPowerShell.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "BuildTypes")]
    public class GetBuildConfigurationsCommand : Cmdlet
    {
        [Parameter(Mandatory = false)]
        public string Project
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(Project))
            {
                Project = ApiHelper.Instance.SelectedProject;
            }

            var response = ApiHelper.Instance.HttpClient.GetAsync(new Uri(ApiHelper.Instance.ProjectsUri,Project)).Result;
            response.EnsureSuccessStatusCode();

            var xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(response.Content.ReadAsStringAsync().Result);

            var xmlApps = xmlResponse.SelectNodes("/project[@id = '" + Project + "']/buildTypes/buildType");

            if (xmlApps == null || xmlApps.Count == 0)
            {
                WriteObject("No build configurations found");
                return;
            }

            foreach (XmlNode node in xmlApps)
            {
                WriteObject(node.Attributes["id"].Value);
            }
        }
    }
}
