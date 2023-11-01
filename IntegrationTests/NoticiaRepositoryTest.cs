using App.Blog.Domain.Entities;
using App.Blog.Infra.Context;
using App.Blog.Infra.Interfaces;
using App.Blog.Infra.Repository;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests
{
    public class NoticiaRepositoryTest 
    {
        private readonly INoticiaRepository _repository;

        public NoticiaRepositoryTest()
        {

            var servico = new ServiceCollection();
            servico.AddTransient<INoticiaRepository, NoticiaRepository>();

            var provedor = servico.BuildServiceProvider();
            _repository = provedor.GetService<INoticiaRepository>();
        }

        [Fact]
        public void TestaObterTodasNoticiasRepositorio()
        {
            //Arrange
            //Act
            Task<List<Noticia>> lista = _repository.ConsultarAsync();

            //Assert
            Assert.NotNull(lista);
        }

        [Fact]
        public void TesteInsereUmaNovaNoticiaNaBaseDeDadosRepositorio()
        {
            //Arrange
            string titulo = "Test";
            string conteudo = "Integration Test";
            string autor = "G1";
            int id = 6;
            DateTime datapublicacao = DateTime.Now;

            var noticia = new Noticia()
            {
                Id = id,
                Titulo = titulo,
                Conteudo = conteudo,
                DataPublicacao = datapublicacao,
                Autor = autor
            };

            //Act
            var retorno = _repository.Adicionar(noticia);

            //Assert
            Assert.True(retorno);
        }

        [Fact]
        public void TestaRemoverNoticia()
        {
            //Arrange
            //Act
            var atualizado = _repository.Excluir(6);

            //Assert
            Assert.True(atualizado);
        }


    }
}
