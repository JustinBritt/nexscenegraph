﻿//
// Copyright 2018 Sean Spicer 
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

using System.Collections.Generic;

namespace Veldrid.SceneGraph
{
    public interface IPipelineState
    {
        ShaderDescription? VertexShaderDescription { get; set; }
        ShaderDescription? FragmentShaderDescription { get; set; }
        IReadOnlyList<ITexture2D> TextureList { get; }
        IReadOnlyList<IBindable> UniformList { get; }
        BlendStateDescription BlendStateDescription { get; set; }
        DepthStencilStateDescription DepthStencilState { get; set; }
        RasterizerStateDescription RasterizerStateDescription { get; set; }
        void AddTexture(ITexture2D texture);
        void AddUniform(IBindable uniform);
        void AddUniform(IDrawable drawable, IBindable uniform);
    }
}