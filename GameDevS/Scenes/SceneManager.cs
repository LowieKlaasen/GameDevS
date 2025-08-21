using System.Collections.Generic;

namespace GameDevS.Scenes
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
