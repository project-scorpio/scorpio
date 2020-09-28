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
using System.Linq;

namespace Scorpio.AspNetCore
{
    public abstract class AspNetCoreUiBootstrapTestBase : AspNetCoreTestBase<UI.Bootstrap.TestModule, UI.Bootstrap.Startup>
    {

    }

    public static class AspNetCoreUiBootstrapTestBaseExtensions
    {
        public static (TagHelperContext, TagHelperOutput) GetContextAndOutput(this ITagHelper tagHelper, string tagName)
        {
            return (tagHelper.GetContext(), tagHelper.GetOutput(tagName));
        }

        public static TagHelperContext GetContext(this ITagHelper _)
        {

            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                Guid.NewGuid().ToString("N"));

            return tagHelperContext;
        }

        public static TagHelperOutput GetOutput(this ITagHelper _, string tagName)
        {

            var tagHelperOutput = new TagHelperOutput(tagName,
                new TagHelperAttributeList(),
                (result, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
            return tagHelperOutput;
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

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>().Test(verifyAction);
        }
        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test, Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>().Test(verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TTagHelper> initAction,
                                            Action<TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TTagHelper> initAction,
                                            Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(verifyAction);
        }
        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TTagHelper> initAction,
                                            Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TagHelperContext> initContextAction,
                                            Action<TagHelperOutput> initOutputAction,
                                            Action<TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>().Test(initContextAction, initOutputAction, verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TagHelperContext> initContextAction,
                                            Action<TagHelperOutput> initOutputAction,
                                            Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>().Test(initContextAction, initOutputAction, verifyAction);
        }
        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TagHelperContext> initContextAction,
                                            Action<TagHelperOutput> initOutputAction,
                                            Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>().Test(initContextAction, initOutputAction, verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TTagHelper> initAction,
                                            Action<TagHelperContext> initContextAction,
                                            Action<TagHelperOutput> initOutputAction,
                                            Action<TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(initContextAction, initOutputAction, verifyAction);
        }

        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TTagHelper> initAction,
                                            Action<TagHelperContext> initContextAction,
                                            Action<TagHelperOutput> initOutputAction,
                                            Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(initContextAction, initOutputAction, verifyAction);
        }
        public static void Test<TTagHelper>(this AspNetCoreUiBootstrapTestBase test,
                                            Action<TTagHelper> initAction,
                                            Action<TagHelperContext> initContextAction,
                                            Action<TagHelperOutput> initOutputAction,
                                            Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
            where TTagHelper : TagHelpers.TagHelper
        {
            test.GetTagHelper<TTagHelper>(initAction).Test(initContextAction,initOutputAction, verifyAction);
        }


        public static void Test(this ITagHelper tagHelper,
                        Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            Test(tagHelper, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                                Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            Test(tagHelper, (t, a, c, o) => verifyAction(a, c, o));
        }
        public static void Test(this ITagHelper tagHelper,
                                Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelper.GetContext(), verifyAction);
        }

        public static void Test(this ITagHelper tagHelper,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            Test(tagHelper, initContextAction, initOutputAction, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            Test(tagHelper, initContextAction, initOutputAction, (t, a, c, o) => verifyAction(a, c, o));
        }
        public static void Test(this ITagHelper tagHelper,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelper.GetContext(), initContextAction, initOutputAction, verifyAction);
        }

        public static void Test(this ITagHelper tagHelper,
        TagHelperContext tagHelperContext,
        Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                TagHelperContext tagHelperContext,
                Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, (t, a, c, o) => verifyAction(a, c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                        TagHelperContext tagHelperContext,
                        Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, c => { }, o => { }, verifyAction);
        }

        public static void Test(this ITagHelper tagHelper,
                TagHelperContext tagHelperContext,
                Action<TagHelperContext> initContextAction,
                Action<TagHelperOutput> initOutputAction,
                Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, initContextAction, initOutputAction, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                TagHelperContext tagHelperContext,
                Action<TagHelperContext> initContextAction,
                Action<TagHelperOutput> initOutputAction,
                Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, initContextAction, initOutputAction, (t, a, c, o) => verifyAction(a, c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                        TagHelperContext tagHelperContext,
                        Action<TagHelperContext> initContextAction,
                        Action<TagHelperOutput> initOutputAction,
                        Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {

            var attrs = tagHelper.GetAttributes<HtmlTargetElementAttribute>();
            if (attrs.IsNullOrEmpty())
            {
                var tagName = tagHelper.GetType().Name.RemovePostFix("TagHelper").ToHyphen();
                Test(tagHelper, tagHelperContext, tagName, initContextAction, initOutputAction, verifyAction);
                return;
            }
            attrs.ForEach(a =>
            {
                Test(tagHelper, tagHelperContext, a.Tag, initContextAction, initOutputAction, verifyAction);
            });
        }

        public static void Test(this ITagHelper tagHelper,
                        TagHelperContext tagHelperContext,
                        string tagName,
                        Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagName, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                                TagHelperContext tagHelperContext,
                                string tagName,
                                Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagName, (t, a, c, o) => verifyAction(a, c, o));
        }
        public static void Test(this ITagHelper tagHelper,
                                TagHelperContext tagHelperContext,
                                string tagName,
                                Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelper.GetOutput(tagName), verifyAction);
        }

        public static void Test(this ITagHelper tagHelper,
                                TagHelperContext tagHelperContext,
                                string tagName,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagName, initContextAction, initOutputAction, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                                TagHelperContext tagHelperContext,
                                string tagName,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagName, initContextAction, initOutputAction, (t, a, c, o) => verifyAction(a, c, o));
        }
        public static void Test(this ITagHelper tagHelper,
                                TagHelperContext tagHelperContext,
                                string tagName,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelper.GetOutput(tagName), initContextAction, initOutputAction, verifyAction);
        }

