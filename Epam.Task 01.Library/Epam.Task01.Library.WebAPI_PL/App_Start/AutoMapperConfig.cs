using AutoMapper;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Authors;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Books;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Issues;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.LibraryItems;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Newspapers;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.Patents;
using Epam.Task01.Library.WebAPI_PL.Models.ViewModels.User;
using Ninject.Modules;
using System.Collections.Generic;

namespace Epam.Task01.Library.WebAPI_PL
{
    public class AutoMapperConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>()
                .To<Mapper>()
                .InSingletonScope()
                .WithConstructorArgument("configurationProvider", CreateConfiguration());
        }

        private MapperConfiguration CreateConfiguration()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                
                cfg.CreateMap<Book, WebApiLibraryItemsModel>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(b => "Book"))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(b => b.Title))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(b => b.Id))
                    .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(b => b.PagesCount));
                cfg.CreateMap<Patent, WebApiLibraryItemsModel>()
                   .ForMember(dest => dest.Type, opt => opt.MapFrom(p => "Patent"))
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(p => p.Title))
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.Id))
                   .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(p => p.PagesCount));
                cfg.CreateMap<Issue, WebApiLibraryItemsModel>()
                   .ForMember(dest => dest.Type, opt => opt.MapFrom(i => "Newspaper"))
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(i => i.Title))
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(i => i.Id))
                   .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(i => i.PagesCount));
                cfg.CreateMap<Newspaper, WebApiNewspaperModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(n => n.Id))
                   .ForMember(dest => dest.Issn, opt => opt.MapFrom(n => n.Issn))
                   .ForMember(dest => dest.Issues, opt => opt.MapFrom(n => new List<Issue>()))
                   .ForMember(dest => dest.PublishingCompany, opt => opt.MapFrom(n => n.PublishingCompany))
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(n => n.Title))
                   .ForMember(dest => dest.City, opt => opt.MapFrom(n => n.City));
                cfg.CreateMap<Issue, WebApiIssueWithoutNewspaperModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(i => i.Id))
                   .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(i => i.PagesCount))
                   .ForMember(dest => dest.YearOfPublishing, opt => opt.MapFrom(i => i.YearOfPublishing))
                   .ForMember(dest => dest.DateOfPublishing, opt => opt.MapFrom(i => i.DateOfPublishing))
                   .ForMember(dest => dest.Commentary, opt => opt.MapFrom(i => i.Commentary))
                   .ForMember(dest => dest.CountOfPublishing, opt => opt.MapFrom(i => i.CountOfPublishing));
                cfg.CreateMap<WebApiCreateAuthorModel, Author>()
                   .ForMember(dest => dest.FirstName, opt => opt.MapFrom(a => a.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(a => a.LastName))
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<WebApiEditAuthorModel, Author>()
                   .ForMember(dest => dest.FirstName, opt => opt.MapFrom(a => a.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(a => a.LastName))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(a => a.Id));
                cfg.CreateMap<WebApiCreateBookModel, Book>()
                   .ForMember(dest => dest.Authors, opt =>opt.Ignore())
                   .ForMember(dest => dest.City, opt => opt.MapFrom(b => b.City))
                   .ForMember(dest => dest.Commentary, opt => opt.MapFrom(b => b.Commentary))
                   .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                   .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(b => b.PagesCount))
                   .ForMember(dest => dest.PublishingCompany, opt => opt.MapFrom(b => b.PublishingCompany))
                   .ForMember(dest => dest.YearOfPublishing, opt => opt.MapFrom(b => b.YearOfPublishing))
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(b => b.Title))
                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                   .ForMember(dest => dest.Isbn, opt => opt.MapFrom(b => b.Isbn));
                cfg.CreateMap<WebApiEditBookModel, Book>()
                   .ForMember(dest => dest.Authors, opt => opt.Ignore())
                   .ForMember(dest => dest.City, opt => opt.MapFrom(b => b.City))
                   .ForMember(dest => dest.Commentary, opt => opt.MapFrom(b => b.Commentary))
                   .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                   .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(b => b.PagesCount))
                   .ForMember(dest => dest.PublishingCompany, opt => opt.MapFrom(b => b.PublishingCompany))
                   .ForMember(dest => dest.YearOfPublishing, opt => opt.MapFrom(b => b.YearOfPublishing))
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(b => b.Title))
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(b => b.Id))
                   .ForMember(dest => dest.Isbn, opt => opt.MapFrom(b => b.Isbn));
                cfg.CreateMap<WebApiCreatePatentModel, Patent>()
                   .ForMember(dest => dest.Authors, opt => opt.Ignore())
                   .ForMember(dest => dest.Country, opt => opt.MapFrom(p => p.Country))
                   .ForMember(dest => dest.Commentary, opt => opt.MapFrom(p => p.Commentary))
                   .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                   .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(p => p.PagesCount))
                   .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(p => p.ApplicationDate))
                   .ForMember(dest => dest.YearOfPublishing, opt => opt.Ignore())
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(p => p.Title))
                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                   .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(p => p.RegistrationNumber));
                cfg.CreateMap<WebApiEditPatentModel, Patent>()
                   .ForMember(dest => dest.Authors, opt => opt.Ignore())
                   .ForMember(dest => dest.Country, opt => opt.MapFrom(p => p.Country))
                   .ForMember(dest => dest.Commentary, opt => opt.MapFrom(p => p.Commentary))
                   .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                   .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(p => p.PagesCount))
                   .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(p => p.ApplicationDate))
                   .ForMember(dest => dest.YearOfPublishing, opt => opt.Ignore())
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(p => p.Title))
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.Id))
                   .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(p => p.PublicationDate))
                   .ForMember(dest => dest.RegistrationNumber, opt => opt.MapFrom(p => p.RegistrationNumber));
                cfg.CreateMap<WebApiCreateNewspaperModel, Newspaper>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                   .ForMember(dest => dest.City, opt => opt.MapFrom(n => n.City))
                   .ForMember(dest => dest.Issn, opt => opt.MapFrom(n => n.Issn))
                   .ForMember(dest => dest.PublishingCompany, opt => opt.MapFrom(n => n.PublishingCompany))
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(n => n.Title));
                cfg.CreateMap<WebApiEditNewspaperModel, Newspaper>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(n => n.Id))
                   .ForMember(dest => dest.City, opt => opt.MapFrom(n => n.City))
                   .ForMember(dest => dest.Issn, opt => opt.MapFrom(n => n.Issn))
                   .ForMember(dest => dest.PublishingCompany, opt => opt.MapFrom(n => n.PublishingCompany))
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(n => n.Title));
                cfg.CreateMap<WebApiCreateIssueModel, Issue>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Newspaper, opt => opt.MapFrom(i => i.Newspaper))
                    .ForMember(dest => dest.Title, opt => opt.Ignore())
                    .ForMember(dest => dest.YearOfPublishing, opt => opt.Ignore())
                    .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                    .ForMember(dest => dest.CountOfPublishing, opt => opt.MapFrom(i => i.CountOfPublishing))
                    .ForMember(dest => dest.Commentary, opt => opt.MapFrom(i => i.Commentary))
                    .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(i => i.PagesCount))
                    .ForMember(dest => dest.DateOfPublishing, opt => opt.MapFrom(c => c.DateOfPublishing));
                cfg.CreateMap<WebApiEditIssueModel, Issue>()
                    .ForMember(dest => dest.Newspaper, opt => opt.Ignore())
                    .ForMember(dest => dest.Title, opt => opt.Ignore())
                    .ForMember(dest => dest.YearOfPublishing, opt => opt.Ignore())
                    .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                    .ForMember(dest => dest.DateOfPublishing, opt => opt.MapFrom(c => c.DateOfPublishing));
            });
            config.AssertConfigurationIsValid();
            return config;
        }
    }
}