using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.WebAPI_PL.Filters;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Patents;

namespace Epam.Task01.Library.WebAPI_PL.Controllers
{
    [Route("api/patent")]
    [LogErrorAttribute]
    [LogUnauthorizedAccess("External")]
    public class PatentController : ApiController
    {
        private readonly IPatentLogic _patentLogic;
        private readonly IMapper _mapper;
        private readonly IAuthorLogic _authorLogic;

        /// <summary>
        /// Controller for working with Patents in WebAPI
        /// </summary>
        /// <param name="mapper">Mapper for linking the patent's model to the patent and back</param>
        /// <param name="patentLogic">Linking to the logic of working with book</param>
        /// <param name="authorLogic">Linking to the logic of working with author</param>
        public PatentController(IMapper mapper, IPatentLogic patentLogic, IAuthorLogic authorLogic)
        {
            _patentLogic = patentLogic;
            _mapper = mapper;
            _authorLogic = authorLogic;
        }

        /// <summary>
        /// Viewing catalog patents
        /// </summary>
        /// <param name="id">ID of the patent to view</param>
        /// <returns>If the correct id is passed and the patent is found, the patent is returned. 
        /// If the id is not passed, all the catalog patents are returned
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetPatents(int id = 0)
        {
            
            if (id > 0)
            {
                var patents = _patentLogic.GetPatents().Where(p => p.Id == id).ToList();
                if (patents == null)
                {
                    return BadRequest($"Patents with {id} not found");
                }

                return Ok(patents);
            }
            if (id == 0)
            {
                var patents= _patentLogic.GetPatents().ToList();
                if (patents == null)
                {
                    return NotFound();
                }

                return Ok(patents);
            }
            else
            {
                return BadRequest("Invalid id");
            }
        }

        /// <summary>
        /// Create new patent
        /// </summary>
        /// <param name="patentmodel">Patent's model for creating with ID author's</param>
        /// <returns>Return the result. If the patent was created successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements</returns>
        [HttpPost]
        public IHttpActionResult CreatePatent([FromBody]WebApiCreatePatentModel patentmodel)
        {
            Patent patent = new Patent();
            if (ModelState.IsValid)
            {
                patent = _mapper.Map<Patent>(patentmodel);
                patent.Authors = new List<Author>();
                foreach (int id in patentmodel.AuthorsId)
                {
                    var author = _authorLogic.GetAuthorById(id);
                    if (author != null)
                    {
                        patent.Authors.Add(author);
                    }
                }
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid)
                {
                    if (_patentLogic.AddPatent(out validationObject, patent))
                    { 
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error creating patent");
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
        /// Edit creating patent
        /// </summary>
        /// <param name="patentmodel">Patent's model edit with ID author's</param>
        /// <returns>Return the result. If the patent was edited successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements
        /// </returns>
        [HttpPut]
        public IHttpActionResult EditPatent([FromBody]WebApiEditPatentModel patentmodel)
        {
            Patent patent = new Patent();
            if (ModelState.IsValid)
            {
                patent = _mapper.Map<Patent>(patentmodel);
                patent.Authors = new List<Author>();
                foreach (int id in patentmodel.AuthorsId)
                {
                    var author = _authorLogic.GetAuthorById(id);
                    if (author != null)
                    {
                        patent.Authors.Add(author);
                    }
                }
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid)
                {
                    if (_patentLogic.EditPatent(out validationObject, patent))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error editing patent");
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
        /// Marking a patent as deleted
        /// </summary>
        /// <param name="id">ID patent for delete</param>
        /// <returns>Return the result. If the patent was deleted successfully, the response is Ok.
        /// If it fails, the response is Bad
        /// </returns>
        [HttpDelete]
        public IHttpActionResult SoftDeletePatent(int id)
        {
            if (id > 0)
            {
                if (_patentLogic.SoftDeletePatent(id))
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
