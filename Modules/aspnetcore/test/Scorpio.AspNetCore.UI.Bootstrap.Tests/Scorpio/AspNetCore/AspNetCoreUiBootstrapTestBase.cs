using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using Shouldly;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scorpio.AspNetCore
{
    public abstract class AspNetCoreUiBootstrapTestBase : AspNetCoreTestBase<UI.Bootstrap.TestModule, UI.Bootstrap.Startup>
    {

    }

    public static class AspNetCoreUiBootstrapTestBaseExtensions
    {
        public static (TagHelperContext, TagHelperOutput) GetContext(this ITagHelper _, string tagName)
        {

            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));
            var tagHelperOutput = new TagHelperOutput(tagName,
                new TagHelperAttributeList(),
                (result, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
            return (tagHelperContext, tagHelperOutput);
        }

        public static TTagHelper GetTagHelper<TTagHelper>(this AspNetCoreUiBootstrapTestBase test)
            where TTagHelper : TagHelpers.TagHelper
        {
            return test.GetTagHelper<TTagHelper>(t => { });
        }

        public static TTagHelper GetTagHelper<TTagHelper>(this AspNetCoreUiBootstrapTestBase test, Action<TTagHelper> initAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            return ActivatorUtilities.CreateInstance<TTagHelper>(test.ServiceProvider).Action(initAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test, Action<TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>().Test(verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test, Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>().Test(verifyAction);
        }
        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test, Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>().Test(verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test, Action<TTagHelper> initAction, Action<TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test, Action<TTagHelper> initAction, Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(verifyAction);
        }
        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test, Action<TTagHelper> initAction, Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(verifyAction);
        }
        public static void Test(this ITagHelper tagHelper, Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            Test(tagHelper, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper, Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            Test(tagHelper, (t, a, c, o) => verifyAction(a, c, o));
        }
        public static void Test(this ITagHelper tagHelper, Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            var attrs = tagHelper.GetAttributes<HtmlTargetElementAttribute>();
            if (attrs.IsNullOrEmpty())
            {
                var tagName = tagHelper.GetType().Name.RemovePostFix("TagHelper").ToHyphen();
                TestCore(tagHelper, tagName, verifyAction, null);
                return;
            }
            attrs.ForEach(a =>
            {
                TestCore(tagHelper, a.Tag, verifyAction, a);
            });
            static void TestCore(ITagHelper tagHelper, string tagName, Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction, HtmlTargetElementAttribute a)
            {
                var (tagHelperContext, tagHelperOutput) = tagHelper.GetContext(tagName);
                ProcessCore(tagHelper, a, tagHelperContext, tagHelperOutput, verifyAction);
            }
        }

        public static void Test(this ITagHelper tagHelper, TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput, Action< TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelperOutput, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper, TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput, Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelperOutput, (t, a, c, o) => verifyAction(a, c, o));
        }
        public static void Test(this ITagHelper tagHelper, TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput, Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            var attrs = tagHelper.GetAttributes<HtmlTargetElementAttribute>();
            if (attrs.IsNullOrEmpty())
            {
                var tagName = tagHelper.GetType().Name.RemovePostFix("TagHelper").ToHyphen();
                TestCore(tagHelper, tagName, tagHelperContext,tagHelperOutput, verifyAction, null);
                return;
            }
            attrs.ForEach(a =>
            {
                TestCore(tagHelper, a.Tag, tagHelperContext, tagHelperOutput, verifyAction, a);
            });
            static void TestCore(ITagHelper tagHelper, string tagName, TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput, Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction, HtmlTargetElementAttribute a)
            {
                ProcessCore(tagHelper, a, tagHelperContext, tagHelperOutput, verifyAction);
            }
        }


        private static void ProcessCore(ITagHelper tagHelper, HtmlTargetElementAttribute attribute, TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput, Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            Should.NotThrow(() => tagHelper.Init(tagHelperContext));
            Should.NotThrow(() => tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput));
            verifyAction(tagHelper, attribute, tagHelperContext, tagHelperOutput);
        }
    }

}
