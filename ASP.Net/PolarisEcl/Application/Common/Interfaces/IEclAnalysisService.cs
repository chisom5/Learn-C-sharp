using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Common.Interfaces;

/*
 - Add new compuation
 - save model info progress
 - start computation
 - view report
 - view results (3 results.)
 - get all computations
*/
public interface IEclAnalysisService
{
    Task<NewComputationResponseDto> AddNewComputationAsync(AddNewComputationRequestDto request, User user);
    Task<string> SaveModelInformation(ModelInfoRequestDto request);

}