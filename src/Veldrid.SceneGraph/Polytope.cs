﻿//
// Copyright (c) 2018 Sean Spicer
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//

using System.Collections.Generic;
using System.Numerics;

namespace Veldrid.SceneGraph
{
    /// <summary>
    /// Class representing convex clipping volumes made up from planes.
    /// When adding planes, normals should point inward
    /// </summary>
    public class Polytope
    {
        private uint _resultMask;
        private List<Plane> _planeList;
        private List<Vector3> _vertexList;
        private Stack<uint> _maskStack;

        public Polytope()
        {
            SetupMask();
        }

        public void SetupMask()
        {
            _resultMask = 0;
            for(uint i=0;i<_planeList.Count;++i)
            {
                _resultMask = (_resultMask<<1) | 1;
            }
            _maskStack.Push(_resultMask);
        }

        public bool Contains(BoundingBox bb)
        {
            if (_maskStack.Count == 0) return true;

            _resultMask = _maskStack.Peek();
            uint selectorMask = 0x1;

            foreach (var plane in _planeList)
            {
                if (0 != (_resultMask & selectorMask))
                {
                    int res = plane.Intersect(bb);
                    if (res < 0) return false;  // Outside the clipping set
                    else if (res > 0) _resultMask ^= selectorMask;  // Don't check again
                }

                selectorMask <<= 1;
            }

            return true;
        }
    }
}