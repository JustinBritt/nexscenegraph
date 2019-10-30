//
// Copyright 2018-2019 Sean Spicer 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;

namespace Veldrid.SceneGraph
{
    public abstract class Drawable : Node, IDrawable
    {
        public string Name { get; set; } = string.Empty;

        public Type VertexType => GetVertexType();
        
        private IBoundingBox _boundingBox;
        private IBoundingBox _initialBoundingBox = BoundingBox.Create();
        public IBoundingBox InitialBoundingBox
        {
            get => _initialBoundingBox;
            set
            {
                _initialBoundingBox = value;
                DirtyBound();
            }
        }
        
        public override void Accept(INodeVisitor nv)
        {
            if (nv.ValidNodeMask(this))
            {
                nv.PushOntoNodePath(this);
                nv.Apply(this);
                nv.PopFromNodePath(this);
            };
        }
        
        public VertexLayoutDescription VertexLayout { get; set; }
        
        public List<IPrimitiveSet> PrimitiveSets { get; } = new List<IPrimitiveSet>();
        
        public event Func<Drawable, BoundingBox> ComputeBoundingBoxCallback;
        public event Action<CommandList, Drawable> DrawImplementationCallback;
        
        protected abstract Type GetVertexType();
        
        public void Draw(GraphicsDevice device, List<Tuple<uint, ResourceSet>> resourceSets, CommandList commandList)
        {
            if (null != DrawImplementationCallback)
            {
                DrawImplementationCallback(commandList, this);
            }
            else
            {
                DrawImplementation(device, resourceSets, commandList);
            }
        }

        protected abstract void DrawImplementation(GraphicsDevice device, List<Tuple<uint, ResourceSet>> resourceSets, CommandList commandList);

        public virtual void ConfigureDeviceBuffers(GraphicsDevice device, ResourceFactory factory)
        {
            // Nothing by default
        }

        public virtual void ConfigurePipelinesForDevice(GraphicsDevice device, ResourceFactory factory,
            ResourceLayout parentLayout)
        {
            // Nothing by default
        }     
        
        public IBoundingBox GetBoundingBox()
        {
            if (_boundingSphereComputed) return _boundingBox;
            
            _boundingBox = _initialBoundingBox;

            _boundingBox.ExpandBy(null != ComputeBoundingBoxCallback
                ? ComputeBoundingBoxCallback(this)
                : ComputeBoundingBox());

            if (_boundingBox.Valid())
            {
                _boundingSphere.Set(_boundingBox.Center, _boundingBox.Radius);
            }
            else
            {
                _boundingSphere.Init();
            }

            _boundingSphereComputed = true;

            return _boundingBox;
        }

        protected abstract IBoundingBox ComputeBoundingBox();

        public abstract DeviceBuffer GetVertexBufferForDevice(GraphicsDevice device);

        public abstract DeviceBuffer GetIndexBufferForDevice(GraphicsDevice device);


    }
}