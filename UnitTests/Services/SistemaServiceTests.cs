using System;
using App.Application.Interfaces;
using App.Application.Services;
using App.Blog.Infra.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.Services;

public class SistemaServiceTests
{
	private SistemaService _sistemaService;

	public SistemaServiceTests()
	{
            _sistemaService = new SistemaService(new Mock<ISistemaRepository>().Object);
	}

	[Fact]
    public void VerificarChaveExiste_PassandoChaveVazia()
    {
        var exception = Assert.Throws<Exception>(() => _sistemaService.VerificarChaveExiste(string.Empty));
        Assert.Equal("Chave precisa ser informada", exception.Message);
    }
}