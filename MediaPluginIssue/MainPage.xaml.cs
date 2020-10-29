using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaPluginIssue
{
    public partial class MainPage: ContentPage
    {
        PhotoListViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel=new PhotoListViewModel();
            this.BindingContext=viewModel;
        }
    }
}
