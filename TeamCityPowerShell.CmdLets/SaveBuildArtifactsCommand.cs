﻿using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace TeamCityPowerShell.CmdLets
{
    [Cmdlet(VerbsData.Save, "BuildArtifacts")]
    public class SaveBuildArtifactsCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string BuildConfiguration
        {
            get;
            set;
        }

        [Parameter(Mandatory = true)]
        public string Tag
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        public bool Unzip
        {
            get;
            set;
        }


        protected override void ProcessRecord()
        {
           
            var response = ApiHelper.Instance.HttpClient.GetAsync(ApiHelper.Instance.GetSaveArtifactUri(BuildConfiguration, Tag)).Result;

            response.EnsureSuccessStatusCode();

            IEnumerable<string> values;
            var fileName = string.Empty;

            if (response.Content.Headers.TryGetValues("Content-Disposition", out values))
            {
                fileName = values.FirstOrDefault().Split('\'')[2].Replace(";", string.Empty);
            }


            ApiHelper.Instance.SaveArtifacts(response.Content.ReadAsByteArrayAsync().Result, BuildConfiguration, Tag, fileName, Unzip);

            WriteObject("Saved");
        }
    }
}