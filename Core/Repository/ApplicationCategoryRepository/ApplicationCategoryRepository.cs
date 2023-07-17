using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.ApplicationCategoryRepository
{
    public class ApplicationCategoryRepository : IApplicationCategoryRepository
    {
        private readonly DBContext _dBContext;

        public ApplicationCategoryRepository(DBContext dBContext)
        {
            _dBContext=dBContext;
        }

        public async Task<ApplicationCategory> AddApplicationCategory(ApplicationCategory applicationCategory)
        {
            await _dBContext.AddAsync(applicationCategory);
            var result = await _dBContext.SaveChangesAsync();
            return result > 0 ? applicationCategory : null;
        }

        public async Task<bool> DeleteApplicationCategory(string id)
        {
            var applicationCategory = await _dBContext.ApplicaionCategories.FindAsync(id);
            if (applicationCategory != null)
            {
                _dBContext.ApplicaionCategories.Remove(applicationCategory);
                var result = _dBContext.SaveChanges();
                return result > 0;
            }
            return false;
        }

        public async Task<IEnumerable<ApplicationCategory>> GetApplicationCategories()
        {
            return await _dBContext.ApplicaionCategories.ToListAsync();
        }

        public async Task<ApplicationCategory> GetApplicationCategory(string id)
        {
            return await _dBContext.ApplicaionCategories.FindAsync(id);
        }

        public async Task<ApplicationCategory> UpdateApplicationCategory(string id, ApplicationCategory applicationCategory)
        {
            ApplicationCategory applicationCategoryToUpdate = await _dBContext.ApplicaionCategories.FindAsync(id);
            if (applicationCategoryToUpdate != null)
            {
                applicationCategoryToUpdate.Name = applicationCategory.Name;
                _dBContext.ApplicaionCategories.Update(applicationCategoryToUpdate);
                var result = _dBContext.SaveChanges();
                return result > 0 ? applicationCategoryToUpdate : null;
            }
            return null;
        }
    }
}
