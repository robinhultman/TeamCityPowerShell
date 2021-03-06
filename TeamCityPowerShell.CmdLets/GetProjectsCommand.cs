﻿using System.Linq;
using System.Management.Automation;
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

            foreach (var name in from XmlNode node in xmlApps where node.Attributes != null select node.Attributes["name"].InnerText)
            {
                ApiHelper.Instance.Projects.Add(name);
                WriteObject(name);
            }
        }
    }
}
