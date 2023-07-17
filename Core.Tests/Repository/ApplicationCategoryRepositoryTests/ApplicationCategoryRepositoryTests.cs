using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Repository.ApplicationCategoryRepository;
using FluentAssertions;
using Xunit;
using System.Xml.Linq;

namespace Core.Tests.Repository.ApplicationCategoryRepositoryTests
{
    public class ApplicationCategoryRepositoryTests
    {


 

        [Fact]
        public async Task ApplicationCategoryRepository_AddApplicationCategory_ReturnApplicationCategoryWithId()
        {
            //Arrange
            var context = await GetDBContext();
            var applicationCategory = new ApplicationCategory()
            {
                Name = "shopping",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
            };
            var applicationCategoryRepository = new ApplicationCategoryRepository(context);
            //Act
            var result = await applicationCategoryRepository.AddApplicationCategory(applicationCategory);
            //Assert

            result.Name.Should().NotBeNullOrEmpty();
            result.Id.Should().NotBeNullOrEmpty();
            result.Id.Should().BeOfType<string>();
            
        }

        [Fact]
        public async Task ApplicationCategoryRepository_DeleteApplicationCategory_ReturnBool()
        {
            //Arrange
            var context = await GetDBContext();
            var applicationCategory = new ApplicationCategory()
            {
                Name = "shopping",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
            };
            var applicationCategoryRepository = new ApplicationCategoryRepository(context);
            //Act
            await applicationCategoryRepository.AddApplicationCategory(applicationCategory);
            var result = await applicationCategoryRepository.DeleteApplicationCategory(applicationCategory.Id);
            //Assert
            result.Should().Be(true);
        }
        [Fact]
        public async Task ApplicationCategoryRepository_GetApplicationCategories_ReturnIEnumberable()
        {
            //Arrange
            var context = await GetDBContext();
           
            var applicationCategoryRepository = new ApplicationCategoryRepository(context);
            //Act
           
            var result = await applicationCategoryRepository.GetApplicationCategories();
            //Assert
            result.Should().BeOfType<List<ApplicationCategory>>();
        }

        public Task<ApplicationCategory> GetApplicationCategory(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationCategory> UpdateApplicationCategory(string id, ApplicationCategory applicationCategory)
        {
            throw new NotImplementedException();
        }

        private async Task<DBContext> GetDBContext()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var _dbContext = new DBContext(options);
            _dbContext.Database.EnsureCreated();

            if (await _dbContext.ApplicaionCategories.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    _dbContext.ApplicaionCategories.Add(
                      new ApplicationCategory()
                      {
                          Id = i.ToString(),
                          Name = i.ToString(),
                          CreatedAt = DateTime.Now,
                          ModifiedAt = DateTime.Now,
                      });
                    await _dbContext.SaveChangesAsync();
                }
            }



            return _dbContext;
        }

    }
}
