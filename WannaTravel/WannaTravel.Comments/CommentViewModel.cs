using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WannaTravel.BusinessLogic.Entities;
using WannaTravel.BusinessLogic.Interfaces;

namespace WannaTravel.Comments
{
    public class CommentViewModel : INotifyPropertyChanged
    {
        private readonly ICommentCollection _commentCollection;
        private ObservableCollection<CommentEntity> commentEntities;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CommentEntity> Comments
        {
            get { return commentEntities; }
            set { commentEntities = value; OnPropertyChanged("Comments"); }
        }

        public CommentViewModel(IEnumerable<CommentEntity> comments)
        {
            Comments = new ObservableCollection<CommentEntity>(comments);
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
