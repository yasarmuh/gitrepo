using MYMA.DataStore.Client.Services;
using MYMA.GrpcDataStore.Service;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace MYMA.ViewModels
{
    public class BaseViewModel : ReactiveObject
    {
        public IDataStore<Models.Student> DataStore => DependencyService.Get<IDataStore<Models.Student>>();

        [Reactive]
        public bool IsBusy { get; set; }

        [Reactive]
        public string Title { get; set; }
    }
}
