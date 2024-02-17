using Autofac;
using Contact_MangeMent.API.Models.Auth;
using Contact_MangeMent.API.Models.Contact;

namespace Contact_MangeMent.API
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<ContactCreateModel>().AsSelf();
            builder.RegisterType<ContactListModel>().AsSelf();
            base.Load(builder);
        }
    }
}
