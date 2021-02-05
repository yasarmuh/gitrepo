using MYMA.GrpcDataStore.Service;
using MYMA.Views;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MYMA.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Student _selectedStudent;

        public ObservableCollection<Student> Students { get; }
        public ICommand LoadItemsCommand { get; }
        public ICommand AddItemCommand { get; }
        public ReactiveCommand<Student, Unit> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Students = new ObservableCollection<Student>();
            LoadItemsCommand = ReactiveCommand.Create(async () => await ExecuteLoadItemsCommand());

            ItemTapped = ReactiveCommand.CreateFromTask<Student>(OnStudentSelected());

            AddItemCommand = ReactiveCommand.Create(async () => await OnAddStudent());
        }

        private Func<Student, Task> OnStudentSelected()
        {
            return async (student) =>
            {
                if (student == null)
                    return;

                // This will push the ItemDetailPage onto the navigation stack
                await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={student.Id}");
            };
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Students.Clear();
                var items = await DataStore.GetItemsAsync();
                foreach (var item in items)
                {
                    Students.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedStudent = null;
        }
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedStudent, value);
                //OnStudentSelected(value);
            }
        }

        private async Task OnAddStudent()
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }


    }
}