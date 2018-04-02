using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CoffeeMaker_Client.UIHelper
{
    public class ListBoxHelper
    {
        public static INotifyCollectionChanged GetSelectedItems(ListBox obj)
        {
            return (INotifyCollectionChanged)obj.GetValue(SelectedItemsProperty);
        }
        public static void SetSelectedItems(ListBox obj, object value)
        {
            obj.SetValue(SelectedItemsProperty, (INotifyCollectionChanged)value);
        }
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(INotifyCollectionChanged),
            typeof(ListBoxHelper), new UIPropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemsChanged)));

        public static void OnSelectedItemsChanged(DependencyObject Sender, DependencyPropertyChangedEventArgs e)
        {
            if (Sender is ListBox == false) return;

            ListBox Target = Sender as ListBox;
            SelectedItemsHelper Helper = SelectedItemsHelper.GetSelectedItemsHelper(Target);

            if (e.NewValue == null && Helper != null)
            {
                Helper.StopListener(Helper.SelectedItemsSource);
                Helper.StopListener(Helper.SelectedItemsTarget);
            }
            else if (e.NewValue != null && Helper == null)
            {
                Helper = new SelectedItemsHelper(Target.SelectedItems as IList, e.NewValue as IList);
                Target.SetValue(SelectedItemsHelper.SelectedItemsHelperProperty, Helper);

                Helper.StartListener(Target.SelectedItems as IList);
                Helper.StartListener(e.NewValue as IList);
            }
        }
    }
}
