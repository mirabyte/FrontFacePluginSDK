using Android.Graphics;

namespace Sample_Content_Plugin_Android
{
    public class ResourceHelper
    {
        /// <summary>
        /// Gets the resource stream via the given name
        /// </summary>
        /// <param name="resName">e.g. /images/image.png</param>
        /// <returns></returns>
        public static Stream GetManifestResourceStream(string resName)
        {
            var currentAssembly = typeof(ResourceHelper).Assembly;
            resName = resName.Replace(@"/", ".");
            return currentAssembly.GetManifestResourceStream(currentAssembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(resName)));
        }

        public static Bitmap LoadBitmapFromEmbeddedResource(string resName)
        {
            var stream = GetManifestResourceStream(resName);
            return BitmapFactory.DecodeStream(stream);
        }
    }
}