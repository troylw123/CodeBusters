using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Assessment;

namespace CodeBusters.Services.Assessment
{
    public interface IAssessmentService
    {
        Task<bool> CreateAssessmentAsync(CreateAssessment model);
    }
}