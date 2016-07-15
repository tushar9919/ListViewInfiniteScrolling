using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ListViewScrolling
{
    public partial class Mainpage : ContentPage
    {
        ViewModel _vm;
        public Mainpage()
        {
            InitializeComponent();

            this._vm = new ViewModel();
            this.BindingContext = _vm;
            this.Appearing += Mainpage_Appearing;
        }

        private void Mainpage_Appearing(object sender, EventArgs e)
        {
            _vm.LoadCommand.Execute(null);
        }
    }
}
