using System.Web.Http;
using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.WebAPI_PL.Filters;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Authors;

namespace Epam.Task01.Library.WebAPI_PL.Controllers
{
    [LogErrorAttribute]
    [LogUnauthorizedAccess("External")]
    [LogErrorAttribute]
    public class AuthorController : ApiController
    {
        private readonly IAuthorLogic _authorLogic;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller for working with authors in WebAPI
        /// </summary>
        /// <param name="mapper">Mapper for linking the author's model to the author</param>
        /// <param name="authorLogic">Linking to the logic of working with authors</param>
        public AuthorController(IMapper mapper, IAuthorLogic authorLogic)
        {
            _authorLogic = authorLogic;
            _mapper = mapper;
        }

        /// <summary>
        /// Viewing catalog authors
        /// </summary>
        /// <param name="search">Optional parameter. Search for authors by the passed substring</param>
        /// <returns>Getting all catalog authors or getting authors according to the passed substring</returns>
        [HttpGet]
        public IHttpActionResult GetAuthor(string search = "")
        {
            if (!string.IsNullOrEmpty(search))
            {
                var authors = _authorLogic.GetAuthorsByString(search);
                return Ok(authors);
            }
            else
            {
                var authors = _authorLogic.GetAuthors();
                return Ok(authors);
            }
        }

        /// <summary>
        /// Create new author
        /// </summary>
        /// <param name="authormodel">Author's model with name and surname for creating</param>
        /// <returns>Return the result. If the author was created successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements</returns>
        [HttpPost]
        public IHttpActionResult CreateAuthor([FromBody]WebApiCreateAuthorModel authormodel)
        {
            if (ModelState.IsValid)
            {
                ValidationObject validationObject = new ValidationObject();
                var author = _mapper.Map<Author>(authormodel);
                if (validationObject.IsValid)
                {
                    if (_authorLogic.AddAuthor(out validationObject, author))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error creating author");
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
        /// Edit creating author
        /// </summary>
        /// <param name="authormodel">Author's model with name and surname for edit</param>
        /// <returns>Return the result. If the author was edited successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements
        /// </returns>
        [HttpPut]
        public IHttpActionResult EditAuthor([FromBody]WebApiEditAuthorModel authormodel)
        {
            if (ModelState.IsValid)
            {
                ValidationObject validationObject = new ValidationObject();
                var author = _mapper.Map<Author>(authormodel);
                if (validationObject.IsValid)
                {
                    if (_authorLogic.EditAuthor(out validationObject, author))
                    {
                        return Ok();
                    }
                    else
                    {
                        BadRequest("Error editing author");
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
    }
}
