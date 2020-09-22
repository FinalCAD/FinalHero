using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using API;
using API.Mappers;
using AutoMapper;
using AutoMapper.Configuration;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
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
    public class PowerServiceTest
    {
        private IPowerService Service { get; }

        private Mock<IPowerRepository> Repository { get; }

        private Mock<IHeroPowerService> HeroPowerService { get; }

        public PowerServiceTest()
        {
            try
            {
                var config = new MapperConfigurationExpression();
                //config.AddProfile<CityMapper>();
                config.AddProfile<HeroMapper>();
                config.AddProfile<PowerMapper>();

                AutoMapper.Mapper.Initialize(config);
            }
            catch
            {
            }

            Repository = new Mock<IPowerRepository>();

            HeroPowerService = new Mock<IHeroPowerService>();

            Service = new PowerService(Repository.Object, HeroPowerService.Object);
        }

        [Fact]
        public async void GetById()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => powers.FirstOrDefault(e => e.Id == id));

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
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => powers.FirstOrDefault(e => e.Id == id));

            var id = 5;

            //Act
            var res = await Service.GetByIdAsync(id);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetByName()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => powers.FirstOrDefault(e => e.Name == name));

            var powerName = "Super strength";

            //Act
            var res = await Service.GetByNameAsync(powerName);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(powerName, res.Name);
        }

        [Fact]
        public async void GetByNameUnknown()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => powers.FirstOrDefault(e => e.Name == name));

            var powerName = "Super test";

            //Act
            var res = await Service.GetByNameAsync(powerName);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetAll()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                },
                new Power
                {
                    Id = 2,
                    Name = "Heat vision",
                    Description = "Shoot laser beams from the eyes"
                },
                new Power
                {
                    Id = 3,
                    Name = "Flight",
                    Description = "Can fly through the air"
                },
                new Power
                {
                    Id = 4,
                    Name = "Batman",
                    Description = "Enough said"
                }
            };

            Repository.Setup(e => e.GetAllAsync())
                .ReturnsAsync(powers);

            //Act
            var res = Mapper.Map<List<Power>>((await Service.GetAllAsync()).Entities);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(res.Count, powers.Count);
        }

        [Fact]
        public async void Create()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => powers.FirstOrDefault(e => e.Name == name));

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => powers.FirstOrDefault(e => e.Id == id));

            Repository.Setup(e => e.InsertAsync(It.IsAny<Power>()))
                .Callback((Power entity) =>
                {
                    entity.Id = 2;
                    powers.Add(entity);
                });

            var entity = new PowerDTO()
            {
                Id = 0,
                Name = "Super Test",
                Description = ""
            };

            //Act
            var dto = await Service.Create(entity);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(2, powers.Count);          
        }

        [Fact]
        public async void CreateExisting()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => powers.FirstOrDefault(e => e.Name == name));

            var dto = new PowerDTO()
            {
                Id = 0,
                Name = "Super strength"
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<BadRequestException>(() => Service.Create(dto));

            // Assert
            Assert.Equal("Cannot create Power with name " + dto.Name + " because it already exists", ex.Message);
        }

        [Fact]
        public async void Update()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => powers.FirstOrDefault(e => e.Id == id));

            Repository.Setup(e => e.UpdateAsync(It.IsAny<Power>()))
                .Callback((Power entity) =>
                {
                    var power = powers.Find(c => c.Id == entity.Id);
                    power.Name = entity.Name;
                    power.Description = entity.Description;
                });

            var id = 1;
            var name = "Super test";
            var description = "Super strong test";

            //Act
            var dto = await Service.Update(id, name, description);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(name, dto.Name);
            Assert.Equal(description, dto.Description);
            Assert.Single(powers);
        }

        [Fact]
        public async void UpdateNonExisting()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => powers.FirstOrDefault(e => e.Id == id));

            var id = 5;
            var name = "Super test";
            var description = "";

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.Update(id, name, description));

            // Assert
            Assert.Equal("Cannot update nonexistant Power", ex.Message);
        }

        [Fact]
        public async void DeleteById()
        {
            //Arrange
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 1
                },
                new HeroPower
                {
                    Id = 2,
                    HeroId = 1,
                    PowerId = 2
                }
            };
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                },
                new Power
                {
                    Id = 2,
                    Name = "Super test",
                    Description = "Super strong test"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => powers.FirstOrDefault(e => e.Id == id));

            this.HeroPowerService.Setup(e => e.GetAllHeroPowerByPowerAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new HeroesPowersResponseDTO
                {
                    Entities = Mapper.Map<List<HeroPowerDTO>>(heropowers.ToList().Where(e => e.PowerId == id))
                });

            this.HeroPowerService.Setup(e => e.DeleteByIdBase(It.IsAny<int>()))
                .Callback((int id) =>
                {
                    heropowers.Remove(heropowers.Find(e => e.Id == id));
                });

            Repository.Setup(e => e.DeleteAsync(It.IsAny<Power>()))
                .Callback((Power entity) =>
                {
                    powers.Remove(entity);
                });

            var id = 2;

            //Act
            var dto = await Service.DeleteById(id);

            // Assert
            Assert.Null(dto);
            Assert.Single(powers);
            Assert.Single(heropowers);
        }

        [Fact]
        public async void DeleteByIdNonExisting()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => powers.FirstOrDefault(e => e.Id == id));

            var id = 5;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.DeleteById(id));

            // Assert
            Assert.Equal("Cannot delete Power with id " + id + " because not found", ex.Message);
        }

        [Fact]
        public async void DeleteByNameNonExisting()
        {
            //Arrange
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => powers.FirstOrDefault(e => e.Name == name));

            var name = "Void";

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.DeleteByName(name));

            // Assert
            Assert.Equal("Cannot delete Power with name " + name + " because not found", ex.Message);
        }
    }
}
