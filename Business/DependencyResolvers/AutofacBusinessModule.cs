using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra;
using Module = Autofac.Module;

namespace Business.DependencyResolvers;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CassLogRepository>().As<ILogRepository>().SingleInstance();
        builder.RegisterType<CassAdvStrategyBehaviorModelRepository>()
            .As<IAdvStrategyBehaviorModelRepository>().SingleInstance();
        builder.RegisterType<CassChurnPredictionMlResultRepository>()
            .As<IChurnPredictionMlResultRepository>().SingleInstance();
        builder.RegisterType<CassClientRepository>().As<IClientRepository>().SingleInstance();
        builder.RegisterType<CassOfferBehaviorModelRepository>()
            .As<IOfferBehaviorModelRepository>().SingleInstance();
        builder.RegisterType<CassGameSessionRepository>()
            .As<IGameSessionRepository>().SingleInstance();
        builder.RegisterType<CassOfferBehaviorModelRepository>()
            .As<IOfferBehaviorModelRepository>().SingleInstance();

        var assembly = Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance().InstancePerDependency();
    }
}