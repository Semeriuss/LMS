using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using System;

namespace CourseLibrary.API.Services.ResourcesService
{
    public class ResourceRepository : IResourcesRepository, IDisposable
    {
        private readonly CourseLibraryContext _context;

        public ResourceRepository(CourseLibraryContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddResources(Resource res)
        {
            _context.Resources.Add(res);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
               // dispose resources when needed
            }
        }

    }
}
