using Microsoft.EntityFrameworkCore;
using RCBACEF.Models;

namespace RCBACEF.QueryOptions
{
    public class BaseQueryOptions : QueryOptions
    {
        public bool IncludeCreatedBy { get; set; } = false;
    }
}
