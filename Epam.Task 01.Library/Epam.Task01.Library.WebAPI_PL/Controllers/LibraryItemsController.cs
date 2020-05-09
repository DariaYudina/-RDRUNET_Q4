using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task01.Library.WebAPI_PL.Filters;
using Epam.Task01.Library.WebAPI_PL.Models;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.LibraryItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epam.Task01.Library.WebAPI_PL.Controllers
{
    [LogErrorAttribute]
    [LogUnauthorizedAccess("External")]
    public class LibraryItemsController : ApiController
    {

        private readonly IMapper _mapper;
        private readonly ICommonLogic _commonLogic;

        /// <summary>
        /// Controller for working with LibraryItems in WebAPI
        /// </summary>
        /// <param name="mapper">Mapper for linking the Library Items's model to the Library Items</param>
        /// <param name="commonLogic">Linking to the logic of working with Library Items</param>
        public LibraryItemsController(IMapper mapper, ICommonLogic commonLogic)
        {
            _mapper = mapper;
            _commonLogic = commonLogic;
        }

        /// <summary>
        /// Viewing catalog Items
        /// </summary>
        /// <param name="strtype">Type or types of Items for viewing (newspapers, book, patent). If omitted, the entire folder is returned</param>
        /// <param name="name">Substring of name for searching. If omitted, the entire folder is returned.</param>
        /// <param name="mincountpages">Minimum number pages of Item lybrary to view. If omitted, the entire folder will be returned</param>
        /// <param name="maxcountpages">Maximum number pages of Item lybrary to view. If omitted, the entire folder will be returned</param>
        /// <param name="countitemonpage">Count of item on page to view. If omitted, the entire folder will be returned</param>
        /// <param name="curpage">Current page of item to view</param>
        /// <returns>Return items of catalog with with the specified parametres</returns>
        public IHttpActionResult Get(string strtype = "", string name = "", int mincountpages = 0, int maxcountpages = int.MaxValue,
                                     int countitemonpage = 0, int curpage = 1)
        {
            var allrecords = _mapper.Map<List<WebApiLibraryItemsModel>>(_commonLogic.GetLibraryItems().ToList());
            List<WebApiLibraryItemsModel> getrecords = new List<WebApiLibraryItemsModel>();
            if (strtype != "")
            {
                string[] arraytype = strtype.Split(' ', ';', ':', ',');
                foreach (var type in arraytype)
                {
                    getrecords.AddRange(allrecords.FindAll(r => r.Type.ToUpper() == type.ToUpper()));
                }
            }
            else
            {
                getrecords = allrecords;
            }

            if (name != "")
            {
                getrecords = getrecords.FindAll(rec => rec.Title.ToUpper().Contains(name.ToUpper()));
            }

            if (mincountpages > 0)
            {
                getrecords = getrecords.FindAll(rec => rec.PagesCount >= mincountpages);
            }
            else if (mincountpages < 0)
            {
                return BadRequest("Minimum count of pages cann't be less zero");
            }

            if (maxcountpages < int.MaxValue)
            {
                getrecords = getrecords.FindAll(rec => rec.PagesCount <= maxcountpages);
            }
            else if (maxcountpages < 0)
            {
                return BadRequest("Maximum count of pages cann't be less zero");
            }

            WebApiLibraryItemsPagesModels pagesrecord = new WebApiLibraryItemsPagesModels();
            if (countitemonpage > 0 && curpage > 0)
            {
                pagesrecord.TotalPages = (int)Math.Ceiling((decimal)getrecords.Count() / countitemonpage);
                if (curpage > pagesrecord.TotalPages)
                {
                    return BadRequest("The specified page is greater than the total number of pages");
                }

                pagesrecord.CurPage = curpage;
                pagesrecord.LibraryItem = getrecords.Skip((curpage - 1) * countitemonpage).Take(countitemonpage);
            }
            else
            {
                if (countitemonpage < 0 || curpage <= 0)
                {
                    return BadRequest("Error in page settings");
                }

                pagesrecord.TotalPages = 1;
                pagesrecord.CurPage = 1;
                pagesrecord.LibraryItem = getrecords;
            }

            if (pagesrecord.LibraryItem == null || pagesrecord.LibraryItem.Count() == 0)
            {
                return NotFound();
            }

            return Ok(pagesrecord);
        }
    }
}
