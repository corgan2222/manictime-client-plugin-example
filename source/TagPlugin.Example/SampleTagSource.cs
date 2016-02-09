﻿using Finkit.ManicTime.Common.TagSources;

namespace TagPlugin
{
    public class SampleTagSource : BasicTagSource
    {
        public SampleTagSource(ITagSourceCache tagSourceCache) : base(tagSourceCache)
        {
        }

        protected override BasicTagSourceInstance CreateServerTagSourceInstance(ITagSourceSettings settings,
            string cacheTimestamp)
        {
            return new SampleTagSourceInstance();
        }
    }
}