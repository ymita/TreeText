using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TreeText.Infrastructure
{
    public class NotificationObject : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティ値の変更をクライアントに通知する。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChanged イベント を発生させる。
        /// </summary>
        /// <param name="propertyName">変更されたプロパティ名</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
