using JWTAuth.Dtos;
using JWTAuth.Entities;

namespace JWTAuth.Services;

public interface IEductionService
{
    Task<List<EducationDetailsDto>?> GetAllEducations(Guid portfolioId);
    Task<EducationDetailsDto?> GetEducation(Guid portfolioId, Guid educationId);
    Task<EducationDetailsDto> AddEducation(Guid portfolioId, EducationCreateUpdateDto req);
    Task<EducationDetailsDto> UpdateEducation(Guid portfolioId, Guid educationId, EducationCreateUpdateDto req);
    Task<bool> DeleteEducation(Guid portfolioId, Guid educationId);
}