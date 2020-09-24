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
using Moq;
using Xunit;

namespace BusinessLogic.Tests.Services
{
    [Collection("Service")]
    public class HeroServiceTest
    {
        private IHeroService Service { get; }

        private Mock<IHeroRepository> Repository { get; }

        private Mock<IHeroPowerService> HeroPowerService { get; }

        private Mock<ICityService> CityService { get; }

        private Mock<IPowerService> PowerService { get; }

        public HeroServiceTest()
        {
            try
            {
                var config = new MapperConfigurationExpression();
                config.AddProfile<CityMapper>();
                config.AddProfile<HeroMapper>();
                config.AddProfile<PowerMapper>();

                AutoMapper.Mapper.Initialize(config);
            }
            catch
            {
            }

            Repository = new Mock<IHeroRepository>();
            HeroPowerService = new Mock<IHeroPowerService>();
            CityService = new Mock<ICityService>();
            PowerService = new Mock<IPowerService>();

            Service = new HeroService(Repository.Object, HeroPowerService.Object, CityService.Object, PowerService.Object);
        }

        #region HeroService

        [Fact]
        public async void GetById()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

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
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            var id = 4;

            //Act
            var res = await Service.GetByIdAsync(id);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetByIdDetailed()
        {
            //Arrange
            var gotham = new City
            {
                Id = 2,
                Name = "Gotham"
            };
            var cities = new List<City>
            {
                gotham
            };
            var power = new Power
            {
                Id = 4,
                Name = "Batman",
                Description = "Enough said"
            };
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 4,
                    Power = power
                }
            };
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2,
                    City = gotham
                }
            };

            Repository.Setup(e => e.GetHeroWithCityAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.HeroPowerService.Setup(e => e.GetAllByHeroIdWithPowerAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heropowers.FindAll(e => e.HeroId == id));

            var id = 1;

            //Act
            var res = await Service.GetByIdDetailedAsync(id);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(id, res.Id);
            Assert.Equal(gotham.Id, res.City.Id);
            Assert.Single(res.Powers);
        }

        [Fact]
        public async void GetByIdDetailedUnknown()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2,
                }
            };

            Repository.Setup(e => e.GetHeroWithCityAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            var id = 4;

            //Act
            var res = await Service.GetByIdDetailedAsync(id);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetByName()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => heroes.FirstOrDefault(e => e.Name == name));

            var name = "Batman";

            //Act
            var res = await Service.GetByNameAsync(name);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(name, res.Name);
        }

        [Fact]
        public async void GetByNameUnknown()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => heroes.FirstOrDefault(e => e.Name == name));

            var name = "Test Man";

            //Act
            var res = await Service.GetByNameAsync(name);

            //Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetByNameDetailedUnknown()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => heroes.FirstOrDefault(e => e.Name == name));

            var name = "Test Man";

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.GetByNameDetailedAsync(name));

            //Assert
            Assert.Equal("Hero with name " + name + " not found", ex.Message);
            
        }

        [Fact]
        public async void GetAllHeroesByCity()
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
                }
            };
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                },
                new Hero
                {
                    Id = 2,
                    Name = "Test Man",
                    CityId = 1
                }
            };

            this.CityService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<CityDTO>(cities.FirstOrDefault(e => e.Id == id)));

            Repository.Setup(e => e.GetAllHeroesByCityAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FindAll(e => e.CityId == id));

            int city_id = 2;

            //Act
            var res = await Service.GetAllHeroesByCityAsync(city_id);

            //Assert
            Assert.NotNull(res);
            Assert.Single(res.Entities);
            Assert.NotNull(res.Entities.Find(e => e.Name == "Batman"));
        }

        [Fact]
        public async void GetAllHeroesByCityUnknown()
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
                }
            };

            this.CityService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<CityDTO>(cities.FirstOrDefault(e => e.Id == id)));

            int city_id = 3;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.GetAllHeroesByCityAsync(city_id));

            //Assert
            Assert.Equal("City with id " + city_id + " not found", ex.Message);
            
            
        }

        [Fact]
        public async void GetAll()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                },
                new Hero
                {
                    Id = 2,
                    Name = "Wonder Woman",
                    CityId = 1
                },
                new Hero
                {
                    Id = 3,
                    Name = "Superman",
                    CityId = 3
                }
            };

            Repository.Setup(e => e.GetAllAsync())
                .ReturnsAsync(heroes);

            //Act
            var res = Mapper.Map<List<HeroDTO>>((await Service.GetAllAsync()).Entities);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(res.Count, heroes.Count);
        }

        [Fact]
        public async void GetAllDetailed()
        {
            //Arrange
            var gotham = new City
            {
                Id = 2,
                Name = "Gotham"
            };
            var cities = new List<City>
            {
                gotham
            };
            var power = new Power
            {
                Id = 4,
                Name = "Batman",
                Description = "Enough said"
            };
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 4,
                    Power = power
                }
            };
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2,
                    City = gotham
                }
            };

            Repository.Setup(e => e.GetAllAsync())
                .ReturnsAsync(() => heroes);


            Repository.Setup(e => e.GetHeroWithCityAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.HeroPowerService.Setup(e => e.GetAllByHeroIdWithPowerAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heropowers.FindAll(e => e.HeroId == id));

            //Act
            var res = await Service.GetAllDetailedAsync();

            //Assert
            Assert.NotNull(res);
            Assert.Single(res.Entities);
        }

        [Fact]
        public async void Create()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => heroes.FirstOrDefault(e => e.Name == name));

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            Repository.Setup(e => e.InsertAsync(It.IsAny<Hero>()))
                .Callback((Hero entity) =>
                {
                    entity.Id = 2;
                    heroes.Add(entity);
                });

            var wonder = new HeroDTO
            {
                Id = 0,
                Name = "Wonder Woman",
                CityId = 1
            };

            //Act
            var dto = await Service.Create(wonder);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(wonder.Name, dto.Name);
            Assert.Equal(2, heroes.Count);
        }

        [Fact]
        public async void CreateExisting()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => heroes.FirstOrDefault(e => e.Name == name));

            var batman = new HeroDTO
            {
                Id = 0,
                Name = "Batman",
                CityId = 2
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<BadRequestException>(() => Service.Create(batman));

            // Assert
            Assert.Equal("Cannot create Hero with name "+ batman.Name +" because it already exists", ex.Message);
        }

        [Fact]
        public async void Update()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            Repository.Setup(e => e.UpdateAsync(It.IsAny<Hero>()))
                .Callback((Hero entity) =>
                {
                    var e = heroes.Find(c => c.Id == entity.Id);
                    e.Name = entity.Name;
                    e.CityId = entity.CityId;
                });

            var id = 1;
            var name = "Batwoman";
            var cityid = 1;

            //Act
            var dto = await Service.Update(id, name, cityid);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(name, dto.Name);
            Assert.Equal(cityid, dto.CityId);
            Assert.Single(heroes);
        }

        [Fact]
        public async void UpdateNonExisting()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            var id = 4;
            var name = "Test Man";
            var cityid = 3;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.Update(id, name, cityid));

            // Assert
            Assert.Equal("Cannot update nonexistant Hero", ex.Message);
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
                    HeroId = 2,
                    PowerId = 2
                }
            };
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                },
                new Hero
                {
                    Id = 2,
                    Name = "Test Man",
                    CityId = 1
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.HeroPowerService.Setup(e => e.GetAllHeroPowerByHeroAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new HeroesPowersResponseDTO
                {
                    Entities = Mapper.Map<List<HeroPowerDTO>>(heropowers.ToList().Where(e => e.HeroId == id))
                });

            this.HeroPowerService.Setup(e => e.DeleteByIdBase(It.IsAny<int>()))
                .Callback((int id) =>
                {
                    heropowers.Remove(heropowers.Find(e => e.Id == id));
                });

            Repository.Setup(e => e.DeleteAsync(It.IsAny<Hero>()))
                .Callback((Hero entity) =>
                {
                    heroes.Remove(entity);
                });

            var id = 2;

            //Act
            var dto = await Service.DeleteById(id);

            // Assert
            Assert.Null(dto);
            Assert.Single(heroes);
            Assert.Single(heropowers);
        }

        [Fact]
        public async void DeleteByIdNonExisting()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            var id = 5;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.DeleteById(id));

            // Assert
            Assert.Equal("Cannot delete Hero with id " + id + " because not found", ex.Message);
        }

        [Fact]
        public async void DeleteByNameNonExisting()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((string name) => heroes.FirstOrDefault(e => e.Name == name));

            var name = "Test Man";

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.DeleteByName(name));

            // Assert
            Assert.Equal("Cannot delete Hero with name " + name + " because not found", ex.Message);
        }

        #endregion

        #region using HeroPower

        [Fact]
        public async void GetAllHeroPowerByHeroUnknown()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            int id = 3;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.GetAllHeroPowerByHeroAsync(id));

            // Assert
            Assert.Equal("Hero with id " + id + " not found", ex.Message);
        }
     
        [Fact]
        public async void GetAllHeroPowerByPowerUnknown()
        {
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 1,
                    Name = "Super strength",
                    Description = "Super strong muscles"
                }
            };

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));

            int id = 3;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.GetAllHeroPowerByPowerAsync(id));

            // Assert
            Assert.Equal("Power with id " + id + " not found", ex.Message);
        }

        
        [Fact]
        public async void GetHeroPowerByHeroAndPowerUnknownHero()
        {
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };
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
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));

            int hero_id = 3;
            int power_id = 3;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.GetHeroPowerByHeroAndPowerAsync(hero_id, power_id));

            // Assert
            Assert.Equal("Hero with id " + hero_id + " not found", ex.Message);
        }

        [Fact]
        public async void GetHeroPowerByHeroAndPowerUnknownPower()
        {
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };
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
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));

            int hero_id = 1;
            int power_id = 3;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.GetHeroPowerByHeroAndPowerAsync(hero_id, power_id));

            // Assert
            Assert.Equal("Power with id " + power_id + " not found", ex.Message);
        }

        [Fact]
        public async void AddHeroPowerUnknownHero()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            var heropower = new HeroPowerDTO
            {
                Id = 0,
                HeroId = 2,
                PowerId = 4
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.AddHeroPower(heropower));

            // Assert
            Assert.Equal("Cannot add power to nonexistant Hero", ex.Message);
        }

        [Fact]
        public async void AddHeroPowerUnknownPower()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 4,
                    Name = "Batman",
                    Description = "Enough said"
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));


            var heropower = new HeroPowerDTO
            {
                Id = 0,
                HeroId = 1,
                PowerId = 3
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.AddHeroPower(heropower));

            // Assert
            Assert.Equal("Cannot add nonexistant power to Hero", ex.Message);
        }

        [Fact]
        public async void AddHeroPowerExisting()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 4,
                    Name = "Batman",
                    Description = "Enough said"
                }
            };
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 4
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));

            this.HeroPowerService.Setup(e => e.GetHeroPowerByHeroAndPowerAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int hero_id, int power_id) => Mapper.Map<HeroPowerDTO>(heropowers.FirstOrDefault(e => e.HeroId == hero_id && e.PowerId == power_id)));

            var heropower = new HeroPowerDTO
            {
                Id = 0,
                HeroId = 1,
                PowerId = 4
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<BadRequestException>(() => Service.AddHeroPower(heropower));

            // Assert
            Assert.Equal("Cannot create this Hero power because it already exists", ex.Message);
        }

        [Fact]
        public async void AddHeroPower()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 4,
                    Name = "Batman",
                    Description = "Enough said"
                },
                new Power
                {
                    Id = 5,
                    Name = "Super test",
                    Description = "Incredible testing abilities"
                }
            };
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 4
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));

            this.HeroPowerService.Setup(e => e.GetHeroPowerByHeroAndPowerAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int hero_id, int power_id) => Mapper.Map<HeroPowerDTO>(heropowers.FirstOrDefault(e => e.HeroId == hero_id && e.PowerId == power_id)));

            this.HeroPowerService.Setup(e => e.CreateBase(It.IsAny<HeroPower>()))
                .ReturnsAsync((HeroPower hp) => 
                {
                    heropowers.Add(hp);
                    return hp;
                });

            var heropower = new HeroPowerDTO
            {
                Id = 0,
                HeroId = 1,
                PowerId = 5,             
            };

            //Act
            var res = await Service.AddHeroPower(heropower);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(2, heropowers.Count);
        }

        [Fact]
        public async void UpdateHeroPowerUnknownHero()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 4,
                    Name = "Batman",
                    Description = "Enough said"
                }
            };
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 4
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            var heropower = new HeroPowerDTO
            {
                Id = 1,
                HeroId = 2,
                PowerId = 4
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.UpdateHeroPower(1, 4, heropower));

            // Assert
            Assert.Equal("Cannot update power to nonexistant Hero", ex.Message);
        }

        [Fact]
        public async void UpdateHeroPowerUnknownPower()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                }
            };
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 4,
                    Name = "Batman",
                    Description = "Enough said"
                }
            };
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 4
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));

            var heropower = new HeroPowerDTO
            {
                Id = 1,
                HeroId = 1,
                PowerId = 5
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.UpdateHeroPower(1, 4, heropower));

            // Assert
            Assert.Equal("Cannot update nonexistant power to Hero", ex.Message);
        }

        [Fact]
        public async void UpdateHeroPowerUnknownHeroPower()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                },
                new Hero
                {
                    Id = 2,
                    Name = "Test Man",
                    CityId = 1
                }
            };
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 4,
                    Name = "Batman",
                    Description = "Enough said"
                },
                new Power
                {
                    Id = 5,
                    Name = "Super test",
                    Description = "Incredible testing abilities"
                }
            };
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 4
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));

            this.HeroPowerService.Setup(e => e.GetHeroPowerByHeroAndPowerAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int hero_id, int power_id) => Mapper.Map<HeroPowerDTO>(heropowers.FirstOrDefault(e => e.HeroId == hero_id && e.PowerId == power_id)));

            var heropower = new HeroPowerDTO
            {
                Id = 1,
                HeroId = 1,
                PowerId = 5
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.UpdateHeroPower(2, 4, heropower));

            // Assert
            Assert.Equal("Cannot update nonexistant Hero power", ex.Message);
        }

        [Fact]
        public async void UpdateHeroPowerExisting()
        {
            //Arrange
            var heroes = new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Batman",
                    CityId = 2
                },
            };
            var powers = new List<Power>
            {
                new Power
                {
                    Id = 4,
                    Name = "Batman",
                    Description = "Enough said"
                },
                new Power
                {
                    Id = 5,
                    Name = "Super test",
                    Description = "Incredible testing abilities"
                }
            };
            var heropowers = new List<HeroPower>
            {
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 4
                },
                new HeroPower
                {
                    Id = 1,
                    HeroId = 1,
                    PowerId = 5
                }
            };

            Repository.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heroes.FirstOrDefault(e => e.Id == id));

            this.PowerService.Setup(e => e.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Mapper.Map<PowerDTO>(powers.FirstOrDefault(e => e.Id == id)));

            this.HeroPowerService.Setup(e => e.GetHeroPowerByHeroAndPowerAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int hero_id, int power_id) => Mapper.Map<HeroPowerDTO>(heropowers.FirstOrDefault(e => e.HeroId == hero_id && e.PowerId == power_id)));

            var heropower = new HeroPowerDTO
            {
                Id = 1,
                HeroId = 1,
                PowerId = 5
            };

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<BadRequestException>(() => Service.UpdateHeroPower(1, 4, heropower));

            // Assert
            Assert.Equal("Cannot update this Hero power to another that already exists", ex.Message);
        }

        #endregion

    }
}
