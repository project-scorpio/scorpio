using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using NSubstitute;

using Shouldly;

using Xunit;

namespace Scorpio.Auditing
{
    public class SimpleLogAuditingStore_Tests
    {
        [Fact]
        public void SaveAsync()
        {
            var logger = Substitute.For<ILogger<SimpleLogAuditingStore>>();
            var store = new SimpleLogAuditingStore(logger);
            var audit = new AuditInfo();
            var actionInfo = new AuditActionInfo();
            actionInfo.ExtraProperties.Add("Key", "Value");
            audit.Actions.Add(actionInfo);
            audit.Exceptions.Add(new Exception());
            audit.ExtraProperties.Add("Key", "Value");
            Should.NotThrow(() => store.SaveAsync(audit));
            logger.ReceivedWithAnyArgs(1).LogInformation(default);
        }

        [Fact]
        public void Logger()
        {
            var logger = Substitute.For<ILogger<SimpleLogAuditingStore>>();
            new SimpleLogAuditingStore().Logger.ShouldBe(NullLogger<SimpleLogAuditingStore>.Instance);
            new SimpleLogAuditingStore(logger).Logger.ShouldBe(logger);
        }
    }
}
