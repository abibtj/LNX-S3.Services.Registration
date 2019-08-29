using AutoMapper;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<ParentAddress, ParentAddressDto>().ReverseMap();
            CreateMap<TeacherAddress, TeacherAddressDto>().ReverseMap();
            CreateMap<StudentAddress, StudentAddressDto>().ReverseMap();
            CreateMap<SchoolAddress, SchoolAddressDto>().ReverseMap();
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Parent, ParentDto>().ReverseMap();
            CreateMap<School, SchoolDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<ScoresEntryTask, ScoresEntryTaskDto>().ReverseMap();
        }
    }
}
