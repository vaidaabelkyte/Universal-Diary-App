using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Windows.Storage;
using DiaryApp.Model;

namespace DiaryApp.Data
{
    internal class PostsProvider
    {
        private readonly string _postsPath = ApplicationData.Current.RoamingFolder.Path + @"\posts.json";
        private readonly DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(List<Post>));

        public List<Post> Posts { get; }

        public PostsProvider()
        {
            if (File.Exists(_postsPath))
            {
                using (var fileStream = new FileStream(_postsPath, FileMode.Open))
                {
                    try
                    {
                        Posts = (List<Post>)_serializer.ReadObject(fileStream);

                        // If loaded successfully
                        return;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            // If can't load the file, create empty list
            Posts = new List<Post>();
            Posts.Add(new Post(1, "Hello!)", "Thank You For choosing DiaryApp!", DateTime.Now));
        }

        public void StorePosts()
        {

            using (var fileStream = new FileStream(_postsPath, FileMode.Create))
            {
                _serializer.WriteObject(fileStream, Posts);
                fileStream.Flush(true);
            }
        }
    }
}
