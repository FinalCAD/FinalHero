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
    public class HeroPowerServiceTest
    {
        private IHeroPowerService Service { get; }

        private Mock<IHeroPowerRepository> Repository { get; }

        public HeroPowerServiceTest()
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

            Repository = new Mock<IHeroPowerRepository>();

            Service = new HeroPowerService(Repository.Object);
        }

        [Fact]
        public async void GetAllHeroPower()
        {
            //Arrange
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
                    Id = 2,
                    HeroId = 2,
                    PowerId = 1
                }
            };

            Repository.Setup(e => e.GetAllAsync())
                .ReturnsAsync(heropowers);

            //Act
            var res = await Service.GetAllHeroPowerAsync();

            //Assert
            Assert.NotNull(res);
            Assert.Equal(res.Entities.Count, heropowers.Count);
        }

        [Fact]
        public async void GetAllHeroPowerByHero()
        {
            //Arrange
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
                    Id = 2,
                    HeroId = 2,
                    PowerId = 1
                }
            };

            Repository.Setup(e => e.GetAllByHeroIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heropowers.FindAll(e => e.HeroId == id));

            var hero_id = 1;

            //Act
            var res = await Service.GetAllHeroPowerByHeroAsync(hero_id);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.Entities.Count);
        }

        [Fact]
        public async void GetAllHeroPowerByPower()
        {
            //Arrange
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
                    Id = 2,
                    HeroId = 2,
                    PowerId = 1
                }
            };

            Repository.Setup(e => e.GetAllByPowerIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heropowers.FindAll(e => e.PowerId == id));

            var power_id = 4;

            //Act
            var res = await Service.GetAllHeroPowerByPowerAsync(power_id);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.Entities.Count);
        }

        [Fact]
        public async void GetAllByHeroIdWithPower()
        {
            //Arrange
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

            Repository.Setup(e => e.GetAllByHeroIdWithPowerAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => heropowers.FindAll(e => e.HeroId == id));

            var hero_id = 1;

            //Act
            var res = await Service.GetAllByHeroIdWithPowerAsync(hero_id);

            //Assert
            Assert.NotNull(res);
            Assert.Single(res);
        }      

        [Fact]
        public async void DeleteHeroPowerByHeroAndPower()
        {
            //Arrange
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
                    Id = 2,
                    HeroId = 2,
                    PowerId = 1
                }
            };

            Repository.Setup(e => e.GetHeroPowerByHeroIdAndPowerIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int hero_id, int power_id) => heropowers.FirstOrDefault(e => e.HeroId == hero_id && e.PowerId == power_id));

            Repository.Setup(e => e.DeleteAsync(It.IsAny<HeroPower>()))
                .Callback((HeroPower entity) =>
                {
                    heropowers.Remove(entity);
                });

            var hero_id = 2;
            var power_id = 1;

            //Act
            await Service.DeleteHeroPowerByHeroAndPower(hero_id, power_id);

            //Assert
            Assert.Single(heropowers);
        }        

       [Fact]
        public async void DeleteHeroPowerByHeroAndPowerUnknown()
        {
            //Arrange
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
                    Id = 2,
                    HeroId = 2,
                    PowerId = 1
                }
            };

            Repository.Setup(e => e.GetHeroPowerByHeroIdAndPowerIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int hero_id, int power_id) => heropowers.FirstOrDefault(e => e.HeroId == hero_id && e.PowerId == power_id));

            var hero_id = 2;
            var power_id = 2;

            //Act
            Exception ex = await Assert.ThrowsAnyAsync<NotFoundException>(() => Service.DeleteHeroPowerByHeroAndPower(hero_id, power_id));

            // Assert
            Assert.Equal("Cannot delete beacause Hero " + hero_id + " doesn't have power " + power_id, ex.Message);
        }
    }
}
