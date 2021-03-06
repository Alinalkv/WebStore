﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        private const string IgnoreActionName = "ignore-action";

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ignore_action = output.Attributes.ContainsName(IgnoreActionName);
            
            if (IsActive(ignore_action))
                MakeActive(output);
            output.Attributes.RemoveAll(AttributeName);
            output.Attributes.RemoveAll(IgnoreActionName);
        }

        private bool IsActive(bool ignore_action)
        {
            //словарь из текущего маршрута
            var route_values = ViewContext.RouteData.Values;
            var curent_controller = route_values["controller"].ToString();
            var curent_action = route_values["action"].ToString();

            //если совпадают параметры из строки запроса, словаря и то, что передано через в action и controller, то ок
            const StringComparison ignore_case = StringComparison.OrdinalIgnoreCase;
            if (!string.IsNullOrEmpty(Controller) && !string.Equals(curent_controller, Controller, ignore_case))
                return false;
            if (!ignore_action && !string.IsNullOrEmpty(Action) && !string.Equals(curent_action, Action, ignore_case))
                return false;

            foreach(var (key, value) in RouteValues)
            {
                if (!route_values.ContainsKey(key) || route_values[key].ToString() != value)
                    return false;
            }

            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            var class_attribute = output.Attributes.FirstOrDefault(attr => attr.Name == "class");

            if (class_attribute is null)
                output.Attributes.Add("class", "active");
            else
            {
                if (class_attribute.Value.ToString()?.Contains("active") ?? false) return;
                output.Attributes.SetAttribute("class", class_attribute.Value + " active");
            }

        }
    }
}
