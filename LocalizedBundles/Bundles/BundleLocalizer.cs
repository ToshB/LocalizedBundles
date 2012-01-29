using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;

namespace LocalizedBundles.Bundles
{
    public class BundleLocalizer
    {
        private readonly ResourceManager _resourceManager;
        private readonly Regex _regex;

        public BundleLocalizer(string regex, ResourceManager resourceManager)
        {
            _regex = new Regex(regex, RegexOptions.Singleline | RegexOptions.Compiled);
            _resourceManager = resourceManager;
        }

        public string LocalizeText(string text, CultureInfo culture)
        {
            MatchCollection matches = _regex.Matches(text);
            var missingResources = new List<string>();
            foreach (Match match in matches)
            {
                var localizedString = _resourceManager.GetString(match.Groups[1].Value, culture);
                if (localizedString != null)
                {
                    text = text.Replace(match.Value, localizedString);
                }
                else
                {
                    missingResources.Add(match.Value);
                }
            }
            if (missingResources.Count > 0)
            {
                throw new Exception("Missing resources: " + string.Join(", ", missingResources));
            }
            return text;
        }
    }
}