using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Models.Responses;

namespace CodeBusters.Services.Response
{
    public interface IResponseService
    {
        Task<bool> CreateResponseAsync(ResponseCreate request);
    }
}