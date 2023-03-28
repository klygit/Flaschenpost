using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flaschenpost
{
    public class ProductDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("brandName")]
        public string BrandName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("descriptionText")]
        public string DescriptionText { get; set; }

        [JsonPropertyName("articles")]
        public IList<ArticleDto> Articles { get; set; }
    }
}
