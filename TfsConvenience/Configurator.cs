using Autofac;

namespace TfsConvenience
{
    public static class Configurator
    {
        public static void ConfigureAutofac(ContainerBuilder builder)
        {
            builder.RegisterType<Query>()
                .As<IQuery>()
                .InstancePerDependency();

            builder.RegisterType<Connection>()
                .As<IConnection>()
                .InstancePerDependency();

        }
    }
}
