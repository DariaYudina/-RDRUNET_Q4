using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.WebAPI_PL.Filters;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epam.Task01.Library.WebAPI_PL.Controllers
{
    [Route("api/issue")]
    [LogErrorAttribute]
    [LogUnauthorizedAccess("External")]
    public class IssueController : ApiController
    {
        private readonly IIssueLogic _issueLogic;
        private readonly IMapper _mapper;
        private readonly INewspaperLogic _newspaperLogic;

        /// <summary>
        /// Controller for working with Issues in WebAPI
        /// </summary>
        /// <param name="mapper">Mapper for linking the issue's model to the issue and back</param>
        /// <param name="issueLogic">Linking to the logic of working with issue</param>
        /// <param name="newspaperLogic">Linking to the logic of working with newspaper</param>
        public IssueController(IMapper mapper, IIssueLogic issueLogic, INewspaperLogic newspaperLogic)
        {
            _issueLogic = issueLogic;
            _mapper = mapper;
            _newspaperLogic = newspaperLogic;
        }

        /// <summary>
        /// Viewing catalog issues
        /// </summary>
        /// <param name="id">ID of the issue to view</param>
        /// <returns>If the correct id is passed and the issue is found, the issue is returned. 
        /// If the id is not passed, all the catalog issues are returned
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetIssues(int id = 0)
        {         
            if (id > 0)
            {
                var issues = _issueLogic.GetIssues().Where(i => i.Id == id).ToList();
                if (issues == null)
                {
                    return BadRequest($"Issues with {id} not found");
                }

                return Ok(issues);
            }
            if (id == 0)
            {
                var issues = _issueLogic.GetIssues().ToList();
                if (issues == null)
                {
                    return NotFound();
                }

                return Ok(issues);
            }
            else
            {
                return BadRequest("Invalid id");
            }
        }

        /// <summary>
        /// Create new issue
        /// </summary>
        /// <param name="issuemodel">Issue's model for creating</param>
        /// <returns>Return the result. If the issue was created successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements</returns>
        [HttpPost]
        public IHttpActionResult CreateIssue([FromBody]WebApiCreateIssueModel issuemodel)
        {
            Issue issue = new Issue();
            if (ModelState.IsValid)
            {
                issuemodel.Newspaper = _newspaperLogic.GetNewspaperById(issuemodel.NewspaperId);
                issue = _mapper.Map<Issue>(issuemodel);
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid)
                {
                    if (_issueLogic.AddIssue(out validationObject, issue))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error creating issue");
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
        /// Edit creating issue
        /// </summary>
        /// <param name="issuemodel">Issue's model edit</param>
        /// <returns>Return the result. If the issue was edited successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements
        /// </returns>
        [HttpPut]
        public IHttpActionResult EditIssue([FromBody]WebApiEditIssueModel issuemodel)
        {
            Issue issue = new Issue();
            if (ModelState.IsValid)
            {
                issue = _mapper.Map<Issue>(issuemodel);
                issue.Newspaper = _newspaperLogic.GetNewspaperById(issuemodel.NewspaperId);
                if (issue.Newspaper == null)
                {
                    return BadRequest();
                }

                issue.Title = issue.Newspaper.Title;
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid)
                {
                    if (_issueLogic.EditIssue(out validationObject, issue))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error editing issue");
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
        /// Marking a issue as deleted
        /// </summary>
        /// <param name="id">ID issue for delete</param>
        /// <returns>Return the result. If the issue was deleted successfully, the response is Ok.
        /// If it fails, the response is Bad
        /// </returns>
        [HttpDelete]
        public IHttpActionResult SoftDeleteIssue(int id)
        {
            if (id > 0)
            {
                if (_issueLogic.SoftDeleteIssue(id))
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
