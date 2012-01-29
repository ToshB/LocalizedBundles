using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace LocalizedBundles.Bundles
{
    public class ResourceCultureProvider
    {
        private readonly CultureInfo _defaultCulture;
        private readonly List<CultureInfo> _cultures;

        public ResourceCultureProvider(CultureInfo defaultCulture)
        {
            _defaultCulture = defaultCulture;
            _cultures = new List<CultureInfo>();
            foreach (var file in Directory.GetFiles(HttpRuntime.AppDomainAppPath + "/App_GlobalResources", "*.resx", SearchOption.TopDirectoryOnly))
            {
                var name = file.Split('\\').Last();
                name = name.Split('.')[1];

                var culture = name != "resx" ? name : _defaultCulture.Name;
                _cultures.Add(new CultureInfo(culture));
            }
        }

        public List<CultureInfo> GetDefinedCultures()
        {
            return _cultures;
        }

        public CultureInfo GetDefaultCulture()
        {
            return _defaultCulture;
        }
    }
}