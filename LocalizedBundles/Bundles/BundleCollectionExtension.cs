using System;
using Microsoft.Web.Optimization;

namespace LocalizedBundles.Bundles
{
    public static class BundleCollectionExtension
    {
        public static void AddLocalizedBundles(this BundleCollection collection, Bundle bundle, BundleLocalizer bundleLocalizer, ResourceCultureProvider cultureProvider, string cultureInvariantVirtualPath = null)
        {
            if (!bundle.Path.Contains("{0}"))
            {
                throw new Exception("Bundle path must contain {0} as placeholder for culture string");
            }

            var cultures = cultureProvider.GetDefinedCultures();
            foreach (var culture in cultures)
            {
                var path = String.Format(bundle.Path, culture.Name);
                var localizedBundle = new LocalizedBundleWrapper(bundle, path, culture, bundleLocalizer);
                collection.Add(localizedBundle);
                
            }

            if (!string.IsNullOrEmpty(cultureInvariantVirtualPath))
            {
                var culture = cultureProvider.GetDefaultCulture();
                var cultureInvariantBundle = new LocalizedBundleWrapper(bundle, cultureInvariantVirtualPath, culture, bundleLocalizer);
                collection.Add(cultureInvariantBundle);
            }            
        }
    }
}
