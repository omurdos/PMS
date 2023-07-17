using Core.Entities;

namespace Core.Repository.ApplicationCategoryRepository
{
    public interface IApplicationCategoryRepository
    {
        public Task<IEnumerable<ApplicationCategory>> GetApplicationCategories();
        public Task<ApplicationCategory> GetApplicationCategory(string id);
        public Task<ApplicationCategory> AddApplicationCategory(ApplicationCategory applicationCategory);
        public Task<ApplicationCategory> UpdateApplicationCategory(string id, ApplicationCategory applicationCategory);
        public Task<bool> DeleteApplicationCategory(string id);
        
    }
}
