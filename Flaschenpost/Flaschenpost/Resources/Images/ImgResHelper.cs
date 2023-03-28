using System.Reflection;
using Xamarin.Forms;

namespace Flaschenpost
{
    internal class ImgResHelper
    {
        internal static string ImgSrcFolderName = "Resources.Images";

        public static ImageSource GetImgSrc(string imgSrcFilename)
        {
            var type = typeof(ImgResHelper);
            var imgSrcPath = $"{type.Namespace}.{ImgSrcFolderName}.{imgSrcFilename}";
            return ImageSource.FromResource(imgSrcPath, type.GetTypeInfo()?.Assembly);
        }
    }
}
