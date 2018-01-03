using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Core
{
    public class PublicConfig
    {
        private readonly int _paginationPZ;
        public PublicConfig(IConfiguration config)
        {
            var configuration = config;
            //todo serialaze config from json to object 
            _paginationPZ = Convert.ToInt32(configuration.GetSection("PublicConfig").GetSection("PaginationPageSize").Value);
        }        


        public int GetPageSize()
        {
            return _paginationPZ;
        }
    }
}
