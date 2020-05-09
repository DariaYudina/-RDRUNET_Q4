using AutoMapper;
using Epam.Task_01.Library.AbstactBLL;
using Epam.Task_01.Library.AbstactBLL.IValidators;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.WebAPI_PL.Filters;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epam.Task01.Library.WebAPI_PL.Controllers
{
    [Route("api/book")]
    [LogUnauthorizedAccess("External")]
    [LogErrorAttribute]
    public class BookController : ApiController
    {
        private readonly IBookLogic _bookLogic;
        private readonly IMapper _mapper;
        private readonly IAuthorLogic _authorLogic;

        /// <summary>
        /// Controller for working with Boooks in WebAPI
        /// </summary>
        /// <param name="mapper">Mapper for linking the book's model to the book and back</param>
        /// <param name="bookLogic">Linking to the logic of working with book</param>
        /// <param name="authorLogic">Linking to the logic of working with author</param>
        public BookController(IMapper mapper, IBookLogic bookLogic, IAuthorLogic authorLogic)
        {
            _bookLogic = bookLogic;
            _mapper = mapper;
            _authorLogic = authorLogic;
        }

        /// <summary>
        /// Viewing catalog books
        /// </summary>
        /// <param name="id">ID of the book to view</param>
        /// <returns>If the correct id is passed and the book is found, the book is returned. 
        /// If the id is not passed, all the catalog books are returned
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetBooks(int id = 0)
        {
            if (id > 0)
            {
                var books = _bookLogic.GetBooks().Where(b => b.Id == id).ToList();
                if (books == null)
                {
                    return BadRequest($"Books with {id} not found");
                }

                return Ok(books);
            }
            if (id == 0)
            {
                var books = _bookLogic.GetBooks().ToList();
                if (books == null)
                {
                    return NotFound();
                }

                return Ok(books);
            }
            else
            {
                return BadRequest("Invalid id");
            }
        }

        /// <summary>
        /// Create new book
        /// </summary>
        /// <param name="bookmodel">Book's model for creating with ID author's</param>
        /// <returns>Return the result. If the book was created successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements</returns>
        [HttpPost]
        public IHttpActionResult CreateBook([FromBody]WebApiCreateBookModel bookmodel)
        {
            Book book = new Book();       
            if (ModelState.IsValid)
            {
                book = _mapper.Map<Book>(bookmodel);
                book.Authors = new List<Author>();
                foreach (int id in bookmodel.AuthorsId)
                {
                    var author = _authorLogic.GetAuthorById(id);
                    if (author != null)
                    {
                        book.Authors.Add(author);
                    }
                }
                ValidationObject validationObject = new ValidationObject();                
                if (validationObject.IsValid)
                {
                    if (_bookLogic.AddBook(out validationObject, book))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error creating book");
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
        /// Edit creating book
        /// </summary>
        /// <param name="bookmodel">Book's model edit with ID author's</param>
        /// <returns>Return the result. If the book was edited successfully, the response is Ok.
        /// If it fails, there is an error in the validation requirements
        /// </returns>
        [HttpPut]
        public IHttpActionResult EditBook([FromBody]WebApiEditBookModel bookmodel)
        {
            Book book = new Book();
            if (ModelState.IsValid)
            {
                book = _mapper.Map<Book>(bookmodel);
                book.Authors = new List<Author>();
                foreach (int id in bookmodel.AuthorsId)
                {
                    var author = _authorLogic.GetAuthorById(id);
                    if (author != null)
                    {
                        book.Authors.Add(author);
                    }
                }
                ValidationObject validationObject = new ValidationObject();
                if (validationObject.IsValid) 
                {
                    if (_bookLogic.EditBook(out validationObject, book))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Error editing book");
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
        /// Marking a book as deleted
        /// </summary>
        /// <param name="id">ID book for delete</param>
        /// <returns>Return the result. If the book was deleted successfully, the response is Ok.
        /// If it fails, the response is Bad
        /// </returns>
        [HttpDelete]
        public IHttpActionResult SoftDeleteBook(int id)
        {
            if (id > 0)
            {
                if (_bookLogic.SoftDeleteBook(id))
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
