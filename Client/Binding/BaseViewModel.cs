using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.Binding
{
    public class BaseViewModel : BaseINotifyPropertyChanged
    {
        /// <summary>
        /// Model Property 변경 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        { }

        public virtual void CreateEventHandler() { }
    }
}
