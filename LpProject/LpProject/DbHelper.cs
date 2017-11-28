// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbHelper.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   数据库访问类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpProject
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
    /// 数据库访问类
    /// </summary>
    public static class DbHelper
    {
        private static readonly string projectsName = "projects.json";

        /// <summary>
        /// 项目
        /// </summary>
        public static List<Project> Projects { get; set; }

        public static Project CurrentProject { get; set; }

        /// <summary>
        /// 保存数据库
        /// </summary>
        public static void Save()
        {
            SaveFile(projectsName, Projects);
        }

        /// <summary>
        /// 加载数据库
        /// </summary>
        public static void Load()
        {
            Projects = LoadFile<List<Project>>(projectsName);
            if (Projects == null)
            {
                Projects = new List<Project>();
                SaveFile(projectsName, Projects);
            }

        }

        /// <summary>
        /// 加载数据库
        /// </summary>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private static T LoadFile<T>(string fileName)
            where T : class
        {
            if (File.Exists(fileName))
            {
                using (FileStream stream = File.Open(
                    fileName,
                    System.IO.FileMode.Open,
                    FileAccess.ReadWrite,
                    FileShare.ReadWrite))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    string json = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    var result = JsonConvert.DeserializeObject<T>(json);
                    stream.Close();
                    return result;
                }
            }
            return default(T);
        }

        /// <summary>
        /// 保存数据库
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        private static void SaveFile<T>(string fileName, T result)
            where T : class
        {
            using (FileStream stream = File.Open(fileName, System.IO.FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                string json = JsonConvert.SerializeObject(result);
                byte[] bytes = UTF8Encoding.UTF8.GetBytes(json);
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
        }
    }
}