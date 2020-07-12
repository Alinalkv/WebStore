using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = AttributeName)]
    public class ActiveRouteTagHelper : TagHelper
    {
        private const string AttributeName = "is-active-route";

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        private ViewContext ViewContext { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(AttributeName);
        }
    }
}
