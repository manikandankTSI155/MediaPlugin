using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MediaPluginIssue
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        protected bool Set<T>(ref T field,T value,[CallerMemberName] string propertyName = null)
        {
            if(!object.Equals(field,value))
            {
                field = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if(changed == null)
                return;

            changed.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
