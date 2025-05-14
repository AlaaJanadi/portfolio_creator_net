using JWTAuth.Dtos;
using JWTAuth.Entities;

namespace JWTAuth.Services;

public interface IPersonalInfoService
{
    Task<PersonalInfoCreateUpdateDto?> GetPersonalInfoAsync(Guid portfolioId);
    // create or update
    Task<PersonalInfoCreateUpdateDto?> UpdatePersonalInfoAsync(Guid portfolioId, PersonalInfoCreateUpdateDto req);
    
    
}