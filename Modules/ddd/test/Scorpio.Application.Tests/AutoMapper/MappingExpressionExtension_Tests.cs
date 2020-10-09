using System.Collections.Generic;

using Scorpio.Data;

using Shouldly;

using Xunit;

namespace AutoMapper
{
    public class MappingExpressionExtension_Tests
    {
        [Fact]
        public void MapFromExtraProperty()
        {
            var mapper = new Mapper(new MapperConfiguration(expression =>
              {
                  expression.CreateMap<TestClass, TestClassDto>()
                            .MapFromExtraProperty(e => e.Age)
                            .MapFromExtraProperty(e => e.Address);
              }));

            var entity = new TestClass { Name = "Test" };
            entity.ExtraProperties.Add("Age", 18);
            entity.ExtraProperties.Add("Address", "Test Address");
            var dto = mapper.Map<TestClassDto>(entity);
            dto.Name.ShouldBe("Test");
            dto.Age.ShouldBe(18);
            dto.Address.ShouldBe("Test Address");
        }

        [Fact]
        public void MapExtraProperties()
        {
            var mapper = new Mapper(new MapperConfiguration(expression =>
            {
                expression.CreateMap<TestClassDto, TestClass>()
                          .MapExtraProperties(c => c.Property(e => e.Age)
                                                    .Property(e => e.Address));
            }));
            var dto = new TestClassDto { Name = "Test", Age = 18, Address = "Test Address" };
            var entity = mapper.Map<TestClass>(dto);
            entity.Name.ShouldBe("Test");
            entity.ExtraProperties.ShouldContainKeyAndValue("Age", 18);
            entity.ExtraProperties.ShouldContainKeyAndValue("Address", "Test Address");
        }

        public class TestClass : IHasExtraProperties
        {

            public string Name { get; set; }
            public IDictionary<string, object> ExtraProperties { get; } = new Dictionary<string, object>();
        }

        public class TestClassDto
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public string Address { get; set; }
        }
    }
}
