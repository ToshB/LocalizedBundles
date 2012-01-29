using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Web.Optimization;

namespace LocalizedBundles.Bundles
{
    public class LocalizedBundleWrapper : Bundle
    {
        private readonly Bundle _bundle;
        private readonly CultureInfo _culture;
        private readonly BundleLocalizer _localizer;

        public LocalizedBundleWrapper(Bundle bundle, string virtualPath, CultureInfo culture, BundleLocalizer localizer) : base(virtualPath, bundle.TransformType)
        {
            _bundle = bundle;
            _culture = culture;
            _localizer = localizer;
        }

        public override BundleResponse GenerateBundleResponse(BundleContext context)
        {
            var response = _bundle.GenerateBundleResponse(context);
            response.Content = _localizer.LocalizeText(response.Content, _culture);
            return response;
        }

        public override IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
        {
            return _bundle.OrderFiles(context, files);
        }

        public override IEnumerable<FileInfo> EnumerateFiles(BundleContext context)
        {
            return _bundle.EnumerateFiles(context);
        }
    }
}