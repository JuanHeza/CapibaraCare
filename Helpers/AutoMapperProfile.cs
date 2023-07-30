namespace LifeCenter.Helpers;

using AutoMapper;
using LifeCenter.Entities;
using LifeCenter.Models.Personal;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> Personal
        CreateMap<CreateRequest, Personal>();

        // UpdateRequest -> Personal
        CreateMap<UpdateRequest, Personal>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    // ignore null role
                    if (x.DestinationMember.Name == "Rol" && src.Rol == null) return false;

                    return true;
                }
            ));
    }
}