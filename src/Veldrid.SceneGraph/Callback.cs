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

using Veldrid.SceneGraph.RenderGraph;

namespace Veldrid.SceneGraph
{
    public interface ICallback
    {
        bool Run(IObject obj, IObject data);
    }

    public interface INodeCallback : ICallback
    {
    }

    public interface IDrawableCullCallback : INodeCallback
    {
        bool Cull(ICullVisitor cv, IDrawable drawable);
    }
    
    public class Callback : ICallback
    {
        public virtual bool Run(IObject obj, IObject data)
        {
            return Traverse(obj, data);
        }

        public bool Traverse(IObject obj, IObject data)
        {
            if (obj is INode node && data is INodeVisitor nodeVisitor)
            {
                nodeVisitor.Traverse(node);
                return true;
            }

            return false;
        }
    }
}