using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListViewScrolling
{
    public class ListViewBehavior : Behavior<ListView>
    {
        public ListView CurrentListView { get; private set; }


        public static readonly BindableProperty PageIndexProperty =
            BindableProperty.Create("PageIndex", typeof(int), typeof(ListViewBehavior),
                defaultValue: 1, defaultBindingMode: BindingMode.TwoWay);
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ListViewBehavior),
                defaultValue: default(ICommand),
                defaultBindingMode: BindingMode.TwoWay);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            CurrentListView = bindable;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
            bindable.ItemAppearing += Bindable_ItemAppearing;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        private void Bindable_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.IsRefreshing)
                return;

            if (Command == null)
                return;

            var items = listView.ItemsSource as IList;
            if (items != null && e.Item == items[items.Count - 1])
            {
                PageIndex = PageIndex + 1;
                Command.Execute(PageIndex);
            }
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);

            CurrentListView = null;
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            bindable.ItemAppearing -= Bindable_ItemAppearing;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = CurrentListView.BindingContext;
        }
    }
}
