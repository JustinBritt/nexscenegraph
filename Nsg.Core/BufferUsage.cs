﻿using System;

namespace Nsg.Core
{
    [Flags]
    public enum BufferUsage : byte
    {
        VertexBuffer = 1 << 0,
        IndexBuffer = 1 << 1,
        UniformBuffer = 1 << 2,
        StructuredBufferReadOnly = 1 << 3,
        StructuredBufferReadWrite = 1 << 4,
        IndirectBuffer = 1 << 5,
        Dynamic = 1 << 6,
        Staging = 1 << 7
    }
}