# Josh2DEndlessShooter
Another Unity game inspired by the Galaxy Shooter game, that uses visual observations to train the agent using mlagents

# Training
## Reward system
One of the most essential part of a fast and successful training process is the reward system, which tells the agent if he's making progress or not.
## 1st try
In this game, I decided to reward my Agent for killing an enemy and anti-reward (punish) him when he touches a wall or gets killed by an enemy. He also gets a little bit punished for shooting so that he doesn't shoot all the time.
