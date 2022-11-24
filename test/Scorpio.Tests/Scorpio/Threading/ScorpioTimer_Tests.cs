//using System.Threading;
//using System.Threading.Tasks;

//using Microsoft.Extensions.DependencyInjection;

//using Scorpio.Modularity;

//using Shouldly;

//using Xunit;

//namespace Scorpio.Threading
//{
//    public class ScorpioTimer_Tests : TestBase.IntegratedTest<IndependentEmptyModule>
//    {
//        [Fact]
//        public async Task DefaultAsync()
//        {
//            using (var timer = ServiceProvider.GetService<ScorpioTimer>())
//            {
//                var act = 0;
//                timer.Period = 10;
//                timer.Elapsed += (o, e) => { act++; Thread.Sleep(100); };
//                timer.RunOnStart = true;
//                timer.Start();
//                await Task.Delay(1000);
//                timer.Stop();
//                act.ShouldBeGreaterThan(1);
//            }
//        }
//    }
//}
