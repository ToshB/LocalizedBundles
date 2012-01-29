Sample Global.asax
```c#
    public class Global : System.Web.HttpApplication
    {
        // Initialize BundleLocalizer with Regex rule for replacements, along with a reference to the resourcemanager to use
        readonly BundleLocalizer _bundleLocalizer = new BundleLocalizer(@"MyStrings.([\w]*)", MyStrings.ResourceManager);
        
        // Initialize the culture provider with an cultureinfo object specifying the base .resx culture
        private readonly ResourceCultureProvider _cultureProvider = new ResourceCultureProvider(new CultureInfo("en-US"));

        protected void Application_Start(object sender, EventArgs e)
        {
            // In the virtualPath string, specify where the cultureinfo name should be inserted
            var bundle = new Bundle("~/scripts.{0}.js", typeof (JsMinify));
            bundle.AddFile("~/JavaScript/MyScript.js");

            // Use the AddLocalizedBundles extension method for adding bundles, with an optional default culture virtual path
            BundleTable.Bundles.AddLocalizedBundles(bundle, _bundleLocalizer, _cultureProvider, "~/scripts.js");
        }   
    }
```