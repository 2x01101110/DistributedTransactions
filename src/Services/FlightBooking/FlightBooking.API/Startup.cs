using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlightBooking.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMasstransit();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
