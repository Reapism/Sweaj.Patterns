﻿namespace Sweaj.Patterns.Serialization.Bytes
{
    public sealed class ByteSerializerOptions
    {
        private ByteSerializerOptions()
        {

        }

        private readonly static ByteSerializerOptions DefaultInstance = new()
        {
            CompressBytes = false
        };

        public static ByteSerializerOptions Default => DefaultInstance;

        public bool CompressBytes { get; set; }
    }
}
