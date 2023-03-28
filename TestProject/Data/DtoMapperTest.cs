using Flaschenpost;

namespace TestProject
{
    public class DtoMapperTest
    {
        [Fact]
        public void ToItems_InputEmpty_ReturnTrue()
        {
            var productDtos = new List<ProductDto>();
            var items = DtoMapper.ToItems(productDtos);

            Assert.True(items.Count == 0);
        }

        [Fact]
        public void ToItems_Input1Product1Article_ReturnTrue()
        {
            var productDtoId = 1;
            var productDtoBrandName = "BrandName1";
            var productDtoName = "Name1";
            var productDtoDescrition = "Description1";

            var articleDtoId = 11;
            var articleDtoUnit = "Liter1";
            var articleDtoImage = "Url1";
            var articleDtoPrice = 10.00;
            var articleDtoShortDescription = "10 x 0,5L (Glas)";
            var articleDtoPricePerUnitText = "(1,10 €/Liter)";

            var productDtos = new List<ProductDto>() { };
            productDtos.Add(new ProductDto()
            {
                Id = productDtoId,
                BrandName = productDtoBrandName,
                Name = productDtoName,
                DescriptionText = productDtoDescrition,

                Articles = new List<ArticleDto>()
                {
                    new ArticleDto()
                    {
                        Id = articleDtoId,
                        ShortDescription = articleDtoShortDescription,
                        Price = articleDtoPrice,
                        Unit = articleDtoUnit,
                        PricePerUnitText = articleDtoPricePerUnitText,
                        Image = articleDtoImage,
                    },
                },
            });

            var items = DtoMapper.ToItems(productDtos);
            Assert.True(items.Count == 1);


            var item = items[0];

            Assert.True(item.Name == productDtoName);
            Assert.True(item.DescriptionText == productDtoDescrition);

            Assert.True(item.ShortDescription == articleDtoShortDescription);
            Assert.True(item.PricePerUnitText == articleDtoPricePerUnitText);
            Assert.True(item.Price == articleDtoPrice);
            Assert.True(item.UrlImage == articleDtoImage);

            // test parse extra data
            Assert.True(item.PricePerUnit == 1.1);
            Assert.True(item.CountBottle == 10);
        }

        [Fact]
        public void ToItems_Input1Product2Article_ReturnTrue()
        {
            var articleDto1 = new ArticleDto()
            {
                Id = 11,
                Price = 10.00,
                Unit = "Liter1",
                Image = "Url1",

                ShortDescription = "10 x 0,5L (Glas)",
                PricePerUnitText = "(1,10 €/Liter)",
            };

            var articleDto2 = new ArticleDto()
            {
                Id = 12,
                Price = 12.00,
                Unit = "Liter2",
                Image = "Url2",

                ShortDescription = "20 x 0,5L (Glas)",
                PricePerUnitText = "(2,10 €/Liter)",
            };

            var productDto = new ProductDto()
            {
                Id = 1,
                BrandName = "BrandName1",
                Name = "Name1",
                DescriptionText = "Description1",

                Articles = new List<ArticleDto>() { articleDto1, articleDto2, },
            };

            var productDtos = new List<ProductDto>() { productDto };

            var items = DtoMapper.ToItems(productDtos);
            Assert.True(items.Count == 2);

            asssert(items[0], productDto, articleDto1);
            asssert(items[1], productDto, articleDto2);
            static void asssert(Item item, ProductDto productDto, ArticleDto articleDto)
            {
                Assert.True(item.Name == productDto.Name);
                Assert.True(item.DescriptionText == productDto.DescriptionText);

                Assert.True(item.ShortDescription == articleDto.ShortDescription);
                Assert.True(item.PricePerUnitText == articleDto.PricePerUnitText);
                Assert.True(item.Price == articleDto.Price);
                Assert.True(item.UrlImage == articleDto.Image);
            }
        }

        [Fact]
        public void ToItems_Input1Product0Article_ReturnTrue()
        {
            var productDto = new ProductDto()
            {
                Id = 1,
                BrandName = "BrandName1",
                Name = "Name1",
                DescriptionText = "Description1",
            };

            var productDtos = new List<ProductDto>() { productDto };

            var items = DtoMapper.ToItems(productDtos);
            Assert.True(items.Count == 0);
        }

        [Fact]
        public void ToItems_InputInvalidParseData_ReturnTrue()
        {
            var articleDto = new ArticleDto()
            {
                Id = 11,
                Price = 10.00,
                Unit = "Liter1",
                Image = "Url1",

                ShortDescription = "",
                PricePerUnitText = "",
            };

            var productDto = new ProductDto()
            {
                Id = 1,
                BrandName = "BrandName1",
                Name = "Name1",
                DescriptionText = "Description1",

                Articles = new List<ArticleDto>() { articleDto, },
            };

            var productDtos = new List<ProductDto>() { productDto };

            var items = DtoMapper.ToItems(productDtos);
            var item = items[0];
            Assert.True(item.PricePerUnit == 0);
            Assert.True(item.CountBottle == 0);
        }
    }
}
