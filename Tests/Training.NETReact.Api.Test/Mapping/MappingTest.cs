using AutoMapper;
using NUnit.Framework;
using Training.NETReact.Application;

namespace Training.NETReact.Api.Test.Mapping
{
    [TestFixture]
    public class MappingProfileTest
    {
        [Test]
        public void MappingConfigurationTest()
        {
            //arrange
            IMapper mapper;
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new MappingProfile());
            });

            //act
            mapper = config.CreateMapper();

            //assert
            Assert.IsTrue(mapper != null);
        }
    }
}
