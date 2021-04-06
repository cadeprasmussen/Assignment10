using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Infastructure
{

    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;
        public PaginationTagHelper (IUrlHelperFactory uhf)
        {
            urlInfo = uhf;
        }
        public PageNumberingInfo PageInfo { get; set; }
        public string TeamName { get; set; }

        //Creating the dictionary or Key value pairs, for the page-urls- to be created

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();
        
        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        //Making the buttons look nice, adding css to pagination
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        //Creating the tags for and html elements for the pagination
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder finishedTag = new TagBuilder("div");

            for(int i = 1; i <= PageInfo.NumPages; i++)
            {
                IUrlHelper urlHelper = urlInfo.GetUrlHelper(ViewContext);
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pagenum"] = i;
                individualTag.Attributes["href"] = urlHelper.Action("Index", KeyValuePairs);

                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);
                    individualTag.AddCssClass(i == PageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                individualTag.InnerHtml.Append(i.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            output.Content.AppendHtml(finishedTag.InnerHtml);
        }



    }
}
