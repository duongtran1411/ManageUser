using AutoMapper;
using BLL_User.Model;
using DAL_User;

namespace BLL_User.BUS
{
    public class UserProfile : Profile
    {
        /*public UserProfile() {
            CreateMap<User, UserDTO>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.user_name))
                .ForMember(des => des.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(des => des.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(des => des.Phone, opt => opt.MapFrom(src => src.phone))
                .ForMember(des => des.CreatedTime, opt => opt.MapFrom(src => src.created_time))
                .ReverseMap();
        }*/
    }
}
