using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListViewScrolling
{
    public class ViewModel : BaseViewModel
    {
        List<string> allString = new List<string>();

        public ICommand LoadCommand { get; private set; }
        public ViewModel()
        {
            this.LoadCommand = new Command<object>(OnLoadCommand);
            
            for (int i = 0; i < 120; i++)
            {
                allString.Add("Name" + i);
            }
        }

        private ObservableCollection<string> _data = new ObservableCollection<string>();
        public ObservableCollection<string> Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        private bool _isProcessing;
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set { SetProperty(ref _isProcessing, value); }
        }

        private int _pageIndex;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { SetProperty(ref _pageIndex, value); }
        }

        private void OnLoadCommand(object parameter)
        {
            IsProcessing = true;
            if (parameter == null)
            {
                Data.Clear();
                PageIndex = 1;
                parameter = PageIndex;
            }
                var items = GetPage(Convert.ToInt32(parameter));
            foreach (var item in items)
            {
                Data.Add(item);
            }
            IsProcessing = false;
        }
        private List<string> GetPage(int pageIndex= 1)
        {
            int pagesize = 20;
            return allString.Skip<string>((pageIndex - 1) * pagesize).Take(pagesize).ToList();
        }
    } 
}
