using FlyAway.Application;

namespace FlyAway.Api.Core
{
    public class FakeApiActor : IApplicationActor
    {
        public int Id => 1;

        public string Identity => "Test API User";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1 };
    }
    
    public class AdminFakeApiActor : IApplicationActor
    {
        public int Id => 2;

        public string Identity => "Fake Admin";

        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1,1000);
    }
}
