using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UEditorNetCore.Handlers;

namespace UEditorNetCore
{
    public class UEditorActionCollection:Dictionary<string,Action<HttpContext>>
    {
        public UEditorActionCollection()
        {
            Add("config", ConfigAction);
            Add("uploadimage", UploadImageAction);
            Add("uploadscrawl", UploadScrawlAction);
            Add("uploadvideo", UploadVideoAction);
            Add("uploadfile", UploadFileAction);
            Add("listimage", ListImageAction);
            Add("listfile", ListFileAction);
            Add("catchimage", CatchImageAction);
        }

        public new UEditorActionCollection Add(string action, Action<HttpContext> handler)
        {
            if (ContainsKey(action))
                this[action] = handler;
            else
                base.Add(action, handler);

            return this;
        }

        public new UEditorActionCollection Remove(string action)
        {
            base.Remove(action);
            return this;
        }

        private void ConfigAction(HttpContext context)
        {
            new ConfigHandler(context).Process();
        }

        private void UploadImageAction(HttpContext context)
        {
            new UploadHandler(context, new UploadConfig()
            {
                AllowExtensions = Config.GetStringList("imageAllowFiles"),
                PathFormat = Config.GetString("imagePathFormat"),
                SizeLimit = Config.GetInt("imageMaxSize"),
                UploadFieldName = Config.GetString("imageFieldName")
            }).Process();
        }

        private void UploadScrawlAction(HttpContext context)
        {
            new UploadHandler(context, new UploadConfig()
            {
                AllowExtensions = new string[] { ".png" },
                PathFormat = Config.GetString("scrawlPathFormat"),
                SizeLimit = Config.GetInt("scrawlMaxSize"),
                UploadFieldName = Config.GetString("scrawlFieldName"),
                Base64 = true,
                Base64Filename = "scrawl.png"
            }).Process();
        }

        private void UploadVideoAction(HttpContext context)
        {
            new UploadHandler(context, new UploadConfig()
            {
                AllowExtensions = Config.GetStringList("videoAllowFiles"),
                PathFormat = Config.GetString("videoPathFormat"),
                SizeLimit = Config.GetInt("videoMaxSize"),
                UploadFieldName = Config.GetString("videoFieldName")
            }).Process();
        }

        private void UploadFileAction(HttpContext context)
        {
            new UploadHandler(context, new UploadConfig()
            {
                AllowExtensions = Config.GetStringList("fileAllowFiles"),
                PathFormat = Config.GetString("filePathFormat"),
                SizeLimit = Config.GetInt("fileMaxSize"),
                UploadFieldName = Config.GetString("fileFieldName")
            }).Process();
        }

        private void ListImageAction(HttpContext context)
        {
            new ListFileManager(
                    context,
                    Config.GetString("imageManagerListPath"),
                    Config.GetStringList("imageManagerAllowFiles"))
                .Process();
        }

        private void ListFileAction(HttpContext context)
        {
            new ListFileManager(
                    context,
                    Config.GetString("fileManagerListPath"),
                    Config.GetStringList("fileManagerAllowFiles"))
                .Process();
        }

        private void CatchImageAction(HttpContext context)
        {
            new CrawlerHandler(context).Process();
        }
    }
}
