using MYMA.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MYMA.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}