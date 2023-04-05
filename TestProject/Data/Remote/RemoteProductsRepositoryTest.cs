using Flaschenpost;

namespace TestProject
{
    public class RemoteProductsRepositoryTest
    {
        private readonly RemoteProductsRepository _rep;

        public RemoteProductsRepositoryTest()
        {
            _rep = new RemoteProductsRepository(new DemoRemoteProductsSource());
        }

        [Fact]
        public async Task GetItemSorted_InputPriceAsc_ReturnTrue()
        {
            await AssertSortAsync(SortId.PriceAsc, (item, itemNext) => Assert.True(item.Price <= itemNext.Price));
        }

        [Fact]
        public async Task GetItemSorted_InputPriceDesc_ReturnTrue()
        {
            await AssertSortAsync(SortId.PriceDesc, (item, itemNext) => Assert.True(item.Price >= itemNext.Price));
        }

        [Fact]
        public async Task GetItemSorted_InputPricePerBottleAsc_ReturnTrue()
        {
            await AssertSortAsync(SortId.PricePerBottleAsc, (item, itemNext) => Assert.True(item.PricePerBottle <= itemNext.PricePerBottle));
        }

        [Fact]
        public async Task GetItemSorted_InputPricePerBottleDesc_ReturnTrue()
        {
            await AssertSortAsync(SortId.PricePerBottleDesc, (item, itemNext) => Assert.True(item.PricePerBottle >= itemNext.PricePerBottle));
        }

        [Fact]
        public async Task GetItemSorted_InputPricePerUnitAsc_ReturnTrue()
        {
            await AssertSortAsync(SortId.PricePerUnitAsc, (item, itemNext) => Assert.True(item.PricePerUnit <= itemNext.PricePerUnit));
        }

        [Fact]
        public async Task GetItemSorted_InputPricePerUnitDesc_ReturnTrue()
        {
            await AssertSortAsync(SortId.PricePerUnitDesc, (item, itemNext) => Assert.True(item.PricePerUnit >= itemNext.PricePerUnit));
        }

        [Fact]
        public async Task GetItemSorted_InputCountBottleAsc_ReturnTrue()
        {
            await AssertSortAsync(SortId.CountBottleAsc, (item, itemNext) => Assert.True(item.CountBottle <= itemNext.CountBottle));
        }

        [Fact]
        public async Task GetItemSorted_InputCountBottleDesc_ReturnTrue()
        {
            await AssertSortAsync(SortId.CountBottleDesc, (item, itemNext) => Assert.True(item.CountBottle >= itemNext.CountBottle));
        }

        private async Task AssertSortAsync(SortId sortId, Action<Flaschenpost.Item, Flaschenpost.Item> assertAction)
        {
            var items = await _rep.GetItemsAsync(sortId);
            for (var index = 0; index < index - 1; index++)
            {
                var item = items[index];
                var itemNext = items[index + 1];

                assertAction.Invoke(item, itemNext);
            }
        }

        [Theory]
        [InlineData(SortId.PriceAsc)]
        [InlineData(SortId.PriceDesc)]
        [InlineData(SortId.PricePerBottleAsc)]
        [InlineData(SortId.PricePerBottleDesc)]
        [InlineData(SortId.PricePerUnitAsc)]
        [InlineData(SortId.PricePerUnitDesc)]
        [InlineData(SortId.CountBottleAsc)]
        [InlineData(SortId.CountBottleDesc)]
        public async Task GetItemSorted_InputAll_ReturnTrue(SortId sortId)
        {
            var assertAction = getAssertAction();

            var rep = new RemoteProductsRepository(new DemoRemoteProductsSource());
            var items = await rep.GetItemsAsync(sortId);

            for (var index = 0; index < items.Count - 1; index++)
            {
                var item = items[index];
                var itemNext = items[index + 1];

                assertAction.Invoke(item, itemNext);
            }


            Action<Flaschenpost.Item, Flaschenpost.Item> getAssertAction()
            {
                return (item, itemNext) => Assert.True(getCondition(item, itemNext));

                bool getCondition(Flaschenpost.Item item, Flaschenpost.Item itemNext)
                {
                    return sortId switch
                    {
                        SortId.PriceAsc => item.Price <= itemNext.Price,
                        SortId.PriceDesc => item.Price >= itemNext.Price,
                        SortId.PricePerBottleAsc => item.PricePerBottle <= itemNext.PricePerBottle,
                        SortId.PricePerBottleDesc => item.PricePerBottle >= itemNext.PricePerBottle,
                        SortId.PricePerUnitAsc => item.PricePerUnit <= itemNext.PricePerUnit,
                        SortId.PricePerUnitDesc => item.PricePerUnit >= itemNext.PricePerUnit,
                        SortId.CountBottleAsc => item.CountBottle <= itemNext.CountBottle,
                        SortId.CountBottleDesc => item.CountBottle >= itemNext.CountBottle,
                        _ => throw new NotImplementedException(),
                    };
                }
            }
        }


        [Fact]
        public async Task GetItemSorted_InputInvalidData_ReturnTrue()
        {
            var rep = new RemoteProductsRepository(new InvalidRemoteProductsSource());
            var items = await rep.GetItemsAsync(SortId.PriceAsc);
            Assert.True(items == null);
        }

        private class InvalidRemoteProductsSource : IRemoteProductsSource
        {
            public Task<string> GetContentAsJsonAsync()
            {
                return Task.FromResult("");
            }
        }
    }
}
