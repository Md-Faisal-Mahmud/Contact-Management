using Autofac;
using Contact_Management.Application.Services.Securities;
using Contact_Management.Application;
using Contact_Management.Infrastructure.Services.Securities;

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

            base.Load(builder);
        }
    }
}