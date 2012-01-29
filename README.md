This is a sample showing how Bundles from Microsoft.Web.Optimization can be utilized to provide server-side JavaScript localization based on resources in satellite assemblies.

When creating bundles you specify a virtual path for the Bundle. This path is registered as a route on application startup (if the initialization logic is placed in Application_Start). On the first request to the route, the bundle files are parsed, concatenated and optionally transformed. The result is then cached for later requests. 

Due to this caching logic, and the fact that the bundles/virtual paths does not support parameters, I needed to write logic to register a bundle for each of the defined cultures. And optionally the default culture on a culture-invariant virtualPath. The ResourceCultureProvider provides a list of CultureInfo-objects for the defined cultures. 

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