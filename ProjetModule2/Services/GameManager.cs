using Scenes;

namespace Services
{
    internal class GameManager
    {
        private int score;
        private int lives;
        private int level;

        public int Score => score;
        public int Lives => lives;
        public int Level => level;

        public GameManager()
        {
            score = 0;
            lives = 3;
            level = 1;
        }


        public void IncreaseScore(int amount)
        {
            score += amount;
        }

        public void IncreaseLives()
        {
            lives++;
        }

        public void DecreaseLives()
        {
            lives--;

            if (lives <= 0)
            {
                IScenesService scenesService = ServiceLocator.Get<IScenesService>();
                scenesService.Load<SceneGameOver>();
            }
        }

        public void IncreaseLevel()
        {
            level++;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetLives()
        {
            return lives;
        }

        public int GetLevel()
        {
            return level;
        }
    }
}
