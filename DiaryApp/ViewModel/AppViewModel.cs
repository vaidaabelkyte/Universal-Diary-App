using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DiaryApp.Data;
using DiaryApp.Model;

namespace DiaryApp.ViewModel
{
    internal class AppViewModel : NotificationBase
    {
        private static AppViewModel _instance;

        public static AppViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppViewModel();
                }

                return _instance;
            }
        }

        private PostsProvider _postsProvider;

        private bool _isMenuOpened;

        public bool IsMenuOpened
        {
            get { return _isMenuOpened; }
            set { SetProperty(ref _isMenuOpened, value); }
        }

        public ObservableCollection<PostViewModel> Posts { get; }

        public List<PostViewModel> SortedPosts
        {
            get { return Posts.OrderByDescending(post => post.TimeAdded).ToList(); }
        }

        private AppViewModel()
        {
            _postsProvider = new PostsProvider();
            Posts = new ObservableCollection<PostViewModel>();
            foreach (var post in _postsProvider.Posts.ToArray())
            {
                Posts.Add(new PostViewModel(post));
            }
            Posts.CollectionChanged += PostsOnCollectionChanged;
        }

        private void PostsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.OldItems != null)
            {
                foreach (PostViewModel oldItem in notifyCollectionChangedEventArgs.OldItems)
                {
                    _postsProvider.Posts.RemoveAll(post => post.Id == oldItem.Id);
                }
            }

            if (notifyCollectionChangedEventArgs.NewItems != null)
            {
                foreach (PostViewModel newItem in notifyCollectionChangedEventArgs.NewItems)
                {
                    _postsProvider.Posts.Add(new Post(newItem.Id, newItem.Title, newItem.Text, newItem.TimeAdded));
                }
            }

            RaisePropertyChanged("SortedPosts");
            _postsProvider.StorePosts();
        }

        public void AddOrUpdatePost(PostViewModel postVm)
        {
            if (postVm.Id == 0)
            {
                if (Posts.Count == 0)
                {
                    postVm.Id = 1;
                }
                else
                {
                    postVm.Id = Posts.Max(vm => vm.Id) + 1;
                }
                Posts.Add(postVm);
            }
            else
            {
                PostViewModel originalViewModel = Posts.First(vm => vm.Id == postVm.Id);
                if (originalViewModel != null)
                {
                    originalViewModel.Text = postVm.Text;
                    originalViewModel.Title = postVm.Title;
                    originalViewModel.TimeAdded = postVm.TimeAdded;
                }

                RaisePropertyChanged("SortedPosts");
                _postsProvider.StorePosts();
            }
        }

        public void RemovePost(PostViewModel postVm)
        {
            Posts.Remove(postVm);
        }
    }
}
