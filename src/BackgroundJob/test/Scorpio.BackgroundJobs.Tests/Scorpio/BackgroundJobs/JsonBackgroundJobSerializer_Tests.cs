using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using Shouldly;

using Xunit;

namespace Scorpio.BackgroundJobs
{
    public class JsonBackgroundJobSerializer_Tests
    {
        [Fact]
        public void Serialize()
        {
            var act = JsonConvert.SerializeObject(1);
            var serialzer = new JsonBackgroundJobSerializer();
            var exp = serialzer.Serialize(1);
            exp.ShouldBe(act);
        }

        [Fact]
        public void Deserialize()
        {
            var str = JsonConvert.SerializeObject(1);
            var serialzer = new JsonBackgroundJobSerializer();
            Should.NotThrow(() => serialzer.Deserialize(str, typeof(int))).ShouldBe(1);
            Should.Throw<JsonSerializationException>(()=>serialzer.Deserialize(str,typeof(BackgroundJobOptions)));
        }      
        
        [Fact]
        public void DeserializeOfT()
        {
            var str = JsonConvert.SerializeObject(1);
            var serialzer = new JsonBackgroundJobSerializer();
            Should.NotThrow(() => serialzer.Deserialize<int>(str)).ShouldBe(1);
            Should.Throw<JsonSerializationException>(()=>serialzer.Deserialize<BackgroundJobOptions>(str));
        }
    }
}
