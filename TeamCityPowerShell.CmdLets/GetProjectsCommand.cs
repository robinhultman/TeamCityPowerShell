using System;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace TeamCityPowerShell.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "Projects")]
    public class GetProjectsCommand : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var client = ApiHelper.Instance.HttpClient;
            var response = client.GetAsync(ApiHelper.Instance.ProjectsUri).Result;

            response.EnsureSuccessStatusCode();

            var xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(response.Content.ReadAsStringAsync().Result);

            var xmlApps = xmlResponse.SelectNodes("/projects/project");

            if (xmlApps == null)
            {
                WriteObject("No projects found");
                return;
            }

            foreach (XmlNode node in xmlApps)
            {
                WriteObject(node.Attributes["name"].InnerText);
            }
        }
    }
}
