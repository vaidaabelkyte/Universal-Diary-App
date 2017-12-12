using System;
using DiaryApp.Model;

namespace DiaryApp.ViewModel
{
    class PostViewModel : NotificationBase<Post>
    {
        public int Id
        {
            get { return This.Id; }
            set { SetProperty(This.Id, value, () => This.Id = value); }
        }

        public String Title
        {
            get { return This.Title; }
            set { SetProperty(This.Title, value, () => This.Title = value); }
        }

        public String Text
        {
            get { return This.Text; }
            set { SetProperty(This.Text, value, () => This.Text = value); }
        }

        public DateTime TimeAdded
        {
            get { return This.TimeAdded; }
            set { SetProperty(This.TimeAdded, value, () => This.TimeAdded = value); }
        }

        public PostViewModel(Post post) : base(post)
        {

        }

        public PostViewModel(PostViewModel postVm = null)
        {
            if (postVm != null)
            {
                This.Id = postVm.Id;
                This.Title = postVm.Title;
                This.Text = postVm.Text;
                This.TimeAdded = postVm.TimeAdded;
            }
        }
    }
}
