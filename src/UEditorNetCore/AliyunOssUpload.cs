using Aliyun.OSS;
using System;
using System.IO;

namespace UEditorNetCore
{
    /// <summary>
    /// 阿里云 OSS 文件上传
    /// </summary>
    public class AliyunOssUpload
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="accessKeyId"></param>
        /// <param name="accessKeySecret"></param>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="fileExt"></param> 
        /// <param name="filePathToUpload"></param> 
        /// <returns>文件名称</returns>
        public static bool UploadFile(string endpoint, string accessKeyId, string accessKeySecret, string bucketName, string key, string fileExt, string filePathToUpload)
        {
            var result = true;
            try
            {
                fileExt = fileExt.TrimStart('.').ToLower();
                var contentType = GetContentType(fileExt);
                var metadata = new ObjectMetadata();
                if (!string.IsNullOrEmpty(contentType))
                {
                    metadata.ContentType = contentType;
                }

                //
                var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
                client.PutObject(bucketName, key, filePathToUpload, metadata);
                // 
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
         
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="accessKeyId"></param>
        /// <param name="accessKeySecret"></param>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="fileExt">文件扩展名称</param> 
        /// <param name="fileStream"></param>
        /// <returns>文件名称</returns>
        public static bool UploadFile(string endpoint, string accessKeyId, string accessKeySecret, string bucketName,string key, string fileExt, Stream fileStream)
        {
            var result = true;
            try
            {
                fileExt = fileExt.TrimStart('.').ToLower();
                var contentType = GetContentType(fileExt);
                var metadata = new ObjectMetadata();
                if (!string.IsNullOrEmpty(contentType))
                {
                    metadata.ContentType = contentType;
                }

                //
                var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
                //var 
                client.PutObject(bucketName, key, fileStream, metadata);
                // 
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
         
        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        private static string GetContentType(string fileExt)
        {
            var contentType = "";
            switch (fileExt)
            {
                case "jpeg":
                case "jpe":
                case "jpg":
                    contentType = "image/jpeg";
                    break;
                case "bmp":
                    contentType = "image/bmp";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                case "gif":
                    contentType = "image/gif";
                    break;
                case "ico":
                    contentType = "image/x-icon";
                    break;
                case "net":
                    contentType = "image/pnetvue";
                    break;
                case "tif":
                case "tiff":
                    contentType = "image/tiff";
                    break;
            }

            return contentType;
        }
    }
}
