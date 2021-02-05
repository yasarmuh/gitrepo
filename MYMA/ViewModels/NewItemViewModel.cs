using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using ReactiveUI;
using MYMA.GrpcDataStore.Service;

namespace MYMA.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public NewItemViewModel()
        {
            SaveCommand = ReactiveCommand.Create(OnSave,
                this.WhenAnyValue(
                    x => x.Text, x => x.Description,
                    (text, description) => !String.IsNullOrWhiteSpace(text) && !String.IsNullOrWhiteSpace(description)
                    ));
            CancelCommand = ReactiveCommand.Create(OnCancel);
            //this.PropertyChanged +=
            //    (_, __) => SaveCommand.ChangeCanExecute();
        }
        [Reactive]
        public string Text { get; set; }

        [Reactive]
        public string Description { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Student newItem = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                AdmisstionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),
                DateofBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow - new TimeSpan(1500, 0, 0, 0, 0)),
                FirstName = "First",
                MiddleName = " ",
                LastName = "Last"
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
