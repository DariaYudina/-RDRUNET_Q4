using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.WebAPI_PL.Filters;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Issues;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Newspapers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Epam.Task01.Library.WebAPI_PL.Controllers
{
    [Route("api/newspaper")]
    [LogErrorAttribute]
    [LogUnauthorizedAccess("External")]
    public class NewspaperController : ApiController
    {
        private readonly INewspaperLogic _newspaperLogic;
        private readonly IIssueLogic _issueLogic;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller for working with Newspapers in WebAPI
        /// </summary>
        /// <param name="mapper">Mapper for linking the newspapers's model to the newspaper and back</param>
        /// <param name="newspaperLogic">Linking to the logic of working with newspaper</param>
        /// <param name="issueLogic">Linking to the logic of working with issue</param>
        public NewspaperController(IMapper mapper, INewspaperLogic newspaperLogic, IIssueLogic issueLogic)
        {
            _newspaperLogic = newspaperLogic;
            _mapper = mapper;
            _issueLogic = issueLogic;
        }

        /// <summary>
        /// Viewing catalog newspapers
        /// </summary>
        /// <param name="id">ID of the newspaper to view</param>
        /// <returns>If the correct id is passed and the newspaper is found, the newspaper is returned. 
        /// If the id is not passed, all the catalog newspapers are returned
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetNewspapers(int id = 0)
        {
            
            if (id > 0)
            {
                var newspapers = _mapper.Map<List<WebApiNewspaperModel>>(_newspaperLogic.GetNewspapers().Where(n => n.Id == id).ToList());
                if (newspapers == null)
                {
                    return BadRequest($"Newspapers with {id} not found");
                }

                foreach (var newspaper in newspapers)
                {
                    newspaper.Issues = _mapper.Map<List<WebApiIssueWithoutNewspaperModel>>(_issueLogic.GetIssuesByNewspaperId(newspaper.Id, 0));
                }

                return Ok(newspapers);
            }
            if (id == 0)
            {
                var newspapers = _mapper.Map<List<WebApiNewspaperModel>>(_newspaperLogic.GetNewspapers().ToList());
                if (newspapers == null)
                {
                    return NotFound();
                }

                foreach (var newspaper in newspapers)
                {
                    newspaper.Issues = _mapper.Map<List<WebApiIssueWithoutNewspaperModel>>(_issueLogic.GetIssuesByNewspaperId(newspaper.Id, 0));
                }

                return Ok(newspapers);
            }
            else
            {
                return BadRequest("Invalid id");
            }
        }

        /// <summary>
        /// Create new newspaper
        /// </summary>
        /// <param name="newspapermodel">Newspaper's model for creating</param>
        /// <returns>Return the result. If the newspaper was created successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements</returns>
        [HttpPost]
        public IHttpActionResult CreateNewspaper([FromBody]WebApiCreateNewspaperModel newspapermodel)
        {            
            Newspaper newspaper = new Newspaper();
            if (ModelState.IsValid)
            {
                newspaper = _mapper.Map<Newspaper>(newspapermodel);
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid)
                {
                    if (_newspaperLogic.AddNewspaper(out validationObject, newspaper))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error creating newspaper");
                    }
                }
                else
                {
                    foreach (var i in validationObject.ValidationExceptions)
                    {
                        if (ModelState.ContainsKey(i.Property))
                        {
                            ModelState.AddModelError(i.Property, i.Message);
                        }
                        else
                        {
                            ModelState.AddModelError("", i.Message);
                        }
                    }

                    return BadRequest(ModelState);
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Edit creating newspaper
        /// </summary>
        /// <param name="newspapermodel">Newspaper's model edit</param>
        /// <returns>Return the result. If the newspaper was edited successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements
        /// </returns>
        [HttpPut]
        public IHttpActionResult EditNewspaper([FromBody]WebApiEditNewspaperModel newspapermodel)
        {
            Newspaper newspaper = new Newspaper();
            if (ModelState.IsValid)
            {
                newspaper = _mapper.Map<Newspaper>(newspapermodel);
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid)
                {
                    if (_newspaperLogic.EditNewspaper(out validationObject, newspaper))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error editing newspaper");
                    }
                }
                else
                {
                    foreach (var i in validationObject.ValidationExceptions)
                    {
                        if (ModelState.ContainsKey(i.Property))
                        {
                            ModelState.AddModelError(i.Property, i.Message);
                        }
                        else
                        {
                            ModelState.AddModelError("", i.Message);
                        }
                    }

                    return BadRequest(ModelState);
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Marking a newspaper as deleted
        /// </summary>
        /// <param name="id">ID newspaper for delete</param>
        /// <returns>Return the result. If the newspaper was deleted successfully, the response is Ok.
        /// If it fails, the response is Bad
        /// </returns>
        [HttpDelete]
        public IHttpActionResult SoftDeleteNewspaper(int id)
        {
            if (id > 0)
            {
                if (_newspaperLogic.SoftDeleteNewspaper(id))
                {
                    return Ok();
                }

                return BadRequest("Deletion error");
            }
            else
            {
                return BadRequest("Invalid Id");
            }
        }
    }
}
