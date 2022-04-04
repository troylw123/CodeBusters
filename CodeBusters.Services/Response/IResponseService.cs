using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Data.Entities;
using CodeBusters.Models.Responses;

namespace CodeBusters.Services.Response
{
    public interface IResponseService
    {
        Task<bool> CreateResponseAsync(ResponseCreate request);
        Task<IEnumerable<ResponseListItem>> GetAllResponsesAsync();
        Task<IEnumerable<ResponseListItem>> GetResponsesByAssessmentIdAsync(int assessmentId);
        Task<bool> UpdateResponseAsync(ResponseEntity request);
        Task<bool> DeleteResponseAsync(int id);
    }
}