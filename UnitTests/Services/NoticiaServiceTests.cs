using System;
using App.Blog.Application.Interfaces;
using App.Blog.Application.Services;
using App.Blog.Domain.Entities;
using App.Blog.Infra.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.Services;


public class NoticiaServiceTests
{
	private NoticiaService _noticiaService;

	public NoticiaServiceTests()
	{
		_noticiaService = new NoticiaService(new Mock<INoticiaRepository>().Object);
	}

    [Fact]
    public async Task ConsultarPorId_PassandoIdNoticiaVazia()
    {
        var exception = await Assert.ThrowsAsync<Exception>(() => _noticiaService.ConsultarPorIdAsync(0));
        Assert.Equal("Id precisa ser informado", exception.Message);
    }

    [Fact]
    public async Task Incluir_PassandoNoticiaJaExistente()
    {
        var exception = await Assert.ThrowsAsync<Exception>(() => _noticiaService.IncluirAsync(new Noticia { Id = 1}));
        Assert.Equal("Id precisa ser igual a 0", exception.Message);
    }

    [Fact]
    public async Task Atualizar_PassandoIdNoticiaVazia()
    {
        var exception = await Assert.ThrowsAsync<Exception>(() => _noticiaService.AtualizarAsync(new Noticia { Id = 0}));
        Assert.Equal("Id precisa ser informado", exception.Message);
    }
}

