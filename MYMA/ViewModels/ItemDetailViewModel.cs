using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MYMA.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;

        [Reactive]
        public string Id { get; set; }

        [Reactive]
        public string Text { get; set; }

        [Reactive]
        public string Description { get; set; }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string studentId)
        {
            try
            {
                var student = await DataStore.GetItemAsync(studentId);
                Id = student.DateofBirth.ToDateTime().ToString("d");
                Text = student.FirstName + student.MiddleName + student.LastName;
                Description = student.AdmisstionDate.ToDateTime().ToString("d");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
