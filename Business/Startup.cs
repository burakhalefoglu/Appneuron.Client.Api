using Autofac;
using AutoMapper;
using Business.Constants;
using Business.DependencyResolvers;
using Business.Fakes.DArch;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.MongoDb;
using DataAccess.Concrete.MongoDb.Collections;
using DataAccess.Concrete.MongoDb.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;


namespace Business
{
    public partial class BusinessStartup
    {
        protected readonly IHostEnvironment HostEnvironment;


        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>

        public virtual void ConfigureServices(IServiceCollection services)
        {


            Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>

                            sp.GetService<IHttpContextAccessor>().HttpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));

            services.AddScoped<IPrincipal>(getPrincipal);
            services.AddMemoryCache();


            services.AddDependencyResolvers(Configuration, new ICoreModule[]
            {
                    new CoreModule()
            });


            services.AddSingleton<ConfigurationManager>();


            services.AddTransient<IElasticSearch, ElasticSearchManager>();

            services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            services.AddAutoMapper(typeof(ConfigurationManager));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()?.GetName();
            };

        }

        /// <summary>
        /// This method gets called by the Development
        /// </summary>
        /// <param name="services"></param> 
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {

            ConfigureServices(services);
            services.AddTransient<IProjectBaseBuyingCountWithDifficultyRepository>(x=> new ProjectBaseBuyingCountWithDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseBuyingCountWithDifficulties));
            services.AddTransient<IProjectBaseSuccessAttemptRateRepository>(x=> new ProjectBaseSuccessAttemptRateRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseSuccessAttemptRates));
            services.AddTransient<IProjectBaseTotalDieWithDifficultyRepository>(x=> new ProjectBaseTotalDieWithDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseTotalDieWithDifficulties));
            services.AddTransient<IProjectBaseAdvClickRepository>(x=> new ProjectBaseAdvClickRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseAdvClicks));
            services.AddTransient<IProjectBasePowerUsageByDifficultyRepository>(x=> new ProjectBasePowerUsageByDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBasePowerUsageByDifficulties));
            services.AddTransient<IPlayerListByDayRepository>(x=> new PlayerListByDayRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayerListByDays));
            services.AddTransient<IChallengeBasedSegmentationRepository>(x=> new ChallengeBasedSegmentationRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ChallengeBasedSegmentations));
            services.AddTransient<IPlayersOnDifficultyLevelRepository>(x=> new PlayersOnDifficultyLevelRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayersOnDifficultyLevels));
            services.AddTransient<IPlayersOnLevelRepository>(x=> new PlayersOnLevelRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayersOnLevels));
            services.AddTransient<IRevenueRepository>(x=> new RevenueRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.Revenues));
            services.AddTransient<IStatisticsByNumberRepository>(x=> new StatisticsByNumberRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.StatisticsByNumbers));
            services.AddTransient<ILevelBaseSessionDataRepository>(x=> new LevelBaseSessionDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.LevelBaseSessionDatas));
            services.AddTransient<IGameSessionEveryLoginDataRepository>(x=> new GameSessionEveryLoginDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.GameSessionEveryLoginDatas));
            services.AddTransient<IDailySessionDataRepository>(x=> new DailySessionDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.DailySessionDatas));
            services.AddTransient<ILevelBaseDieDataRepository>(x=> new LevelBaseDieDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.LevelBaseDieDatas));
            services.AddTransient<IEveryLoginLevelDataRepository>(x=> new EveryLoginLevelDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.EveryLoginLevelDatas));
            services.AddTransient<IGeneralDataRepository>(x=> new GeneralDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.GeneralDatas));
            services.AddTransient<IBuyingEventRepository>(x=> new BuyingEventRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.BuyingEvents));
            services.AddTransient<IAdvEventRepository>(x=> new AdvEventRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.AdvEvents));
            services.AddTransient<ITestRepository>(x=> new TestRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.Tests));
            services.AddTransient<ILogRepository, LogRepository>();

            services.AddDbContext<ProjectDbContext, DArchInMemory>(ServiceLifetime.Transient);
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();



        }
        /// <summary>		
        /// This method gets called by the Staging
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IProjectBaseBuyingCountWithDifficultyRepository>(x=> new ProjectBaseBuyingCountWithDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseBuyingCountWithDifficulties));
            services.AddTransient<IProjectBaseSuccessAttemptRateRepository>(x=> new ProjectBaseSuccessAttemptRateRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseSuccessAttemptRates));
            services.AddTransient<IProjectBaseTotalDieWithDifficultyRepository>(x=> new ProjectBaseTotalDieWithDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseTotalDieWithDifficulties));
            services.AddTransient<IProjectBaseAdvClickRepository>(x=> new ProjectBaseAdvClickRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseAdvClicks));
            services.AddTransient<IProjectBasePowerUsageByDifficultyRepository>(x=> new ProjectBasePowerUsageByDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBasePowerUsageByDifficulties));
            services.AddTransient<IPlayerListByDayRepository>(x=> new PlayerListByDayRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayerListByDays));
            services.AddTransient<IChallengeBasedSegmentationRepository>(x=> new ChallengeBasedSegmentationRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ChallengeBasedSegmentations));
            services.AddTransient<IPlayersOnDifficultyLevelRepository>(x=> new PlayersOnDifficultyLevelRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayersOnDifficultyLevels));
            services.AddTransient<IPlayersOnLevelRepository>(x=> new PlayersOnLevelRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayersOnLevels));
            services.AddTransient<IRevenueRepository>(x=> new RevenueRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.Revenues));
            services.AddTransient<IStatisticsByNumberRepository>(x=> new StatisticsByNumberRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.StatisticsByNumbers));
            services.AddTransient<ILevelBaseSessionDataRepository>(x=> new LevelBaseSessionDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.LevelBaseSessionDatas));
            services.AddTransient<IGameSessionEveryLoginDataRepository>(x=> new GameSessionEveryLoginDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.GameSessionEveryLoginDatas));
            services.AddTransient<IDailySessionDataRepository>(x=> new DailySessionDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.DailySessionDatas));
            services.AddTransient<ILevelBaseDieDataRepository>(x=> new LevelBaseDieDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.LevelBaseDieDatas));
            services.AddTransient<IEveryLoginLevelDataRepository>(x=> new EveryLoginLevelDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.EveryLoginLevelDatas));
            services.AddTransient<IGeneralDataRepository>(x=> new GeneralDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.GeneralDatas));
            services.AddTransient<IBuyingEventRepository>(x=> new BuyingEventRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.BuyingEvents));
            services.AddTransient<IAdvEventRepository>(x=> new AdvEventRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.AdvEvents));
            services.AddTransient<ITestRepository>(x=> new TestRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.Tests));
            services.AddTransient<ILogRepository, LogRepository>();

            services.AddDbContext<ProjectDbContext>();

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();


        }

        /// <summary>
        /// This method gets called by the Production
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IProjectBaseBuyingCountWithDifficultyRepository>(x=> new ProjectBaseBuyingCountWithDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseBuyingCountWithDifficulties));
            services.AddTransient<IProjectBaseSuccessAttemptRateRepository>(x=> new ProjectBaseSuccessAttemptRateRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseSuccessAttemptRates));
            services.AddTransient<IProjectBaseTotalDieWithDifficultyRepository>(x=> new ProjectBaseTotalDieWithDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseTotalDieWithDifficulties));
            services.AddTransient<IProjectBaseAdvClickRepository>(x=> new ProjectBaseAdvClickRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBaseAdvClicks));
            services.AddTransient<IProjectBasePowerUsageByDifficultyRepository>(x=> new ProjectBasePowerUsageByDifficultyRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ProjectBasePowerUsageByDifficulties));
            services.AddTransient<IPlayerListByDayRepository>(x=> new PlayerListByDayRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayerListByDays));
            services.AddTransient<IChallengeBasedSegmentationRepository>(x=> new ChallengeBasedSegmentationRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.ChallengeBasedSegmentations));
            services.AddTransient<IPlayersOnDifficultyLevelRepository>(x=> new PlayersOnDifficultyLevelRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayersOnDifficultyLevels));
            services.AddTransient<IPlayersOnLevelRepository>(x=> new PlayersOnLevelRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.PlayersOnLevels));
            services.AddTransient<IRevenueRepository>(x=> new RevenueRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.Revenues));
            services.AddTransient<IStatisticsByNumberRepository>(x=> new StatisticsByNumberRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.StatisticsByNumbers));
            services.AddTransient<ILevelBaseSessionDataRepository>(x=> new LevelBaseSessionDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.LevelBaseSessionDatas));
            services.AddTransient<IGameSessionEveryLoginDataRepository>(x=> new GameSessionEveryLoginDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.GameSessionEveryLoginDatas));
            services.AddTransient<IDailySessionDataRepository>(x=> new DailySessionDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.DailySessionDatas));
            services.AddTransient<ILevelBaseDieDataRepository>(x=> new LevelBaseDieDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.LevelBaseDieDatas));
            services.AddTransient<IEveryLoginLevelDataRepository>(x=> new EveryLoginLevelDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.EveryLoginLevelDatas));
            services.AddTransient<IGeneralDataRepository>(x=> new GeneralDataRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.GeneralDatas));
            services.AddTransient<IBuyingEventRepository>(x=> new BuyingEventRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.BuyingEvents));
            services.AddTransient<IAdvEventRepository>(x=> new AdvEventRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.AdvEvents));
            services.AddTransient<ITestRepository>(x=> new TestRepository(x.GetRequiredService<MongoDbContextBase>(), Collections.Tests));
            services.AddTransient<ILogRepository, LogRepository>();

            services.AddDbContext<ProjectDbContext>();

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();

        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));

        }

    }
}
