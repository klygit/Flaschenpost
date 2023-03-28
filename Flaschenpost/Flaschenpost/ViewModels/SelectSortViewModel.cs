using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Flaschenpost
{
    internal class SelectSortViewModel : BaseViewModel
    {
        public ObservableCollection<SelectSortData> SortDatas { get; } = new();

        public Command<SelectSortData> SortDataTappedCommand { get; }

        private readonly IPageManager _pageManager;
        private readonly IPrefManager _prefManager;

        public SelectSortViewModel(IPageManager pageManager, IPrefManager prefManager)
        {
            _pageManager = pageManager;
            _prefManager = prefManager;

            SortDatas.Add(new SelectSortData(SortId.PriceAsc));
            SortDatas.Add(new SelectSortData(SortId.PriceDesc));

            SortDatas.Add(new SelectSortData(SortId.PricePerBottleAsc));
            SortDatas.Add(new SelectSortData(SortId.PricePerBottleDesc));

            SortDatas.Add(new SelectSortData(SortId.PricePerUnitAsc));
            SortDatas.Add(new SelectSortData(SortId.PricePerUnitDesc));

            SortDatas.Add(new SelectSortData(SortId.CountBottleAsc));
            SortDatas.Add(new SelectSortData(SortId.CountBottleDesc));

            SortDataTappedCommand = new Command<SelectSortData>(async (sortData) => await OnSortDataSelected(sortData));

            UpdateSelectedState(_prefManager.GetSortId());
        }

        private async Task OnSortDataSelected(SelectSortData sortData)
        {
            if (sortData == null)
                return;

            _prefManager.SetSortId(sortData.Id);
            UpdateSelectedState(sortData.Id);
            await _pageManager.PopPageAsync();
            Device.BeginInvokeOnMainThread(async () => await _pageManager.DisplayToastAsync(sortData.DisplayText));
        }

        private void UpdateSelectedState(SortId sortId)
        {
            foreach (var sortData in SortDatas)
            {
                sortData.IsSelected = sortData.Id == sortId;
            }
        }
    }

    public class SelectSortData : INotifyPropertyChanged
    {
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;

        internal SortId Id { get; private set; }
        public string DisplayText { get; private set; }

        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected == value)
                    return;

                isSelected = value;
                OnPropertyChanged();
            }
        }

        internal SelectSortData(SortId id)
        {
            Id = id;

            DisplayText = id switch
            {
                SortId.PriceAsc => "Preis aufsteigend",
                SortId.PriceDesc => "Preis absteigend",
                SortId.PricePerBottleAsc => "Preis pro Flasche aufsteigend",
                SortId.PricePerBottleDesc => "Preis pro Flasche absteigend",
                SortId.PricePerUnitAsc => "Preis pro Liter aufsteigend",
                SortId.PricePerUnitDesc => "Preis pro Liter absteigend",
                SortId.CountBottleAsc => "Anzahl Flaschen im Gebinde aufsteigend",
                SortId.CountBottleDesc => "Anzahl Flaschen im Gebinde absteigend",
                _ => "N/A",
            };
        }
    }
}
