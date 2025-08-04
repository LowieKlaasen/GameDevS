using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevS
{
    public class SceneManager
    {
        private Stack<IScene> scenes;

        public SceneManager()
        {
            scenes = new Stack<IScene>();
        }

        public void AddScene(IScene scene)
        {
            scene.Load();

            scenes.Push(scene);
        }
        public void RemoveScene()
        {
            scenes.Pop();
        }

        public IScene GetCurrentScene()
        {
            return scenes.Peek();
        }
    }
}
