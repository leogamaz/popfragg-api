using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using popfragg.Api.Controllers;
using popfragg.Shared.DTOS.NadesDTO;
using popfragg.Domain.Services;
using popfragg.Domain.Models.Nades;
using FluentAssertions;


namespace popfragg.Tests.Controllers
{
    public class NadesControllerTests
    {
        [Fact(DisplayName ="01 - Obter nade pelo Id")]
        public async Task GetNadeById_ShouldReturnNade_WhenIdExists()
        {
            // Arrange
            var nadeId = Guid.NewGuid();
            var authordId = Guid.NewGuid();
            

            // Criamos o DTO que esperamos receber
            var expectedNade = new Nades
            {
                Id = nadeId,
                AuthorId = authordId,
                Title = "Smoke A Mid",
                Visibility = "Public",
                AccuracyLevel = "High",
                Technique = "Jumpthrow",
                Description = "A useful smoke for mid control.",
                Side = "T",
                Rating = 4.5m,
                Map = "de_mirage",
                Type = "Smoke",
                VideoUrl = "https://youtube.com/example",
                Tags =
                [
                    new NadeTags
                    {
                        Id = Guid.NewGuid(),
                        NadeId = nadeId,
                        Tag = "Mid Control",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new NadeTags
                    {
                        Id = Guid.NewGuid(),
                        NadeId = nadeId,
                        Tag = "Rsuh Mid",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                ],
                Images =
                [
                    new NadeImages
                    {
                        Id = Guid.NewGuid(),
                        NadeId = nadeId,
                        ImageUrl = "url1.png",
                        ImageOrder = 0,
                        UpdatedAt = DateTime.UtcNow,
                        UploadedAt = DateTime.UtcNow
                    },
                    new NadeImages
                    {
                        Id = Guid.NewGuid(),
                        NadeId = nadeId,
                        ImageUrl = "url2.png",
                        ImageOrder = 1,
                        UpdatedAt = DateTime.UtcNow,
                        UploadedAt = DateTime.UtcNow
                    },
                ],
                Accesses =
                [
                    new NadeAccess
                    {
                        Id = Guid.NewGuid(),
                        NadeId = nadeId,
                        GrantedAt = DateTime.UtcNow.AddDays(-1)
                    },
                ],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            // Mockamos o serviço que ainda não existe
            var nadeService = new Mock<INadesService>();
            nadeService.Setup(s => s.GetNadeByIdAsync(nadeId))
                       .ReturnsAsync(expectedNade);
            var currentUserService = new Mock<ICurrentUserService>();

            // Controller que vai chamar o serviço
            var controller = new NadesController(nadeService.Object, currentUserService.Object);

            // Act
            var result = await controller.GetNadeById(nadeId.ToString());

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var nade = okResult.Value as NadeDTO;
            nade?.Title.Should().Be("Smoke A Mid");
        }
        [Fact(DisplayName = "02 - Obter nade pelo Id, not found")]
        public async Task GetNadeById_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var nadeId = Guid.NewGuid();
            // Mockamos o serviço que ainda não existe
            var nadeService = new Mock<INadesService>();
            nadeService.Setup(s => s.GetNadeByIdAsync(nadeId))
                       .ReturnsAsync((Nades?)null);
            var currentUserService = new Mock<ICurrentUserService>();

            // Controller que vai chamar o serviço
            var controller = new NadesController(nadeService.Object, currentUserService.Object);
            // Act
            var result = await controller.GetNadeById(nadeId.ToString());
            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact(DisplayName = "03 - Criar Nade")]
        public async Task CreateNade_ShouldReturnCreatedNade_WhenDataIsValid()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var newNadeDto = new NadeDTO
            {
                Title = "Flashbang B Short",
                Visibility = "Private",
                Technique = "Standard",
                Side = "CT",
                Map = "de_dust2",
                Type = "Flashbang",
                VideoUrl = "https://youtube.com/flashbangbshort",
                Tags = ["B Site", "Short"],
                Images =
                [
                    new NadeImagesDTO
                    {
                        ImageUrl = "flash1.png",
                        OrderImage = 0
                    },
                ],
                Accesses = [Guid.NewGuid()]
            };
            var createdNade = new Nades
            {
                Id = Guid.NewGuid(),
                AuthorId = authorId,
                Title = newNadeDto.Title,
                Visibility = newNadeDto.Visibility,
                Technique = newNadeDto.Technique,
                Side = newNadeDto.Side,
                Map = newNadeDto.Map,
                Type = newNadeDto.Type,
                VideoUrl = newNadeDto.VideoUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var nadeService = new Mock<INadesService>();
            nadeService.Setup(s => s.AddNadeAsync(newNadeDto, authorId))
                       .ReturnsAsync(createdNade);
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(c => c.Id).Returns(authorId.ToString());
            var controller = new NadesController(nadeService.Object, currentUserService.Object);
            // Act
            var result = await controller.CreateNade(newNadeDto);
            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var nade = okResult.Value as NadeDTO;
            nade?.Title.Should().Be("Flashbang B Short");
        }

        [Fact(DisplayName = "04 - Mostrar todas as nades que o usuario possui acesso")]
        public async Task GetAllNadesForUser_ShouldReturnNades_WhenUserHasAccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedNades = new List<Nades>
            {
                new() {
                    Id = Guid.NewGuid(),
                    AuthorId = userId,
                    Title = "Smoke A Mid",
                    Visibility = "Public",
                    Technique = "Jumpthrow",
                    Side = "T",
                    Map = "de_mirage",
                    Type = "Smoke",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    AuthorId = userId,
                    Title = "Flash B Short",
                    Visibility = "Private",
                    Technique = "Standard",
                    Side = "CT",
                    Map = "de_dust2",
                    Type = "Flashbang",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            var nadeService = new Mock<INadesService>();
            nadeService.Setup(s => s.GetAllNadesForUserAsync(userId))
                       .ReturnsAsync(expectedNades);
            var currentUserService = new Mock<ICurrentUserService>();
            currentUserService.Setup(c => c.Id).Returns(userId.ToString());
            var controller = new NadesController(nadeService.Object, currentUserService.Object);
            // Act
            var result = await controller.GetAllNadesForUser();
            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var nades = okResult.Value as IEnumerable<Nades>;
            nades.Should().HaveCount(2);
        }
    }

}
