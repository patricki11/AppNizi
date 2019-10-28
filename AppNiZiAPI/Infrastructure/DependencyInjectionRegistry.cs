﻿using AppNiZiAPI.Models.Repositories;
using AppNiZiAPI.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppNiZiAPI.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IPatientRepository, PatientRepository>();
            services.AddSingleton<IAuthorizationRepository, AuthorizationRepository>();

            return services;
        }
    }
}
