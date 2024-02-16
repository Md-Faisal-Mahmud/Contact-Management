using Autofac;
using Contact_MangeMent.API.Models;

namespace Contact_MangeMent.API
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            base.Load(builder);
        }
    }
}
