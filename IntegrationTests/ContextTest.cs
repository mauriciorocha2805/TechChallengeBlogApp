using App.Blog.Domain.Entities;
using App.Blog.Infra.Context;
using App.Blog.Infra.Repository;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class ContextTest
    {
       
        private readonly DbContextOptions<NoticiaDBContext> _context;

        public ContextTest(DbContextOptions<NoticiaDBContext> options)
        {
            _context = options;
        }


        [Fact]
        public void TestaConexaoContextoComBD()
        {

            //Arrange
            var contexto = new NoticiaDBContext(_context);
            bool conectado;

            //Act
            try
            {
                conectado = contexto.Database.CanConnect();
            }
            catch (Exception e)
            {
                throw new Exception($"Não foi possível conectar a base de dados.[{e.Message}]");

            }

            //Assert
            Assert.True(conectado);
        }

    }
}
