using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace TeamCityPowerShell.CmdLets
{
    public class ApiHelper
    {
        private static ApiHelper _instance;

        private ApiHelper()
        {
           
        }

        public static ApiHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiHelper();
                }

                return _instance;
            }
        }
        
        public HttpClient HttpClient { get; set; }
        public Uri ApiUri { get; set; }

        public Uri ProjectsUri { get { return new Uri(ApiUri, "httpAuth/app/rest/projects/"); } }
        public Uri BuildTypesUri { get { return new Uri(ApiUri, "httpAuth/app/rest/buildTypes/"); } }
        public Uri ArtifactUri { get { return new Uri(ApiUri, "repository/downloadAll/"); } }
        public string SelectedProject { get; set; }
        
        public string DeployPath { get;set; }
        public HashSet<string> Projects = new HashSet<string>(); 

        public Uri GetSaveArtifactUri(string buildConfiguration, string tag)
        {
            return new Uri(ArtifactUri + buildConfiguration + "/" + tag +
                           ".tcbuildtag/.lastPinned");
        }

        public void SaveArtifacts(byte[] artifacts, string buildConfiguration, string tag, string fileName, bool unzip)
        {
            var di = Directory.CreateDirectory(Path.Combine(DeployPath, buildConfiguration, tag));

            File.WriteAllBytes(Path.Combine(di.FullName, fileName), artifacts);

            if (unzip)
            {
                ExtractZipFile(Path.Combine(di.FullName, fileName), di.FullName.ToString());
                File.Delete(Path.Combine(di.FullName, fileName));
            }
        }


        public void ExtractZipFile(string archiveFilenameIn, string outFolder)
        {
            ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ZipFile(fs);
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;
                    }
                    string entryFileName = zipEntry.Name;
                    byte[] buffer = new byte[4096];
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    string fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true;
                    zf.Close();
                }
            }
        }
    }
}
