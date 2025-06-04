using Mapster;
using MapsterIssue.Models;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace MapsterIssue;

public class MapperTests
{
    private IMapper _mapper;
    
    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMapster();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var config = TypeAdapterConfig.GlobalSettings;
        //config.Default.PreserveReference(true);
        
        config.NewConfig<User, UserDto>()
            .Map(dest => dest.UserId, src => src.Id);
        
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

    [Test]
    public void Mapping_Test()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User"
        };
        var result = _mapper.Map<UserDto>(user);
        
        Assert.That(result.UserId, Is.EqualTo(user.Id));
        Assert.That(result.Name, Is.EqualTo(user.Name));
    }
}