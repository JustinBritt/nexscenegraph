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

using System.Numerics;
using Examples.Common.Wpf;
using ImageFrac.Common.UserControls.Graphics._3D;
using Veldrid;
using Veldrid.SceneGraph;
using Veldrid.SceneGraph.InputAdapter;

namespace SceneInScene.Wpf
{
    public class SceneInSceneViewModel : ViewModelBase
    {
        
        // <Gnomons>
        private IGroup _gnomonRoot;
        public IGroup GnomonRoot
        {
            get => _gnomonRoot;
            set
            {
                _gnomonRoot = value;
                OnPropertyChanged("GnomonRoot");
            }
        }

        private Matrix4x4 _mainViewMatrix;
        public Matrix4x4 MainViewMatrix
        {
            get => _mainViewMatrix;
            set 
            {
                _mainViewMatrix = value;
                OnPropertyChanged("MainViewMatrix");
            }
        }
        // </Gnomons>
        
        internal SceneInSceneViewModel() : base()
        {
            SceneRoot = Examples.Common.PathExampleScene.Build();
            CameraManipulator = TrackballManipulator.Create();
            FsaaCount = TextureSampleCount.Count16;

            GnomonRoot = Group.Create();
            var gnomon = Gnomon.Create();
            GnomonRoot.AddChild(gnomon);
            
            EventHandler = new ViewMatrixEventHandler(this);
        }
    }
}