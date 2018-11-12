using FluentFTP;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;

namespace UEditorNetCore
{
    public class FtpUpload
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file">上传文件</param>
        /// <param name="saveFilePath">保存完整路径/param>
        /// <param name="ip">保存服务器IP</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param> 
        /// <returns></returns>
        public static bool UploadFile(IFormFile file, string saveFilePath, string ip, string username, string pwd)
        {
            var result = UploadFile(file.OpenReadStream(), saveFilePath, ip, username, pwd);
            return result;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="saveFilePath">保存完整路径</param>
        /// <param name="ip">保存服务器IP</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static bool UploadFile(Stream fileStream, string saveFilePath, string ip, string username, string pwd)
        {
            var result = false;
            var client = new FtpClient(ip)
            {
                Credentials = new NetworkCredential(username, pwd)
            };
            try
            {
                client.Connect();
                client.Upload(fileStream, saveFilePath, createRemoteDir: true);
                if (client.FileExists(saveFilePath))
                {
                    result = true; 
                }
                client.Disconnect();
            }
            catch (FtpException ex)
            {
                client.Disconnect();
                //
            }
            return result;
        }
    }
}
