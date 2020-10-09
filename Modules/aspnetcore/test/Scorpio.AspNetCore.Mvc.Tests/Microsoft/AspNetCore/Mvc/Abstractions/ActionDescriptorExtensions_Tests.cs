using System;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Scorpio;

using Shouldly;

using Xunit;

namespace Microsoft.AspNetCore.Mvc.Abstractions
{
    public class ActionDescriptorExtensions_Tests
    {
        [Fact]
        public void AsControllerActionDescriptor()
        {
            ActionDescriptor descriptor = new PageActionDescriptor();
            Should.Throw<ScorpioException>(() => descriptor.AsControllerActionDescriptor());
            descriptor = new ControllerActionDescriptor();
            Should.NotThrow(() => descriptor.AsControllerActionDescriptor()).ShouldNotBeNull();
        }
        [Fact]
        public void IsControllerAction()
        {
            ActionDescriptor descriptor = new PageActionDescriptor();
            Should.NotThrow(() => descriptor.IsControllerAction()).ShouldBeFalse();
            descriptor = new ControllerActionDescriptor();
            Should.NotThrow(() => descriptor.IsControllerAction()).ShouldBeTrue();
        }

        [Fact]
        public void GetMethodInfo()
        {
            ActionDescriptor descriptor = new ControllerActionDescriptor() { MethodInfo = ((Action)GetMethodInfo).Method };
            descriptor.GetMethodInfo().ShouldNotBeNull();
        }

        [Fact]
        public void GetReturnType()
        {
            ActionDescriptor descriptor = new ControllerActionDescriptor() { MethodInfo = ((Action)GetMethodInfo).Method };
            descriptor.GetReturnType().ShouldBe(typeof(void));
        }

        [Fact]
        public void HasObjectResult()
        {
            var descriptor = new ControllerActionDescriptor() { MethodInfo = ((Func<string>)(() => default)).Method };
            descriptor.HasObjectResult().ShouldBeTrue();
            descriptor.MethodInfo = ((Func<ViewResult>)(() => default)).Method;
            descriptor.HasObjectResult().ShouldBeFalse();
            descriptor.MethodInfo = ((Func<JsonResult>)(() => default)).Method;
            descriptor.HasObjectResult().ShouldBeTrue();
            descriptor.MethodInfo = ((Func<ObjectResult>)(() => default)).Method;
            descriptor.HasObjectResult().ShouldBeTrue();
        }

    }
}
