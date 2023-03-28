using System.Collections.Generic;

namespace Flaschenpost
{
    public class DtoMapper
    {
        public static List<Item> ToItems(IList<ProductDto> productDtos)
        {
            var items = new List<Item>();

            if (productDtos != null)
            {
                foreach (var productDto in productDtos)
                {
                    if (productDto.Articles == null)
                        continue;

                    foreach (var articleDto in productDto.Articles)
                    {
                        // merge productDto and articleDto to one item object
                        items.Add(ToItem(productDto, articleDto));
                    }
                }
            }

            return items;
        }

        private static Item ToItem(ProductDto productDto, ArticleDto articleDto)
        {
            var item = new Item()
            {
                Name = productDto.Name,
                DescriptionText = productDto.DescriptionText,

                ShortDescription = articleDto.ShortDescription,
                Price = articleDto.Price,
                PricePerUnitText = articleDto.PricePerUnitText,
                UrlImage = articleDto.Image,
            };


            // parse some extra value for sorting
            item.PricePerUnit = parsePricePerUnit();
            double parsePricePerUnit()
            {
                var indexOfEnd = item.PricePerUnitText.IndexOf("€");
                if (indexOfEnd == -1 || indexOfEnd >= item.PricePerUnitText.Length)
                    return 0;

                var pricePerUnitStr = item.PricePerUnitText.Substring(0, indexOfEnd).Replace("(", "");
                double.TryParse(pricePerUnitStr, out var pricePerUnit);
                return pricePerUnit;
            }

            item.CountBottle = parseCountBottle();
            int parseCountBottle()
            {
                var indexOfEnd = item.ShortDescription.IndexOf("x");
                if (indexOfEnd == -1 || indexOfEnd >= item.ShortDescription.Length)
                    return 0;

                var countBottleStr = item.ShortDescription.Substring(0, indexOfEnd).Trim();
                int.TryParse(countBottleStr, out var countBottle);
                return countBottle;
            }

            return item;
        }
    }
}
