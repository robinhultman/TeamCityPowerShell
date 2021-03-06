﻿using System;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TeamCityPowerShell.CmdLets
{
    [Cmdlet(VerbsCommon.Select, "BuildServer")]
    public class SelectBuildServerCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("AU")]
        public string ApiUri { get; set; }
        
        [Parameter(Mandatory = true)]
        [Alias("User", "U")]
        public string ApiUserName { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("Pwd", "P")]
        public string ApiPassword { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("DP", "Path")]
        public string LocalDeployPath { get; set; }
        

        protected override void ProcessRecord()
        {
            var clientHandler = new HttpClientHandler { Credentials = new NetworkCredential(ApiUserName, ApiPassword) };
            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization",
              "Basic " +
              Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", ApiUserName, ApiPassword))));

            ApiHelper.Instance.HttpClient = httpClient;
            ApiHelper.Instance.ApiUri = new Uri(ApiUri);
            ApiHelper.Instance.DeployPath = LocalDeployPath;
        }
    }
}