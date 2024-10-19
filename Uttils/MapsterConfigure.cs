using DentalClinic.DataTransferObjects;
using DentalClinic.Models;
using Mapster;
using System.Reflection;

namespace DentalClinic.Uttils
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            // Configure Product to ProductDto mapping
            TypeAdapterConfig<User, UsersDto>.NewConfig()
                    .Map(dest => dest.Id, src => src.Id)
                    .Map(dest => dest.FirstName, src => src.FirstName)
                    .Map(dest => dest.SecondName, src => src.SecondName)
                    .Map(dest => dest.ThirdName, src => src.ThirdName)
                    .Map(dest => dest.FourthName, src => src.FourthName)
                                .Map(dest => dest.UserType, src => UserTypeMappings.GetDisplayName(src.UserType))  // Explicitly map UserType
                                .Ignore(dest => dest.CreatedAt)   // Handle nullability or dynamic value assignment
                                .Ignore(dest => dest.LastUpdatedAt)
                                .IgnoreNonMapped(true);



            // Configure Review to ReviewDto mapping
            TypeAdapterConfig<UsersDto, User>.NewConfig();
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        }
    }
}

