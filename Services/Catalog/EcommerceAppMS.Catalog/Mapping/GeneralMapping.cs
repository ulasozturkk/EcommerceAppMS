using AutoMapper;
using EcommerceAppMS.Catalog.Dtos;
using EcommerceAppMS.Catalog.Models;


namespace EcommerceAppMS.Catalog.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();

        CreateMap<Course, CourseCreateDto>().ReverseMap();
        CreateMap<Course, UpdateCourseDto>().ReverseMap();
    }
}
