using System;
using System.Collections.Generic;
using System.Text;

using Scorpio.Options;

using Shouldly;

using Xunit;

namespace Scorpio.Auditing
{
    public class AuditingOptionsExtensions_Tests
    {
        [Fact]
        public void EnableAuditingController()
        {
            var options = new AuditingOptions();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingController)).ShouldBeFalse();
            options.EnableAuditingController();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingController)).ShouldBeTrue();
        }

        [Fact]
        public void DisableAuditingController()
        {
            var options = new AuditingOptions();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingController)).ShouldBeFalse();
            options.EnableAuditingController();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingController)).ShouldBeTrue();
            options.DisableAuditingController();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingController)).ShouldBeFalse();
        }

        [Fact]
        public void IsAuditingController()
        {
            var options = new AuditingOptions();
            options.IsAuditingController().ShouldBeFalse();
            options.EnableAuditingController();
            options.IsAuditingController().ShouldBeTrue();
            options.DisableAuditingController();
            options.IsAuditingController().ShouldBeFalse();
        }

        [Fact]
        public void EnableAuditingPage()
        {
            var options = new AuditingOptions();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingPage)).ShouldBeFalse();
            options.EnableAuditingPage();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingPage)).ShouldBeTrue();
        }

        [Fact]
        public void DisableAuditingPage()
        {
            var options = new AuditingOptions();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingPage)).ShouldBeFalse();
            options.EnableAuditingPage();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingPage)).ShouldBeTrue();
            options.DisableAuditingPage();
            options.GetOption<bool>(nameof(AuditingOptionsExtensions.IsAuditingPage)).ShouldBeFalse();
        }

        [Fact]
        public void IsAuditingPage()
        {
            var options = new AuditingOptions();
            options.IsAuditingPage().ShouldBeFalse();
            options.EnableAuditingPage();
            options.IsAuditingPage().ShouldBeTrue();
            options.DisableAuditingPage();
            options.IsAuditingPage().ShouldBeFalse();
        }
    }
}
