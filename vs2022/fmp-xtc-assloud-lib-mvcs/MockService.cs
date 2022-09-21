using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.Assloud.LIB.Proto;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net;

namespace XTC.FMP.MOD.Assloud.LIB.MVCS
{
    public class MockService
    {
        public static MockService Instance
        {
            get
            {
                if (null == instance_)
                    instance_ = new MockService();
                return instance_;
            }
        }

        public Logger? logger { get; set; }

        private Dictionary<string, ContentEntity>? contentsMap_ { get; set; }

        private List<string> mounted_ = new List<string>();

        private static MockService? instance_ { get; set; } = null;

        public Error MountDisk(string _dir)
        {
            logger?.Info("ready to mount disk: {0}", _dir);
            if(mounted_.Contains(_dir))
            {
                return Error.OK;
            }

            if (!Directory.Exists(_dir))
            {
                return Error.NewAccessErr("{0} not found", _dir);
            }

            var dotAssloud = Path.Combine(_dir, ".assloud");
            if (!File.Exists(dotAssloud))
            {
                return Error.NewAccessErr("{0} not found", dotAssloud);
            }

            contentsMap_ = new Dictionary<string, ContentEntity>();
            foreach (var bundle_dir in Directory.GetDirectories(_dir))
            {
                string bundle_name = Path.GetFileName(bundle_dir);
                foreach (var content_dir in Directory.GetDirectories(bundle_dir))
                {
                    string content_name = Path.GetFileName(content_dir);
                    if (content_name.StartsWith("_"))
                        continue;

                    string meta_file = Path.Combine(content_dir, "meta.json");
                    if (!File.Exists(meta_file))
                    {
                        logger?.Error("{0} not found", meta_file);
                        continue;
                    }

                    ContentEntity content = new ContentEntity();
                    //content.Path = _dir;
                    content.BundleUuid = bundle_name;
                    content.Name = content_name;
                    try
                    {
                        string json = File.ReadAllText(meta_file);
                        /*
                        var meta = JsonConvert.DeserializeObject<MetaEntity>(json);
                        content.Meta = meta;
                        if (null == content.Meta)
                            throw new NullReferenceException("meta is null");
                        contentsMap_[content.Meta.Uri] = content;
                        */
                    }
                    catch (System.Exception ex)
                    {
                        logger?.Error("parse {0} failed!", meta_file);
                        logger?.Exception(ex);
                    }
                }
            }
            mounted_.Add(_dir);
            logger?.Info("found {0} contents", contentsMap_.Count);
            return Error.OK;
        }

        public async Task<ContentListResponse> CallMatch(ContentMatchRequest _request)
        {
            logger?.Trace("mock Match ");
            var response = new ContentListResponse();
            response.Status = new Status();
            if (null != contentsMap_)
            {
                foreach (var pair in contentsMap_)
                {
                    foreach (var pattern in _request.Patterns)
                    {
                        if (Regex.IsMatch(pair.Key, pattern))
                            response.Contents.Add(pair.Value);
                    }
                }
                response.Total = response.Contents.Count;
            }
            else
            {
                response.Status.Code = HttpStatusCode.InternalServerError.GetHashCode();
                response.Status.Message = "contentsMap is null";
            }
            return await Task.FromResult(response);
        }
    }
}
