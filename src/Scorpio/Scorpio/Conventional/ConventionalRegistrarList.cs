using System.Collections.Generic;

namespace Scorpio.Conventional
{
    internal class ConventionalRegistrarList:List<IConventionalRegistrar>
    {
        public static ConventionalRegistrarList Registrars { get; } = new ConventionalRegistrarList();

    }
}