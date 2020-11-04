using NSubstitute;
using Xunit;
using Shouldly;
namespace Scorpio.BackgroundWorkers
{
    public class BackgroundWorkerManager_Tests
    {

        [Fact]
        public void Add()
        {
            var worker = Substitute.For<IBackgroundWorker>();
          using  var manager = new BackgroundWorkerManager();
            manager.Add(worker);
            manager.BackgroundWorkers.ShouldHaveSingleItem().ShouldBe(worker);
            worker.Received(0).StartAsync();
        }

        [Fact]
        public void AddByStarted()
        {
            var worker = Substitute.For<IBackgroundWorker>();
           using var manager = new BackgroundWorkerManager();
            Shouldly.Should.NotThrow(() => manager.StartAsync());
            manager.Add(worker);
            manager.BackgroundWorkers.ShouldHaveSingleItem().ShouldBe(worker);
            worker.Received(1).StartAsync();
        }

        [Fact]
        public void Start()
        {
            var worker = Substitute.For<IBackgroundWorker>();
            using var manager = new BackgroundWorkerManager();
            manager.Add(worker);
            manager.BackgroundWorkers.ShouldHaveSingleItem().ShouldBe(worker);
            worker.Received(0).StartAsync();
            Shouldly.Should.NotThrow(() => manager.StartAsync());
            worker.Received(1).StartAsync();

        }

        [Fact]
        public void Stop()
        {
            var worker = Substitute.For<IBackgroundWorker>();
            var manager = new BackgroundWorkerManager();
            manager.Add(worker);
            manager.BackgroundWorkers.ShouldHaveSingleItem().ShouldBe(worker);
            worker.Received(0).StopAsync();
            Shouldly.Should.NotThrow(() => manager.StopAsync());
            worker.Received(1).StopAsync();
        }

        [Fact]
        public void Dispose()
        {
            var worker = Substitute.For<IBackgroundWorker>();
            var manager = new BackgroundWorkerManager();
            manager.Add(worker);
            manager.BackgroundWorkers.ShouldHaveSingleItem().ShouldBe(worker);
            worker.Received(0).StopAsync();
            Shouldly.Should.NotThrow(() => manager.Dispose());
            worker.Received(0).StopAsync();
        }
        [Fact]
        public void DisposeWithStarted()
        {
            var worker = Substitute.For<IBackgroundWorker>();
            var manager = new BackgroundWorkerManager();
            manager.Add(worker);
            manager.BackgroundWorkers.ShouldHaveSingleItem().ShouldBe(worker);
            Should.NotThrow(() => manager.StartAsync());
            worker.Received(0).StopAsync();
            Shouldly.Should.NotThrow(() => manager.Dispose());
            worker.Received(1).StopAsync();
        }
    }
}