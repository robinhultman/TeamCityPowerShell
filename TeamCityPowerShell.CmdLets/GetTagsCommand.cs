using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Xml;

namespace TeamCityPowerShell.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "BuildTags")]
    public class GetTagsCommand : Cmdlet
    {
        [Parameter(Mandatory = false)]
        public string BuildConfiguration { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(BuildConfiguration))
            {
                BuildConfiguration = ApiHelper.Instance.SelectedBuildConfiguration;
            }

            var response = ApiHelper.Instance.HttpClient.GetAsync(new Uri(ApiHelper.Instance.BuildTypesUri, BuildConfiguration+ "/builds")).Result;
            response.EnsureSuccessStatusCode();

            var xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(response.Content.ReadAsStringAsync().Result);

            var xmlBuilds = xmlResponse.SelectNodes("/builds[build/@status = 'SUCCESS']/build");
            var tags = new HashSet<string>();
            foreach (XmlNode build in xmlBuilds)
            {
                response = ApiHelper.Instance.HttpClient.GetAsync(new Uri(ApiHelper.Instance.ApiUri, build.Attributes["href"].Value)).Result;
                var buildDoc = new XmlDocument();
                buildDoc.LoadXml(response.Content.ReadAsStringAsync().Result);
                
                var tagsDoc = buildDoc.SelectNodes("/build/tags/tag");

                foreach (XmlNode tag in tagsDoc)
                {
                    if (!tags.Contains(tag.InnerText))
                    {
                        tags.Add(tag.InnerText);
                    }
                }
            }

            foreach (var tag in tags)
            {
                WriteObject(tag);
            }
        }
    }
}