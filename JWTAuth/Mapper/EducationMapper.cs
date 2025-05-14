using JWTAuth.Dtos;
using JWTAuth.Entities;

namespace JWTAuth.Mapper;

public static class EducationMapper
{
    public static EducationDetailsDto From(this Education education)
    {
        return new EducationDetailsDto()
        {
            SchoolName = education.SchoolName,
            Description = education.Description,
            StartDate = education.StartDate,
            EndDate = education.EndDate,
            IsCurrent = education.IsCurrent,
            CertificateUrl = education.CertificateUrl,
            Details = education.Details
        };
    }

    public static Education To(this EducationCreateUpdateDto req)
    {
        return new Education()
        {
            SchoolName = req.SchoolName,
            Description = req.Description,
            StartDate = req.StartDate,
            EndDate = req.EndDate,
            IsCurrent = req.IsCurrent,
            CertificateUrl = req.CertificateUrl,
            Details = req.Details
        };
    }
}