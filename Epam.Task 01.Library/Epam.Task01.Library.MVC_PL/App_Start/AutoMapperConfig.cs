using System;
using System.Linq;
using AutoMapper;
using Epam.Task01.Library.Entity;
using Epam.Task01.Library.MVC_PL.Models;
using Epam.Task01.Library.MVC_PL.ViewModels;
using Epam.Task01.Library.MVC_PL.ViewModels.Authors;
using Epam.Task01.Library.MVC_PL.ViewModels.Books;
using Epam.Task01.Library.MVC_PL.ViewModels.Issues;
using Epam.Task01.Library.MVC_PL.ViewModels.Newspapers;
using Epam.Task01.Library.MVC_PL.ViewModels.Patents;
using Epam.Task01.Library.MVC_PL.ViewModels.User;
using Ninject.Modules;

namespace Epam.Task01.Library.MVC_PL
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
                cfg.CreateMap<Book, DisplayLibraryItemModel>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom((c, d) =>
                    {
                        bool manyauthors = c.Authors.Count > 0;
                        string result = c.ToString();
                        if (manyauthors)
                        {
                            result += " — ";
                        }

                        result += $"{c.Title} ({c.YearOfPublishing})";
                        return result;
                    }))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Isbn))
                    .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(c => c.PagesCount))
                    .ForMember(dest => dest.PrimaryKey, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Deleted, opt => opt.MapFrom(c => c.Deleted))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(c => c.GetType().Name));
                cfg.CreateMap<Issue, DisplayLibraryItemModel>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(c => c.CountOfPublishing == null ? $"{c.Title} {c.YearOfPublishing}" :
                    $"{c.Title} №{c.CountOfPublishing}/{c.YearOfPublishing}"))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.Newspaper.Issn))
                    .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(c => c.PagesCount))
                    .ForMember(dest => dest.PrimaryKey, opt => opt.MapFrom(c => c.Id))
                    .ForMember(dest => dest.Deleted, opt => opt.MapFrom(c => c.Deleted))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(c => c.GetType().Name));
                cfg.CreateMap<Patent, DisplayLibraryItemModel>()
                     .ForMember(dest => dest.Title, opt => opt.MapFrom(c => $"«{c.Title}» {c.PublicationDate.ToString("dd.MM.yyyy")}"))
                     .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.RegistrationNumber))
                     .ForMember(dest => dest.PagesCount, opt => opt.MapFrom(c => c.PagesCount))
                     .ForMember(dest => dest.PrimaryKey, opt => opt.MapFrom(c => c.Id))
                     .ForMember(dest => dest.Deleted, opt => opt.MapFrom(c => c.Deleted))
                     .ForMember(dest => dest.Type, opt => opt.MapFrom(c => c.GetType().Name));
                cfg.CreateMap<DisplayAuthorModel, Author>();
                cfg.CreateMap<Author, DisplayAuthorModel>();
                cfg.CreateMap<CreateAuthorModel, Author>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<CreateBookModel, Book>()
                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(c => c.Authors))
                    .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<Book, DisplayBookModel>();
                cfg.CreateMap<Book, EditBookModel>()
                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(c => c.Authors))
                    .ForMember(dest => dest.AuthorsId, opt => opt.Ignore())
                    .ForMember(dest => dest.AuthorsSelectList, opt => opt.Ignore());
                cfg.CreateMap<EditBookModel, Book>()
                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(c => c.Authors))
                    .ForMember(dest => dest.Deleted, opt => opt.Ignore());
                cfg.CreateMap<Patent, DisplayPatentModel>()
                    .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(c => c.ApplicationDate))
                    .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(c => c.PublicationDate))
                    .ForMember(dest => dest.YearOfPublishing, opt => opt.MapFrom(c => c.YearOfPublishing));
                cfg.CreateMap<CreatePatentModel, Patent>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(c => c.Authors))
                    .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(c => c.ApplicationDate))
                    .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(c => c.PublicationDate))
                    .ForMember(dest => dest.YearOfPublishing, opt => opt.Ignore());
                cfg.CreateMap<Patent, EditPatentModel>()
                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(c => c.Authors))
                    .ForMember(dest => dest.AuthorsId, opt => opt.Ignore())
                    .ForMember(dest => dest.AuthorsSelectList, opt => opt.Ignore());
                cfg.CreateMap<EditPatentModel, Patent>()
                    .ForMember(dest => dest.Authors, opt => opt.MapFrom(c => c.Authors))
                    .ForMember(dest => dest.YearOfPublishing, opt => opt.Ignore())
                    .ForMember(dest => dest.Deleted, opt => opt.Ignore());
                cfg.CreateMap<Issue, DisplayIssueModel>()
                    .ForMember(dest => dest.DateOfPublishing, opt => opt.MapFrom(c => c.DateOfPublishing));
                cfg.CreateMap<CreateIssueModel, Issue>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Newspaper, opt => opt.MapFrom(c => c.Newspaper))
                    .ForMember(dest => dest.Title, opt => opt.Ignore())
                    .ForMember(dest => dest.YearOfPublishing, opt => opt.Ignore())
                    .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                    .ForMember(dest => dest.DateOfPublishing, opt => opt.MapFrom(c => c.DateOfPublishing));
                cfg.CreateMap<Issue, EditIssueModel>()
                    .ForMember(dest => dest.Newspaper, opt => opt.MapFrom(c => c.Newspaper))
                    .ForMember(dest => dest.NewspaperId, opt => opt.MapFrom(c => c.Newspaper.Id))
                    .ForMember(dest => dest.DateOfPublishing, opt => opt.MapFrom(c => c.DateOfPublishing))
                    .ForMember(dest => dest.NewspaperSelectList, opt => opt.Ignore());
               cfg.CreateMap<EditIssueModel, Issue>()
                    .ForMember(dest => dest.Newspaper, opt => opt.MapFrom(c => c.Newspaper))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(c => c.Newspaper.Title))
                    .ForMember(dest => dest.YearOfPublishing, opt => opt.Ignore())
                    .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                    .ForMember(dest => dest.DateOfPublishing, opt => opt.MapFrom(c => c.DateOfPublishing));
                cfg.CreateMap<Newspaper, DisplayNewspaperModel>();
                cfg.CreateMap<CreateNewspaperModel, Newspaper>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()); 
                cfg.CreateMap<Issue, IssuePublicationModel>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(c => c.Newspaper.Title));
                cfg.CreateMap<SignUpModel, User>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Roles, opt => opt.Ignore()); 
                cfg.CreateMap<User, UserDetailsModel>();
                cfg.CreateMap<User, UserEditModel>()
                .ForMember(dest => dest.RolesId, opt => opt.Ignore());
            });
            config.AssertConfigurationIsValid();
            return config;
        }
    }
}