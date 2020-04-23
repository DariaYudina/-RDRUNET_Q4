using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Epam.Task01.Library.MVC_PL.Models;

namespace Epam.Task01.Library.MVC_PL.Halpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(
            this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            var limit = 3;
            if(pageInfo.PageNumber != 1)
            {
                TagBuilder tagPrevious = new TagBuilder("a");
                result.Append(GenerateHref(tagPrevious, 1, "Previous", pageUrl, pageInfo).ToString());
            }
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                if ( i >= pageInfo.PageNumber - limit && i <= pageInfo.PageNumber + limit)
                {
                    result.Append(GenerateHref(tag, i, i.ToString(), pageUrl, pageInfo).ToString());
                }
                var slice = 1 + limit - pageInfo.PageNumber;
                if (pageInfo.PageNumber <= limit && i <= pageInfo.PageNumber + (limit + slice)
                    && i > limit && i> pageInfo.PageNumber + limit )
                {
                    result.Append(GenerateHref(tag, i, i.ToString(), pageUrl, pageInfo).ToString());
                }
                else
                {
                    var sliceright = limit - (pageInfo.TotalItems - pageInfo.PageNumber);
                    if ( i >= pageInfo.PageNumber - (limit + sliceright) && i <= pageInfo.TotalItems &&
                         sliceright > 0  && i < pageInfo.PageNumber - limit)
                    {
                        result.Append(GenerateHref(tag, i, i.ToString(), pageUrl, pageInfo).ToString());
                    }
                }
            }
            if (pageInfo.PageNumber != pageInfo.TotalPages)
            {
                TagBuilder tagNext = new TagBuilder("a");
                result.Append(GenerateHref(tagNext, pageInfo.TotalPages, "Next", pageUrl, pageInfo).ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        private static TagBuilder GenerateHref(TagBuilder tag, int i, string text,
            Func<int, string> pageUrl, PageInfo pageInfo)
        {
            tag.MergeAttribute("href", pageUrl(i));
            tag.InnerHtml = text;
            if (i == pageInfo.PageNumber)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
                tag.AddCssClass("disabled");
            }
            tag.AddCssClass("btn btn-default ");

            return tag;
        }
    }
}
