﻿using Autofac;
using Contact_Management.Application.Services.Securities;
using Contact_Management.Application;
using Contact_Management.Infrastructure.Services.Securities;
using Contact_Management.Infrastructure.Services.Auth;
using Contact_Management.Application.Services.Auth;
using Contact_Management.Infrastructure.Services.Contact;
using Contact_Management.Application.Services.Contact;

namespace Contact_Management.Infrastructure
{
    public class InfrastructureModule : Module
    {
        public InfrastructureModule()
        {
            
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenService>().As<ITokenService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ContactService>().As<IContactService>()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}