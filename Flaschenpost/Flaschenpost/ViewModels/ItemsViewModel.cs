using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Flaschenpost
{
    internal class ItemsViewModel : BaseViewModel
    {
        public string ToolBarItemSortTitle => "Sort";
        public string ToolBarItemSortImgSrc => "ic_tb__sort.png";

        public bool IsRefreshing { get { return _isRefreshing; } set { SetProperty(ref _isRefreshing, value); } } // bindable trigger for RefreshView
        private bool _isRefreshing = false;

        private bool _dataLoadedOnce = false;
        private SortId _sortId;

        public ICommand OpenSelectSortPageCommand { get; }
        public ICommand LoadItemsCommand { get; }
        public Command<Item> ItemTappedCommand { get; }

        public ObservableCollection<Item> Items { get; } = new();

        private readonly IPageManager _pageManager;
        private readonly IDataManager _dataManager;
        private readonly IPrefManager _prefManager;

        public ItemsViewModel(IPageManager pageManager, IDataManager dataManager, IPrefManager prefManager)
        {
            _pageManager = pageManager;
            _dataManager = dataManager;
            _prefManager = prefManager;

            OpenSelectSortPageCommand = new Command(async () => await OpenSelectSortPage());
            ItemTappedCommand = new Command<Item>(async (item) => await OnItemTapped(item));
            LoadItemsCommand = new Command(async () => await LoadItems());
        }

        internal async Task OnAppearingAsync()
        {
            var sortId = _prefManager.GetSortId();
            if (_dataLoadedOnce && Items.Count > 0 && _sortId == sortId)
                return;

            _sortId = sortId;

            await LoadItems();
        }

        private async Task OpenSelectSortPage()
        {
            await _pageManager.PushPageAsync(new SelectSortPage());
        }

        private async Task LoadItems()
        {
            try
            {
                Items.Clear();
                var items = await _dataManager.GetItemAsync(_sortId);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            _dataLoadedOnce = true;
            IsRefreshing = false;
        }

        private async Task OnItemTapped(Item item)
        {
            if (item == null)
                return;

            await _pageManager.PushPageAsync(new ItemDetailPage(item));
        }
    }
}
