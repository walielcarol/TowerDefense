## Concept
Le concept vient d'un livre (classic of mountains and seas) parlant des créatures imaginaires dans la montagne et la mer. Je prend la phoenix est le poisson méchant comme des creatures principales.

Le but de jeu est de jouer la partie Montagne contre la partie Mer, pour survivre le plus de temps possible, car les enemies viennent sans fin.

Le phoenix peut se voler du seul, ou bien constrire une flame avec son copain (cliquer deux phoenix). RabbitQueen peut attaquer plusieurs en même temps mais la vision est limité. RabbitKing peut attaquer un enemy à la distance. À remarquer, le FlyingFish peut être seulement attaqué par le phoenix.

La portal se trouve dans l'ElementPerso -- Scenes -- Menu.


## Script

### MoveableTower
Pour la classe de phoenix, en leur donnant la capacité d'attaquer les enemies en solo ou en équipe. Si tu clique deux phoenix, ils peuvent se construire un FireWall.

### MoveableTowerManager
Ce script permet de controler tous les phoenix dans la scène. Si un phoenix est en solo, il peux lui indiquer une base le plus proche de l'enemie pour s'intaller.

### Raycast
Ce script est attaché avec Camera, qui transmet les infos de souris pour 'MoveableTowerManager' grâce au 'event'.

### TimeManager
Ce script sert à noter le temps passé, pour indiquer au joueur au moment de 'GameOver'. Le damage de phoenix est lié (augementer) avec le temps passé aussi.

### RandomSpawn
À la fin de Wave que j'ai saisi, les enemies random vont arriver, avec une pause de plus en plus court.

### Others
Dans le script 'Damager', j'ai modifié pour que le damage de phoenix pourrait augmenter selon le temps qui passe.


## Visuel
J'essai d'utiliser le post-processing dans ce projet, mais le résultat en Game mode a toujours une différence de celui en Scene mode à cause du Render Mode du Canvas. J'arrive pas à resoudre ce conflit donc je le laisse tomber.

## Musique
La musique n'est pas ce que j'ai composé. Je le trouve sur Youtube car j'aime bien celui dans le Jeu Maplestory et je trouve cela rend coherent pour mon thème.




