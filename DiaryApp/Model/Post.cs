using System;
using System.Runtime.Serialization;

namespace DiaryApp.Model
{
    [DataContract]
    internal class Post
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime TimeAdded { get; set; }

        public Post() : this(0, " ", " ", DateTime.Now)
        {
        }

        public Post(int id, string title, string text, DateTime timeAdded)
        {
            Id = id;
            Title = title;
            Text = text;
            TimeAdded = timeAdded;
        }
    }
}
