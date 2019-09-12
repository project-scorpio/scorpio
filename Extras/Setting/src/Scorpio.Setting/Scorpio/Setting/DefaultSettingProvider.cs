using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Setting
{
    internal class DefaultSettingProvider : SettingProvider, DependencyInjection.ISingletonDependency
    {
        public DefaultSettingProvider(ISettingStore settingStore) : base(settingStore)
        {
        }

        protected override string Key => null;
    }
}
