using System;
using Microsoft.Extensions.DependencyInjection;
using OnlineCourse.Entity;

namespace OnlineCourse.Core.Services
{
    public abstract class ServiceBase
    {
        protected IServiceProvider ServiceProvider { get; }

        protected IUnitOfWork UnitOfWorkProvider => ServiceProvider.GetService<IUnitOfWork>();

        protected ServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected IUnitOfWork CreateUnitOfWork()
        {
            return UnitOfWorkProvider;
        }


    }
}
