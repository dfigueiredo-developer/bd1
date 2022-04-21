namespace Tests;

using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Projeto_BD1.Data.Contracts;
using Projeto_BD1.Helpers;
using Projeto_BD1.Models;
using System.Data;
using Xunit;

public class GenericTest : IClassFixture<Program>
{
    private ServiceProvider _serviceProvider;
    private IDatabaseMethods? _context;

    public GenericTest(Program fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
        _context = _serviceProvider.GetService<IDatabaseMethods>();
    }

    [Fact]
    public async void GetArrayListGenericTest()
    {
        var centers = await _context.crudArrayListAsync<Center>("SELECT * FROM Center");
        Assert.NotEmpty(centers);
    }


    [Fact]
    public async void GetSingleObjectGenericTest()
    {
        var parameters = new DynamicParameters();
        parameters.Add("ID", 1, DbType.Int32);
        var centerList = await _context.crudArrayListAsync<Center>("SELECT * FROM Center Where ID=@ID", parameters);
        var center = HelpersFunctions.GetFirstObject<Center>(centerList);

        Assert.NotNull(center);
        Assert.IsType<Center>(center);
    }



    [Fact]
    public async void PostArrayListGenericTest()
    {
        var parameters = new DynamicParameters();
        parameters.Add("Location", "Celorico da Beira", DbType.String);
        parameters.Add("AlocPercent", 90, DbType.Int32);
        parameters.Add("isActive", 1, DbType.Int32);
        var center = await _context.crudArrayListAsync<Center>("INSERT INTO Center (Location, AlocPercent, IsActive) OUTPUT INSERTED.* VALUES (@Location, @AlocPercent, @isActive)", parameters);


        Assert.NotNull(center);
    }


    [Fact]
    public async void UpdateArrayListGenericTest()
    {
        var parameters = new DynamicParameters();
        parameters.Add("Location", "Celorico de Bastos", DbType.String);
        parameters.Add("WhereLocation", "Celorico da Beira", DbType.String);
        var center = await _context.crudArrayListAsync<Center>("Update Center Set Location = @Location OUTPUT inserted.* Where Location = @WhereLocation", parameters);


        Assert.NotNull(center);

        
    }


    [Fact]
    public async void DeleteArrayListGenericTest()
    {
        var parameters = new DynamicParameters();
        parameters.Add("ID", 9, DbType.Int32);
        var center = await _context.crudArrayListAsync<int>("DELETE Center WHERE ID = @ID", parameters);


        Assert.NotNull(center);
    }



}