        public static void Test(this ITagHelper tagHelper,
                TagHelperContext tagHelperContext,
                TagHelperOutput tagHelperOutput,
                Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelperOutput, (t, a, c, o) => verifyAction(c, o));
        }
        public static void Test(this ITagHelper tagHelper,
                        TagHelperContext tagHelperContext,
                        TagHelperOutput tagHelperOutput,
                        Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelperOutput, (t, a, c, o) => verifyAction(a, c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                        TagHelperContext tagHelperContext,
                        TagHelperOutput tagHelperOutput,
                        Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelperOutput, c => { }, o => { }, verifyAction);
        }

        public static void Test(this ITagHelper tagHelper,
                                TagHelperContext tagHelperContext,
                                TagHelperOutput tagHelperOutput,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelperOutput, initContextAction, initOutputAction, (a, c, o) => verifyAction(c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                                TagHelperContext tagHelperContext,
                                TagHelperOutput tagHelperOutput,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            tagHelper.Test(tagHelperContext, tagHelperOutput, initContextAction, initOutputAction, (t, a, c, o) => verifyAction(a, c, o));
        }

        public static void Test(this ITagHelper tagHelper,
                                TagHelperContext tagHelperContext,
                                TagHelperOutput tagHelperOutput,
                                Action<TagHelperContext> initContextAction,
                                Action<TagHelperOutput> initOutputAction,
                                Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            var attr = tagHelper.GetAttributes<HtmlTargetElementAttribute>().Where(a => a.Tag == tagHelperOutput.TagName).FirstOrDefault();
            ProcessCore(tagHelper, attr, tagHelperContext, tagHelperOutput, initContextAction, initOutputAction, verifyAction);
        }


        private static void ProcessCore(ITagHelper tagHelper,
                                        HtmlTargetElementAttribute attribute,
                                        TagHelperContext tagHelperContext,
                                        TagHelperOutput tagHelperOutput,
                                        Action<TagHelperContext> initContextAction,
                                        Action<TagHelperOutput> initOutputAction,
                                        Action<ITagHelper, HtmlTargetElementAttribute, TagHelperContext, TagHelperOutput> verifyAction)
        {
            initContextAction(tagHelperContext);
            initOutputAction(tagHelperOutput);
            Should.NotThrow(() => tagHelper.Init(tagHelperContext));
            Should.NotThrow(() => tagHelper.ProcessAsync(tagHelperContext, tagHelperOutput));
            verifyAction(tagHelper, attribute, tagHelperContext, tagHelperOutput);
        }
    }

}
