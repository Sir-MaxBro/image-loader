using ImageLoader.Shared;
using StructureMap;
using System;

namespace ImageLoader.IoC
{
    public static class BootstrapContainer
    {
        private static Lazy<IContainer> _containerLazy = new Lazy<IContainer>(InitializeContainer);

        public static IContainer Container => _containerLazy.Value;

        private static IContainer InitializeContainer()
        {
            return new Container(c =>
            {
                c.AddRegistry<SharedRegistry>();
            });
        }
    }
}
