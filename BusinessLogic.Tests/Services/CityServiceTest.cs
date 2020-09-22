using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using API;
using API.Mappers;
using AutoMapper;
using AutoMapper.Configuration;
using BusinessLogic.Exceptions;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace BusinessLogic.Tests.Services
{
    [Collection("Service")]
    public class CityServiceTest
    {

        private ICityService Service { get; }

        private Mock<ICityRepository> Repository { get; }

        public CityServiceTest()
        {
            try
            {
                var config = new MapperConfigurationExpression();
                config.AddProfile<CityMapper>();
                //config.AddProfile<HeroMapper>();
                //config.AddProfile<PowerMapper>();

                AutoMapper.Mapper.Initialize(config);
            }
            catch
            {
            }

            Repository = new Mock<ICityRepository>();
            Service = new CityService(Repository.Object);
        }

        [Fact]
        public async void GetById()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => cities.FirstOrDefault(e => e.Id == id));

            var id = 1;

            //Act
            var res = await Service.GetByIdAsync(id);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(id, res.Id);
        }

        [Fact]
        public async void GetByIdUnknown()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                }               
            };
            
            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => cities.FirstOrDefault(e => e.Id == id));
            
            var id = 4;

            //Act
            var res = await Service.GetByIdAsync(id);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetByName()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => cities.FirstOrDefault(e => e.Name == name));

            var cityName = "London";

            //Act
            var res = await Service.GetByNameAsync(cityName);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(cityName, res.Name);
        }

        [Fact]
        public async void GetByNameUnknown()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => cities.FirstOrDefault(e => e.Name == name));

            var cityName = "Paris";

            //Act
            var res = await Service.GetByNameAsync(cityName);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetAll()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                },
                new City
                {
                    Id = 2,
                    Name = "Gotham"
                },
                new City
                {
                    Id = 3,
                    Name = "Metropolis"
                }
            };

            Repository.Setup(e => e.GetAllAsync())
                .ReturnsAsync(cities);

            //Act
            var res = Mapper.Map<List<City>>((await Service.GetAllAsync()).Entities);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(res.Count, cities.Count);
        }

        [Fact]
        public async void Create()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                }
            };            

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => cities.FirstOrDefault(e => e.Name == name));

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => cities.FirstOrDefault(e => e.Id == id));

            Repository.Setup(e => e.InsertAsync(It.IsAny<City>()))
                .Callback((City entity) => 
                {
                    entity.Id = 2;
                    cities.Add(entity);
                });

            var name = "Gotham";

            //Act
            var dto = await Service.Create(name);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(name, dto.Name);
            Assert.Equal(2, cities.Count);
        }

        [Fact]
        public async void CreateExisting()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => cities.FirstOrDefault(e => e.Name == name));

            var name = "London";

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<BadRequestException>(() => Service.Create(name));

            // Assert
            Assert.Equal("Cannot create City because it already exists", ex.Message);
        }

        [Fact]
        public async void Update()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => cities.FirstOrDefault(e => e.Id == id));

            Repository.Setup(e => e.UpdateAsync(It.IsAny<City>()))
                .Callback((City entity) =>
                {
                    cities.Find(c => c.Id == entity.Id).Name = entity.Name;
                });

            var id = 1;
            var name = "Paris";

            //Act
            var dto = await Service.Update(id, name);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(name, dto.Name);
            Assert.Single(cities);
        }

        [Fact]
        public async void UpdateNonExisting()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => cities.FirstOrDefault(e => e.Id == id));

            var id = 5;
            var name = "Paris";

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.Update(id,name));

            // Assert
            Assert.Equal("Cannot update nonexistant City", ex.Message);
        }

        [Fact]
        public async void DeleteById()
        {
            //Arrange
            var wonder = new Hero
            {
                Id = 2,
                Name = "Wonder Woman",
                CityId = 1
            };
            var heroes = new List<Hero>
            {
                wonder
            };
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London",
                    Heroes = heroes
                }
            };

            Repository.Setup(e => e.GetCityWithHeroesAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => cities.FirstOrDefault(e => e.Id == id));

            Repository.Setup(e => e.DeleteAsync(It.IsAny<City>()))
                .Callback((City entity) =>
                {
                    entity.Heroes.ForEach(e => e.CityId = null);
                    cities.Remove(entity);
                });

            var id = 1;

            //Act
            var dto = await Service.DeleteById(id);

            // Assert
            Assert.Null(dto);
            Assert.Empty(cities);
            Assert.Null(wonder.CityId);
        }

        [Fact]
        public async void DeleteByIdNonExisting()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London",
                    Heroes = new List<Hero>
                    {
                        new Hero
                        {
                            Id = 2,
                            Name = "Wonder Woman",
                            CityId = 1
                        }
                    }
                }
            };

            Repository.Setup(e => e.GetCityWithHeroesAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => cities.FirstOrDefault(e => e.Id == id));

            var id = 5;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.DeleteById(id));

            // Assert
            Assert.Equal("Cannot delete City with id " + id + " because not found", ex.Message);
        }

        [Fact]
        public async void DeleteByNameNonExisting()
        {
            //Arrange
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "London",
                    Heroes = new List<Hero>
                    {
                        new Hero
                        {
                            Id = 2,
                            Name = "Wonder Woman",
                            CityId = 1
                        }
                    }
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => cities.FirstOrDefault(e => e.Name == name));

            var name = "Void";

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.DeleteByName(name));

            // Assert
            Assert.Equal("Cannot delete City with name " + name + " because not found", ex.Message);
        }
    }
}
