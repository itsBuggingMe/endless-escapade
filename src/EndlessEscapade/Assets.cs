using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade
{
    internal static partial class Assets
    {
    }
    /// <summary>
    /// A wrapper for <see cref="Asset{T}"/> with some information about the asset.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct AssetWrapper<T> where T : class
    {
        public readonly string Path;
        private Asset<T> asset;

        public AssetWrapper(string path, AssetRequestMode requestMode = AssetRequestMode.DoNotLoad)
        {
            Path = path;
            asset = ModContent.Request<T>(path, requestMode);
        }
        /// <summary>
        /// Returns the underlying asset value. <br/>
        /// If the asset hasn't finished loading yet, it waits until its done loading. <br/>
        /// Equivalent to <see cref="AssetRequestMode.ImmediateLoad"/>.
        /// </summary>
        public T ValueImmediate
        {
            get
            {
                if(asset.State != AssetState.Loaded)
                Request(AssetRequestMode.ImmediateLoad);
                return asset.Value;
            }
        }
        /// <summary>
        /// Returns the underlying asset value or the asset's default if it hasn't loaded yet. <br/>
        /// If the asset hasn't been requested for loading yet, it begins loading. <br/>
        /// Equivalent to <see cref="AssetRequestMode.AsyncLoad"/>.
        /// </summary>
        public T Value
        {
            get
            {
                if (asset.State == AssetState.NotLoaded)
                    asset = ModContent.Request<T>(Path, AssetRequestMode.AsyncLoad);

                return asset.Value;
            }
        }
        /// <summary>
        /// Attempts to obtain the underlying value of the asset asychronously. <br/>
        /// </summary>
        /// <param name="value">The underlying asset value, or the default if the asset hasn't finished loading.</param>
        /// <returns><see langword="true"/> if the asset finished loading, <see langword="false"/> otherwise.</returns>
        public bool TryGet(out T value)
        {
            if (asset.State == AssetState.NotLoaded)
                asset = ModContent.Request<T>(Path, AssetRequestMode.AsyncLoad);
            value = asset.Value;
            return asset.State == AssetState.Loaded;
        }

        /// <summary>
        /// Attempts to obtain the underlying asset value without triggering an asset load.
        /// </summary>
        /// <param name="value">The underlying asset value, or the default if the asset hasn't finished or started loading.</param>
        /// <returns><see langword="true"/> if the asset finished loading, <see langword="false"/> otherwise.</returns>
        public readonly bool TryGetNoLoad(out T value)
        {
            value = asset.Value;
            return asset.State == AssetState.Loaded;
        }

        private void Request(AssetRequestMode requestMode)
        {
            if (requestMode != AssetRequestMode.DoNotLoad && asset.State == AssetState.NotLoaded)
                asset = ModContent.Request<T>(Path, requestMode);
            if (requestMode == AssetRequestMode.ImmediateLoad && asset.State != AssetState.Loaded)
                asset.Wait();
        }
    }
}
