using System;
using CourseLibrary.API.Entities;

namespace CourseLibrary.API.Services.ResourcesService
{
    public interface IResourcesRepository
    {
        void AddResources(Resource res);
        bool Save();
    }
}
