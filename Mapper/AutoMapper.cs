using AutoMapper;
using TwoFactorAuth.DataModels;
using TwoFactorAuth.TwoFactorAuthDto;

namespace TwoFactorAuth.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserRegisterDto, RegisterModel>().ReverseMap();
        }
    }
}
