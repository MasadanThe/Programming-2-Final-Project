using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Text;
using System.Net.Sockets;

namespace HOMMCopyCat_Spelprojekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private MouseState oldState;
        int screenWidth = 1000;
        int screenHeight = 700;
        //Textures
        Texture2D whiteTexture;
        Texture2D grassTexture;
        Texture2D cityTexture;
        Texture2D rockTexture;

        //Fighting
        Texture2D fightingGrass;
        FightingScreen fightingScreen1 = new FightingScreen();
        int enemyAttackDefense;
        bool yourTurn;
        int globalEnemyIndex;
        //Put the current enemy in a list
        Army currentEnemy = new Army(0);
        //Wanna be for-loop
        int troopTurn = 0;

        //Restart screen
        GameOverScreen gameover1 = new GameOverScreen();
        Texture2D restart;
        //Menu 1
        Texture2D exitChosen;
        Texture2D loadChosen;
        Texture2D newGameChosen;
        MainMenu mainMenu1 = new MainMenu();
        //Menu 2
        Texture2D map1Chosen;
        Texture2D map2Chosen;
        Texture2D easyChosen;
        Texture2D mediumChosen;
        Texture2D hardChosen;
        Texture2D backChosen;
        Texture2D beginChosen;
        NewGameMenu newGameMenu1 = new NewGameMenu();

        //Troops
        Texture2D pugilist;
        Texture2D slinger;
        Texture2D poisonMaster;
        Texture2D knight;
        Texture2D pyromancer;
        Texture2D enemy;

        //ESC screen
        Texture2D spara;
        ESCScreen escScreen1 = new ESCScreen();

        //Cityscreen 1
        CityScreen cityScreen1 = new CityScreen();

        //Town 1
        Town1 town1 = new Town1();

        //Resources screen & Nextturn related
        ResourceScreen player1Resources = new ResourceScreen();
        Texture2D nextTurn;
        private SpriteFont font;
        string resourceDraw;
        int addResources = 0;
        int day = 1;
        int totalDays = 0;

        //Buildings
        Texture2D pugilistBuilding;
        Texture2D slingerBuilding;
        Texture2D poisonMasterBuilding;
        Texture2D knightBuilding;
        Texture2D pyromancerBuilding;
        //Hero
        Boris hero = new Boris(1);
        Texture2D borisTexture;

        //Click related
        int state = 0;
        int difficulty = -1;
        int map = -1;
        //Mouse position
        int cursorX;
        int cursorY;
        double doubleCursorX;
        double doubleCursorY;
        //World
        World world1;

        //Random
        Random r = new Random();

        

        public Game1()
        {
            IsMouseVisible = true; //Gör musen synlig
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Resolution
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            currentEnemy.AddPugilist();
            currentEnemy.AddSlinger();
            currentEnemy.AddPoisonMaster();
            currentEnemy.AddKnight();
            currentEnemy.AddPyromancer();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            whiteTexture = Content.Load<Texture2D>("White");
            grassTexture = Content.Load<Texture2D>("Grass");
            cityTexture = Content.Load<Texture2D>("City");
            rockTexture = Content.Load<Texture2D>("Rock");

            //Restart
            restart = Content.Load<Texture2D>("Restart");

            //ESC screen
            spara = Content.Load<Texture2D>("Spara");

            //Menu 0
            exitChosen = Content.Load<Texture2D>("AvslutaVald");
            loadChosen = Content.Load<Texture2D>("LaddaInVald");
            newGameChosen = Content.Load<Texture2D>("NyttSpelVald");
            //Menu 2
            map1Chosen = Content.Load<Texture2D>("Bana1Vald");
            map2Chosen = Content.Load<Texture2D>("Bana2Vald");
            easyChosen = Content.Load<Texture2D>("LättVald");
            mediumChosen = Content.Load<Texture2D>("MedelVald");
            hardChosen = Content.Load<Texture2D>("SvårtVald");
            backChosen = Content.Load<Texture2D>("TillbakaVald");
            beginChosen = Content.Load<Texture2D>("StartaVald");

            //Resource screen
            font = Content.Load<SpriteFont>("File");
            nextTurn = Content.Load<Texture2D>("NextTurn");

            //Buildings
            pugilistBuilding = Content.Load<Texture2D>("PugilistBuilding");
            slingerBuilding = Content.Load<Texture2D>("SlingerBuilding");
            poisonMasterBuilding = Content.Load<Texture2D>("PoisonMasterBuilding");
            knightBuilding = Content.Load<Texture2D>("KnightBuilding");
            pyromancerBuilding = Content.Load<Texture2D>("PyromancerBuilding");

            //Troops
            pugilist = Content.Load<Texture2D>("Pugilist");
            slinger = Content.Load<Texture2D>("Slinger");
            poisonMaster = Content.Load<Texture2D>("PoisonMaster");
            knight = Content.Load<Texture2D>("Knight");
            pyromancer = Content.Load<Texture2D>("Pyromancer");
            enemy = Content.Load<Texture2D>("Enemy");

            //Hero
            borisTexture = Content.Load<Texture2D>("Boris");

            //Fighting textures
            fightingGrass = Content.Load<Texture2D>("FightingGrass");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //If the world has been created
            if (world1 != null)
            {
                //Checks if any enemy is left
                bool enemiesLeft = false;
                foreach (Enemy a in world1.armies)
                {
                    if (a.alive == true)
                    {
                        enemiesLeft = true;
                    }

                }

                if (enemiesLeft == false)
                {
                    state = 1000;
                }
            }
            // Kollar knapptryck
            KeyboardState keyboardState = Keyboard.GetState();
            // If they hit esc, exit
            if (keyboardState.IsKeyDown(Keys.Escape) && state == 5)
            {
                state = 100;
            }

            //Upgrades hero if possible
            hero.Upgrade();
            
            MouseState newState = Mouse.GetState();
            Debug.WriteLine(state);
            Debug.WriteLine("Hero attack: " + hero.attackP);
            Debug.WriteLine("Hero defense: " + hero.defendP);

            //Restart
            if (state == 50)
            {
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released) //Gör så att musen inte spammar sönder
                {
                    cursorX = newState.X; //Kollar musens x position vid tryck
                    cursorY = newState.Y; //Kollar musens y position vid tryck
                    Click(cursorX, cursorY, gameover1.Draw());
                }
            }
            //Fighting
            if (state == 20)
            {

                bool alive = false;
                bool enemyAlive = false;
                //Check if
                for (int aliveCheck = 0; aliveCheck < 5; aliveCheck++)
                {
                    if (hero.army.troops[aliveCheck].troopSize > 0)
                    {
                        alive = true;
                    }
                    if (currentEnemy.troops[aliveCheck].troopSize > 0)
                    {
                        enemyAlive = true;
                    }
                }
                if (alive == false)
                {
                    state = 50;
                }
                if (enemyAlive == false)
                {
                    //Sets enemy as dead
                    (world1.armies[globalEnemyIndex] as Enemy).alive = false;
                    //Goes back to map
                     state = 5;
                }
                Debug.WriteLine("Troop turn Value: " + troopTurn);
                //Checks if it is your turn and that troop is alive
                if (yourTurn == true && hero.army.troops[troopTurn].troopSize > 0)
                {

                    if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released) //Gör så att musen inte spammar sönder
                    {
                        bool enemy = false;
                        int enemyIndex = 0;
                        bool friendly = false;
                        cursorX = newState.X; //Kollar musens x position vid tryck
                        cursorY = newState.Y; //Kollar musens y position vid tryck
                        string[] location = Click(cursorX, cursorY, fightingScreen1.Draw()).Split('S');
                        double oldX = hero.army.troops[troopTurn].X / 100;
                        double oldY = hero.army.troops[troopTurn].Y / 100;
                        float newX = int.Parse(location[0]) / 100;
                        float newY = int.Parse(location[1]) / 100;

                        //Avståndsformeln
                        double distance = Math.Sqrt((oldX - newX) * (oldX - newX) + (oldY - newY) * (oldY - newY));

                        //Checks if there already is an enemy or friendly on the spot
                        for (int g = 0; g < 5; g++)
                        {
                            if (currentEnemy.troops[g].troopSize > 0 && currentEnemy.troops[g].X == int.Parse(location[0]) && currentEnemy.troops[g].Y == int.Parse(location[1]))
                            {
                                enemy = true;
                                enemyIndex = g;
                            }
                            if (hero.army.troops[g].troopSize > 0 && hero.army.troops[g].X == int.Parse(location[0]) && hero.army.troops[g].Y == int.Parse(location[1]))
                            {
                                friendly = true;
                            }
                        }

                        //Checks if the troop is ranged or melee
                        if (hero.army.troops[troopTurn].ranged == true)
                        {
                            //If you clicked on an enemy that is alive
                            if (enemy == true)
                            {
                                //Damage and send xp to hero
                                hero.EarnXP(currentEnemy.troops[enemyIndex].Defense(hero.army.troops[troopTurn].Damage(hero.attackP), enemyAttackDefense));
                                //Make it enemy turn
                                yourTurn = false;
                            }
                            //If you did not click on an enemy nor a friendly
                            else if (friendly != true)
                            {
                                if (distance <= hero.army.troops[troopTurn].movementSpeed)
                                {
                                    //Sets new position
                                    hero.army.troops[troopTurn].X = int.Parse(location[0]);
                                    hero.army.troops[troopTurn].Y = int.Parse(location[1]);
                                    //Make it enemy turn
                                    yourTurn = false;
                                }
                            }
                        }
                        else
                        {
                            //If you clicked on an enemy that is alive
                            if (enemy == true)
                            {

                                //If you stand beside him
                                if (distance <= Math.Sqrt(2))
                                {
                                    //Damage and send xp to hero
                                    hero.EarnXP(currentEnemy.troops[enemyIndex].Defense(hero.army.troops[troopTurn].Damage(hero.attackP), enemyAttackDefense));

                                    //Make it enemy turn
                                    yourTurn = false;
                                }

                            }
                            //If you did not click on an enemy nor a friendly
                            else if (friendly != true)
                            {
                                if (distance <= hero.army.troops[troopTurn].movementSpeed)
                                {
                                    //Sets new position
                                    hero.army.troops[troopTurn].X = int.Parse(location[0]);
                                    hero.army.troops[troopTurn].Y = int.Parse(location[1]);

                                    //Make it enemy turn
                                    yourTurn = false;
                                }
                            }
                        }
                    }
                    //Återställer stadiet
                    oldState = newState;
                }
                //Checks if it is your turn but that troop is not alive
                else if (yourTurn == true && hero.army.troops[troopTurn].troopSize <= 0)
                {
                    //Make it enemy turn
                    yourTurn = false;
                }
                //Checks if it is enemy turn and that troop is alive
                else if (yourTurn == false && currentEnemy.troops[troopTurn].troopSize > 0)
                {
                    double enemyDistance = 1000000000;
                    double distance = 0;
                    int enemyIndex = 0;
                    //Iterates through the heroes army
                    for (int number = 0; number < 5; number++)
                    {
                        //If the enemy is alive
                        if (hero.army.troops[number].troopSize > 0)
                        {
                            //Checks the distance
                            distance = Math.Sqrt((currentEnemy.troops[troopTurn].X - hero.army.troops[number].X) * (currentEnemy.troops[troopTurn].X - hero.army.troops[number].X)
                            + (currentEnemy.troops[troopTurn].Y - hero.army.troops[number].Y) * (currentEnemy.troops[troopTurn].Y - hero.army.troops[number].Y));

                            //If that enemy is closer
                            if (enemyDistance > distance)
                            {
                                enemyDistance = distance;
                                enemyIndex = number;
                            }
                        }
                        
                        
                    }
                    //If the ai is ranged
                    if (currentEnemy.troops[troopTurn].ranged == true)
                    {
                        //Damage
                        Debug.WriteLine("Enemy Index: " + enemyIndex);
                        hero.army.troops[enemyIndex].Defense(currentEnemy.troops[troopTurn].Damage(enemyAttackDefense), hero.defendP);
                        //Make it your turn
                        yourTurn = true;
                    }
                    //If the ai is melee
                    else
                    {
                        //If the ai can hit
                        if (enemyDistance/100 <= Math.Sqrt(2))
                        {
                            // Damage
                            hero.army.troops[enemyIndex].Defense(currentEnemy.troops[troopTurn].Damage(enemyAttackDefense), hero.defendP);

                            //Make it your turn
                            yourTurn = true;
                        }
                        else
                        {
                            for (int move = 0; move < currentEnemy.troops[troopTurn].movementSpeed; move++)
                            {
                                bool character = false;
                                double differenceX = currentEnemy.troops[troopTurn].X - hero.army.troops[enemyIndex].X;
                                double differenceY = currentEnemy.troops[troopTurn].Y - hero.army.troops[enemyIndex].Y;

                                //If the enemy is to the left
                                if (differenceX > 100)
                                {

                                    for (int moveCheck = 0; moveCheck < 5; moveCheck++)
                                    {
                                        if (currentEnemy.troops[moveCheck].troopSize > 0 && currentEnemy.troops[moveCheck].X == currentEnemy.troops[troopTurn].X - 100 && currentEnemy.troops[moveCheck].Y == currentEnemy.troops[troopTurn].Y && hero.army.troops[moveCheck].troopSize > 0 && hero.army.troops[moveCheck].X == currentEnemy.troops[troopTurn].X - 100 && hero.army.troops[moveCheck].Y == currentEnemy.troops[troopTurn].Y)
                                        {
                                            character = true;
                                        }
                                    }

                                    if (character == false)
                                    {
                                        currentEnemy.troops[troopTurn].X -= 100;
                                    }
                                }
                                //If the enemy is to the right
                                if (differenceX < -100)
                                {

                                    for (int moveCheck = 0; moveCheck < 5; moveCheck++)
                                    {
                                        if (currentEnemy.troops[moveCheck].troopSize > 0 && currentEnemy.troops[moveCheck].X == currentEnemy.troops[troopTurn].X + 100 && currentEnemy.troops[moveCheck].Y == currentEnemy.troops[troopTurn].Y && hero.army.troops[moveCheck].troopSize > 0 && hero.army.troops[moveCheck].X == currentEnemy.troops[troopTurn].X + 100 && hero.army.troops[moveCheck].Y == currentEnemy.troops[troopTurn].Y)
                                        {
                                            character = true;
                                        }
                                    }

                                    if (character == false)
                                    {
                                        currentEnemy.troops[troopTurn].X += 100;
                                    }
                                }
                                //If the enemy is above
                                if (differenceY > 100)
                                {

                                    for (int moveCheck = 0; moveCheck < 5; moveCheck++)
                                    {
                                        if (currentEnemy.troops[moveCheck].troopSize > 0 && currentEnemy.troops[moveCheck].X == currentEnemy.troops[troopTurn].X && currentEnemy.troops[moveCheck].Y == currentEnemy.troops[troopTurn].Y - 100&& hero.army.troops[moveCheck].troopSize > 0 && hero.army.troops[moveCheck].X == currentEnemy.troops[troopTurn].X && hero.army.troops[moveCheck].Y == currentEnemy.troops[troopTurn].Y - 100)
                                        {
                                            character = true;
                                        }
                                    }

                                    if (character == false)
                                    {
                                        currentEnemy.troops[troopTurn].Y -= 100;
                                    }
                                }
                                //If the enemy is under
                                if (differenceY < -100)
                                {

                                    for (int moveCheck = 0; moveCheck < 5; moveCheck++)
                                    {
                                        if (currentEnemy.troops[moveCheck].troopSize > 0 && currentEnemy.troops[moveCheck].X == currentEnemy.troops[troopTurn].X && currentEnemy.troops[moveCheck].Y == currentEnemy.troops[troopTurn].Y + 100 && hero.army.troops[moveCheck].troopSize > 0 && hero.army.troops[moveCheck].X == currentEnemy.troops[troopTurn].X && hero.army.troops[moveCheck].Y == currentEnemy.troops[troopTurn].Y + 100)
                                        {
                                            character = true;
                                        }
                                    }

                                    if (character == false)
                                    {
                                        currentEnemy.troops[troopTurn].Y += 100;
                                    }
                                }

                            }
                            yourTurn = true;
                        }

                    }

                    //Next troop
                    if (troopTurn == 4)
                    {
                        troopTurn = 0;
                    }
                    else
                    {
                        troopTurn++;
                    }
                }
                //Checks if it is enemy turn but that troop is not alive
                else if (yourTurn == false && currentEnemy.troops[troopTurn].troopSize <= 0)
                {
                    //Next troop
                    if (troopTurn == 4)
                    {
                        troopTurn = 0;
                    }
                    else
                    {
                        troopTurn++;
                    }
                    
                    //Make it enemy turn
                    yourTurn = true;
                }

            }


            else
            {
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released) //Gör så att musen inte spammar sönder
                {
                    cursorX = newState.X; //Kollar musens x position vid tryck
                    cursorY = newState.Y; //Kollar musens y position vid tryck
                    switch (state) //Check if mouse clicked on something clickable
                    {
                        //Mainmenu
                        case 0:
                            Click(cursorX, cursorY, mainMenu1.Draw());
                            break;
                        //Secondary meny
                        case 1:
                            Click(cursorX, cursorY, newGameMenu1.Draw());
                            break;

                        //First map
                        case 5:

                            if (Click(cursorX, cursorY, player1Resources.Draw()) == "5")
                            {
                                addResources = 1;
                                
                            }
                            if (Click(cursorX, cursorY, world1.Draw()) != "1")
                            {
                                //Räkna ut om man kan gå ett visst avstånd
                                string texture = Click(cursorX, cursorY, world1.Draw());
                                //If you are not clicking on the city
                                if (texture != "0")
                                {
                                    string[] coordinates = texture.Split('h');
                                    int oldX = hero.xPos / 20;
                                    int oldY = hero.yPos / 20;
                                    int newX = int.Parse(coordinates[0]) / 20;
                                    int newY = int.Parse(coordinates[1]) / 20;
                                    int distance = Convert.ToInt32((Math.Ceiling(Math.Sqrt((oldX - newX) * (oldX - newX) + (oldY - newY) * (oldY - newY)))));//Avståndsformeln och avrunda ruppåt
                                    Debug.WriteLine("Distance: " + distance);//Debug
                                    if (distance <= hero.movementSpeed) //Avståndsformeln
                                    {
                                        //Move the hero
                                        hero.xPos = int.Parse(coordinates[0]);
                                        hero.yPos = int.Parse(coordinates[1]);
                                        hero.movementSpeed -= distance;
                                        for(int k = 0; k < world1.armies.Count; k++)
                                        {
                                            //Om det är en fiende
                                            if (hero.xPos == (world1.armies[k] as Enemy).X && hero.yPos == (world1.armies[k] as Enemy).Y && (world1.armies[k] as Enemy).alive == true)
                                            {
                                                state = 20;
                                                globalEnemyIndex = k;
                                                
                                            }
                                            //Ger trupperna till currentEnemy
                                            for (int n = 0; n < 5; n++)
                                            {
                                                //Sets enemy values and starting values for a fight
                                                currentEnemy.troops[n].troopSize = (world1.armies[globalEnemyIndex] as Enemy).troops[n].troopSize;
                                                Debug.WriteLine("Troops: " + currentEnemy.troops[n].troopName + " " + + currentEnemy.troops[n].troopSize);
                                                currentEnemy.troops[n].SetTotalHealth();
                                                currentEnemy.troops[n].ResetPosition();

                                                hero.army.troops[n].ResetPosition();
                                                hero.army.troops[n].SetTotalHealth();
                                                yourTurn = true;
                                                troopTurn = 0;

                                            }

                                        }
                                    }
                                }

                            }

                            break;

                        //CityScreen
                        case 10:
                            Click(cursorX, cursorY, cityScreen1.Draw());
                            //Sends back to map
                            if (Click(cursorX, cursorY, player1Resources.Draw()) == "5")
                            {
                                    state = 5;
                            }
                            break;
                        //Esc screen
                        case 100:
                            Click(cursorX, cursorY, escScreen1.Draw());
                            break;

                    }
                    Debug.WriteLine("X = " + cursorX); //Debug för att se musens x postion vid tryck
                    Debug.WriteLine("Y = " + cursorY); //Debug för att se musens y postion vid tryck
                }
                oldState = newState; //Återställer stadiet

                //New turn
                if (addResources == 1)
                {
                    //Adds the total days
                    totalDays++;
                    //Knows it is a new week
                    if (day == 7)
                    {
                        day = 1;
                        foreach (MilitaryBuilding b in town1.EnterTown())
                        {
                            b.weeklyBoughtTroops = 0;
                        }

                        //Ökar trupperna i fienden baserat på svårighetsgrad
                        foreach (Enemy e in world1.armies)
                        {
                            if (difficulty == 0)
                            {
                                e.troops[0].troopSize = Math.Ceiling(e.troops[0].troopSize * 1.25);
                                e.troops[1].troopSize = Math.Ceiling(e.troops[1].troopSize * 1.25);
                                e.troops[2].troopSize = Math.Ceiling(e.troops[2].troopSize * 1.25);
                                e.troops[3].troopSize = Math.Ceiling(e.troops[3].troopSize * 1.25);
                                e.troops[4].troopSize = Math.Ceiling(e.troops[4].troopSize * 1.25);
                            }
                            else if (difficulty == 1)
                            {
                                e.troops[0].troopSize = Math.Ceiling(e.troops[0].troopSize * 1.5);
                                e.troops[1].troopSize = Math.Ceiling(e.troops[1].troopSize * 1.5);
                                e.troops[2].troopSize = Math.Ceiling(e.troops[2].troopSize * 1.5);
                                e.troops[3].troopSize = Math.Ceiling(e.troops[3].troopSize * 1.5);
                                e.troops[4].troopSize = Math.Ceiling(e.troops[4].troopSize * 1.5);
                            }
                            else
                            {
                                e.troops[0].troopSize = Math.Ceiling(e.troops[0].troopSize * 2);
                                e.troops[1].troopSize = Math.Ceiling(e.troops[1].troopSize * 2);
                                e.troops[2].troopSize = Math.Ceiling(e.troops[2].troopSize * 2);
                                e.troops[3].troopSize = Math.Ceiling(e.troops[3].troopSize * 2);
                                e.troops[4].troopSize = Math.Ceiling(e.troops[4].troopSize * 2);
                            }
                        }
                    }
                    //Normal day
                    else
                    {
                        day++;
                    }
                    //Get more resources
                    foreach (Resources t in player1Resources.Resources())
                    {
                        t.Add();
                    }
                    //Reset movementspeed
                    hero.movementSpeed = hero.maxMovementSpeed;
                    addResources = 0;

                    //Connect
                    try
                    {
                        //Send data
                        TcpClient tcpClient = new TcpClient();
                        string address = "127.0.0.1";
                        int port = 8001;
                        tcpClient.Connect(address, port);
                        //Convert info to byte and send it
                        Byte[] bMessage = Encoding.UTF8.GetBytes("Gold: " + player1Resources.resources[0].value.ToString());
                        Socket socket = tcpClient.Client;
                        socket.Send(bMessage);
                    }
                    catch
                    {

                    }
                }
            }


            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (state == 0)
            {
                //Make white background
                for (int y = 0; y < screenHeight; y += 20)
                {
                    for (int x = 0; x < screenWidth; x += 20)
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(x, y));
                    }
                }
                //Draw buttons
                foreach (Texture t in mainMenu1.Draw())
                {
                    if (t.Draw() == "AvslutaVald")
                    {
                        spriteBatch.Draw(exitChosen, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "LaddaInVald")
                    {
                        spriteBatch.Draw(loadChosen, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "NyttSpelVald")
                    {
                        spriteBatch.Draw(newGameChosen, new Vector2(t.X, t.Y));
                    }
                }
            }

            if (state == 1)
            {
                //Make white background
                for (int y = 0; y < screenHeight; y += 20)
                {
                    for (int x = 0; x < screenWidth; x += 20)
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(x, y));
                    }
                }
                //Draw buttons
                foreach (Texture t in newGameMenu1.Draw())
                {
                    switch (t.Draw())
                    {
                        case "LättVald":
                            spriteBatch.Draw(easyChosen, new Vector2(t.X, t.Y));
                            break;
                        case "MedelVald":
                            spriteBatch.Draw(mediumChosen, new Vector2(t.X, t.Y));
                            break;
                        case "SvårtVald":
                            spriteBatch.Draw(hardChosen, new Vector2(t.X, t.Y));
                            break;
                        case "Bana1Vald":
                            spriteBatch.Draw(map1Chosen, new Vector2(t.X, t.Y));
                            break;
                        case "Bana2Vald":
                            spriteBatch.Draw(map2Chosen, new Vector2(t.X, t.Y));
                            break;
                        case "TillbakaVald":
                            spriteBatch.Draw(backChosen, new Vector2(t.X, t.Y));
                            break;
                        case "StartaVald":
                            spriteBatch.Draw(beginChosen, new Vector2(t.X, t.Y));
                            break;
                    }
                }

                //Draw the square that shows which button is selected
                if (map == 0)
                {
                    spriteBatch.Draw(grassTexture, new Vector2(600, 100));
                }
                else if (map == 1)
                {
                    spriteBatch.Draw(grassTexture, new Vector2(600, 200));
                }

                if (difficulty == 0)
                {
                    spriteBatch.Draw(grassTexture, new Vector2(200, 100));
                }
                else if (difficulty == 1)
                {
                    spriteBatch.Draw(grassTexture, new Vector2(200, 200));
                }
                else if (difficulty == 2)
                {
                    spriteBatch.Draw(grassTexture, new Vector2(200, 300));
                }

            }

            //Draw first map
            if (state == 5) {
                foreach (Texture t in world1.Draw())
                {
                    if (t.Draw() == "whiteTexture")
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "cityTexture")
                    {
                        spriteBatch.Draw(cityTexture, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "grassTexture")
                    {
                        spriteBatch.Draw(grassTexture, new Vector2(t.X, t.Y));
                    }
                    
                }

                //Draws out enemy
                foreach (Enemy e in world1.armies)
                {
                    if (e.Draw() == "enemyTexture" && e.alive == true)
                    {
                        spriteBatch.Draw(enemy, new Vector2(e.X, e.Y));
                    }
                }

                //Make white background for the text
                for (int y = 600; y < screenHeight; y += 20)
                {
                    for (int x = 0; x < screenWidth; x += 20)
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(x, y));
                    }
                }

                resourceDraw = "";//Resets resource variable
                foreach (Resources t in player1Resources.Resources())
                {
                    resourceDraw += t.Value() + "   ";
                }
                //Draw the text
                spriteBatch.DrawString(font, resourceDraw + totalDays.ToString() + " Days", new Vector2(10, 625), Color.Black);

                //Next turn button
                foreach (Texture t in player1Resources.Draw())
                {
                    spriteBatch.Draw(nextTurn, new Vector2(t.X, t.Y));
                }

                if (hero.name == "Boris")
                {
                    spriteBatch.Draw(borisTexture, new Vector2(hero.xPos, hero.yPos));
                }
            }

            //Gameover screen
            if (state == 50)
            {
                foreach (Texture t in gameover1.Draw())
                {
                    if (t.Draw() == "whiteTexture")
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "Restart")
                    {
                        spriteBatch.Draw(restart, new Vector2(t.X, t.Y));
                    }

                }
                spriteBatch.DrawString(font, "Game Over", new Vector2(400, 100), Color.Black);
            }

            if (state == 1000)
            {
                foreach (Texture t in gameover1.Draw())
                {
                    if (t.Draw() == "whiteTexture")
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "Restart")
                    {
                        spriteBatch.Draw(restart, new Vector2(t.X, t.Y));
                    }
                }
                spriteBatch.DrawString(font, "Victory", new Vector2(400, 100), Color.Black);
            }

            //Esc screen
            if(state == 100)
            {
                foreach (Texture t in escScreen1.Draw())
                {
                    if (t.Draw() == "whiteTexture")
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "Restart")
                    {
                        spriteBatch.Draw(restart, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "Spara")
                    {
                        spriteBatch.Draw(spara, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "AvslutaVald")
                    {
                        spriteBatch.Draw(exitChosen, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "TillbakaVald")
                    {
                        spriteBatch.Draw(backChosen, new Vector2(t.X, t.Y));
                    }

                }
            }

            //Draws inside city
            if (state == 10)
            {
                foreach (Texture t in cityScreen1.Draw())
                {
                    if (t.Draw() == "whiteTexture")
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(t.X, t.Y));
                    }
                    if (t.Draw() == "PugilistBuilding")
                    {
                        spriteBatch.Draw(pugilistBuilding, new Vector2(t.X, t.Y));
                        if (town1.EnterTown()[0].built == true)
                        {
                            //Draw checkmark
                            spriteBatch.Draw(grassTexture, new Vector2(t.X, t.Y));
                        }
                    }
                    if (t.Draw() == "SlingerBuilding")
                    {
                        spriteBatch.Draw(slingerBuilding, new Vector2(t.X, t.Y));
                        if (town1.EnterTown()[1].built == true)
                        {
                            //Draw checkmark
                            spriteBatch.Draw(grassTexture, new Vector2(t.X, t.Y));
                        }
                    }
                    if (t.Draw() == "PoisonMasterBuilding")
                    {
                        spriteBatch.Draw(poisonMasterBuilding, new Vector2(t.X, t.Y));
                        if (town1.EnterTown()[2].built == true)
                        {
                            //Draw checkmark
                            spriteBatch.Draw(grassTexture, new Vector2(t.X, t.Y));
                        }
                    }
                    if (t.Draw() == "KnightBuilding")
                    {
                        spriteBatch.Draw(knightBuilding, new Vector2(t.X, t.Y));
                        if (town1.EnterTown()[3].built == true)
                        {
                            //Draw checkmark
                            spriteBatch.Draw(grassTexture, new Vector2(t.X, t.Y));
                        }
                    }
                    if (t.Draw() == "PyromancerBuilding")
                    {
                        spriteBatch.Draw(pyromancerBuilding, new Vector2(t.X, t.Y));
                        if (town1.EnterTown()[4].built == true)
                        {
                            //Draw checkmark
                            spriteBatch.Draw(grassTexture, new Vector2(t.X, t.Y));
                        }
                    }

                    if (t.Draw() == "Pugilist")
                    {
                        //Draw character
                        spriteBatch.Draw(pugilist, new Vector2(t.X, t.Y));
                        //Draw troop purshaseability
                        //Eftersom att det är en lista från Buildings så måste man säga till att det är en Militarybuilding
                        spriteBatch.DrawString(font, (town1.EnterTown()[0] as MilitaryBuilding).AvailableTroops().ToString(), new Vector2(t.X, (t.Y + 120)), Color.Black);
                            
                        //Draw troop size
                        spriteBatch.DrawString(font, hero.army.troops[0].troopSize.ToString(), new Vector2(t.X, t.Y + 200), Color.Black);
                    }
                    if (t.Draw() == "Slinger")
                    {
                        //Draw character
                        spriteBatch.Draw(slinger, new Vector2(t.X, t.Y));
                        //Draw troop purshaseability
                        //Eftersom att det är en lista från Buildings så måste man säga till att det är en Militarybuilding
                        spriteBatch.DrawString(font, (town1.EnterTown()[1] as MilitaryBuilding).AvailableTroops().ToString(), new Vector2(t.X, (t.Y + 120)), Color.Black);
                        //Draw troop size
                        spriteBatch.DrawString(font, hero.army.troops[1].troopSize.ToString(), new Vector2(t.X, t.Y + 200), Color.Black);
                    }
                    if (t.Draw() == "PoisonMaster")
                    {
                        //Draw character
                        spriteBatch.Draw(poisonMaster, new Vector2(t.X, t.Y));
                        //Draw troop purshaseability
                        //Eftersom att det är en lista från Buildings så måste man säga till att det är en Militarybuilding
                        spriteBatch.DrawString(font, (town1.EnterTown()[2] as MilitaryBuilding).AvailableTroops().ToString(), new Vector2(t.X, (t.Y + 120)), Color.Black);
                        //Draw troop size
                        spriteBatch.DrawString(font, hero.army.troops[2].troopSize.ToString(), new Vector2(t.X, t.Y + 200), Color.Black);
                    }
                    if (t.Draw() == "Knight")
                    {
                        //Draw character
                        spriteBatch.Draw(knight, new Vector2(t.X, t.Y));
                        //Draw troop purshaseability
                        //Eftersom att det är en lista från Buildings så måste man säga till att det är en Militarybuilding
                        spriteBatch.DrawString(font, (town1.EnterTown()[3] as MilitaryBuilding).AvailableTroops().ToString(), new Vector2(t.X, (t.Y + 120)), Color.Black);
                        //Draw troop size
                        spriteBatch.DrawString(font, hero.army.troops[3].troopSize.ToString(), new Vector2(t.X, t.Y + 200), Color.Black);
                    }
                    if (t.Draw() == "Pyromancer")
                    {
                        //Draw character
                        spriteBatch.Draw(pyromancer, new Vector2(t.X, t.Y));
                        //Draw troop purshaseability
                        //Eftersom att det är en lista från Buildings så måste man säga till att det är en Militarybuilding
                        spriteBatch.DrawString(font, (town1.EnterTown()[4] as MilitaryBuilding).AvailableTroops().ToString(), new Vector2(t.X, (t.Y + 120)), Color.Black);
                        //Draw troop size
                        spriteBatch.DrawString(font, hero.army.troops[4].troopSize.ToString(), new Vector2(t.X, t.Y + 200), Color.Black);
                    }
                }
                //Make white background for the text
                for (int y = 600; y < screenHeight; y += 20)
                {
                    for (int x = 0; x < screenWidth; x += 20)
                    {
                        spriteBatch.Draw(whiteTexture, new Vector2(x, y));
                    }
                }
                //Resets resource variable
                resourceDraw = "";
                foreach (Resources t in player1Resources.Resources())
                {
                    resourceDraw += t.Value() + "   ";
                }
                //Draw the text
                spriteBatch.DrawString(font, resourceDraw + totalDays.ToString() + " Days", new Vector2(10, 625), Color.Black);

                //Exit button button
                foreach (Texture t in player1Resources.Draw())
                {
                    spriteBatch.Draw(nextTurn, new Vector2(t.X, t.Y));
                }
            }

            if (state == 20)
            {
                
                //Checks where the mouse is
                MouseState mouseState = Mouse.GetState();
                doubleCursorX = mouseState.X;
                doubleCursorY = mouseState.Y;
                //Debug.WriteLine(Math.Floor(doubleCursorX / 100) * 100 + "   " + Math.Floor(doubleCursorY / 100) * 100);
                //Display grass
                foreach (Texture t in fightingScreen1.Draw())
                {
                    if (t.Draw() == "fightingGrassTexture")
                    {
                        spriteBatch.Draw(fightingGrass, new Vector2(t.X, t.Y));
                    }
                }

                //Display friendly troops
                hero.army.troops[0].Draw(spriteBatch, pugilist);
                hero.army.troops[1].Draw(spriteBatch, slinger);
                hero.army.troops[2].Draw(spriteBatch, poisonMaster);
                hero.army.troops[3].Draw(spriteBatch, knight);
                hero.army.troops[4].Draw(spriteBatch, pyromancer);
                //Display enemy troops
                currentEnemy.troops[0].Draw(spriteBatch, pugilist);
                currentEnemy.troops[1].Draw(spriteBatch, slinger);
                currentEnemy.troops[2].Draw(spriteBatch, poisonMaster);
                currentEnemy.troops[3].Draw(spriteBatch, knight);
                currentEnemy.troops[4].Draw(spriteBatch, pyromancer);
                //Checks if I am holding the cursor over a friendly troop that is alive, rounded to the nearest hundred
                for (int i = 0; i < hero.army.troops.Count; i++)
                {
                    if (hero.army.troops[i].X == Math.Floor(doubleCursorX / 100) * 100 && hero.army.troops[i].Y == Math.Floor(doubleCursorY / 100) * 100 && hero.army.troops[i].troopSize > 0)
                    {
                        //Display friendly troops
                        hero.army.troops[0].DrawText(spriteBatch, font, pugilist);
                        hero.army.troops[1].DrawText(spriteBatch, font, slinger);
                        hero.army.troops[2].DrawText(spriteBatch, font, poisonMaster);
                        hero.army.troops[3].DrawText(spriteBatch, font, knight);
                        hero.army.troops[4].DrawText(spriteBatch, font, pyromancer);

                    }
                }
                //Checks if I am holding the cursor over a enemy troop that is alive, rounded to the nearest hundred
                for (int i = 0; i < currentEnemy.troops.Count; i++)
                {
                    if (currentEnemy.troops[i].X == Math.Floor(doubleCursorX / 100) * 100 && currentEnemy.troops[i].Y == Math.Floor(doubleCursorY / 100) * 100 & currentEnemy.troops[i].troopSize > 0)
                    {
                        //Display enemy troops
                        currentEnemy.troops[0].DrawText(spriteBatch, font, pugilist);
                        currentEnemy.troops[1].DrawText(spriteBatch, font, slinger);
                        currentEnemy.troops[2].DrawText(spriteBatch, font, poisonMaster);
                        currentEnemy.troops[3].DrawText(spriteBatch, font, knight);
                        currentEnemy.troops[4].DrawText(spriteBatch, font, pyromancer);

                    }
                }
                //Draws player turn
                if (hero.army.troops[troopTurn].troopSize > 0)
                {
                    //Draw checkmark
                    spriteBatch.Draw(grassTexture, new Vector2(hero.army.troops[troopTurn].X, hero.army.troops[troopTurn].Y));
                }
            }

            if (state == 50)
            {

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        string Click(int cursorX, int cursorY, List<Texture> texture)
        {
            foreach (Texture t in texture)
            {
                for (int y = t.Y; y < t.Y + t.Height; y++)
                {
                    for (int x = t.X; x < t.X + t.Width; x++)
                    {
                        if (cursorX == x && cursorY == y)
                        {
                            switch (t.Draw())
                            {
                                case "Restart":
                                    Restart();
                                    break;
                                case "NyttSpelVald":
                                    state = 1;
                                    return "1";
                                    break;
                                case "AvslutaVald":
                                    Exit();
                                    return "1";
                                    break;
                                case "Spara":
                                    Save();
                                    state = 5;
                                    break;
                                case "TillbakaVald":
                                    if(state == 100)
                                    {
                                        state = 5;
                                    }
                                    else
                                    { 
                                    state = state - 1;
                                    }
                                    return "1";
                                    break;
                                case "LättVald":
                                    difficulty = 0;
                                    return "1";
                                    break;
                                case "MedelVald":
                                    difficulty = 1;
                                    return "1";
                                    break;
                                case "SvårtVald":
                                    difficulty = 2;
                                    return "1";
                                    break;
                                case "Bana1Vald":
                                    map = 0;
                                    return "1";
                                    break;
                                case "Bana2Vald":
                                    map = 1;
                                    return "1";
                                    break;
                                case "StartaVald":
                                    if (map != -1 && difficulty != -1)
                                    {
                                        if (map == 0)
                                        {
                                            //Set starting point for hero
                                            hero.xPos = 80;
                                            hero.yPos = 140;

                                            //Set starting point for map
                                            world1 = new World(60, 80, hero.xPos, hero.yPos);
                                            state = 5;
                                            //Sets start troops based on difficulty
                                            if (difficulty == 0)
                                            {
                                                enemyAttackDefense = 0;
                                                hero.army.troops[0].troopSize = 50;
                                            }
                                            else if (difficulty == 1)
                                            {
                                                enemyAttackDefense = 2;
                                                hero.army.troops[0].troopSize = 40;
                                            }
                                            else
                                            {
                                                enemyAttackDefense = 5;
                                                hero.army.troops[0].troopSize = 30;
                                            }
                                            return "1";

                                        }
                                        if (map == 1)
                                        {
                                            //Set starting point for hero
                                            hero.xPos = 680;
                                            hero.yPos = 520;

                                            //Set starting point for map
                                            world1 = new World(660, 460, hero.xPos, hero.yPos);
                                            state = 5;
                                            //Sets start troops based on difficulty
                                            if (difficulty == 0)
                                            {
                                                enemyAttackDefense = 0;
                                                hero.army.troops[0].troopSize = 50;
                                            }
                                            else if (difficulty == 1)
                                            {
                                                enemyAttackDefense = 2;
                                                hero.army.troops[0].troopSize = 40;
                                            }
                                            else
                                            {
                                                enemyAttackDefense = 5;
                                                hero.army.troops[0].troopSize = 30;
                                            }
                                            return "1";
                                        }

                                    }
                                    break;
                                case "LaddaInVald":
                                    if (Properties.Settings1.Default.NumberList != null)
                                    {
                                        Load();
                                        state = 5;
                                    }
                                    return "1";
                                    break;
                                case "NextTurn":
                                    return "5";
                                    break;
                                case "grassTexture":
                                    return (t.X + "h" + t.Y).ToString();
                                    break;
                                case "cityTexture":
                                    if (Math.Abs(hero.xPos - t.X) <= 20 && Math.Abs(hero.yPos - t.Y) <= 20)
                                    {
                                        state = 10;
                                    }
                                    
                                    return "0";
                                    break;

                               //The buildings
                                case "PugilistBuilding":
                                    //Checks if you have enough resources and if it is not already build
                                    if (player1Resources.Resources()[0].value >= town1.EnterTown()[0].goldCost && player1Resources.Resources()[1].value >= town1.EnterTown()[0].woodCost &&
                                        player1Resources.Resources()[2].value >= town1.EnterTown()[0].stoneCost && town1.EnterTown()[0].built == false)
                                    {
                                        //Removes the resources and makes the building built = true
                                        player1Resources.Resources()[0].value -= town1.EnterTown()[0].goldCost;
                                        player1Resources.Resources()[1].value -= town1.EnterTown()[0].woodCost;
                                        player1Resources.Resources()[2].value -= town1.EnterTown()[0].stoneCost;
                                        town1.EnterTown()[0].built = true;
                                    }
                                    return "PugilistBuilding";
                                    break;
                                case "SlingerBuilding":
                                    //Checks if you have enough resources and if it is not already build
                                    if (player1Resources.Resources()[0].value >= town1.EnterTown()[1].goldCost && player1Resources.Resources()[1].value >= town1.EnterTown()[1].woodCost &&
                                        player1Resources.Resources()[2].value >= town1.EnterTown()[1].stoneCost && town1.EnterTown()[1].built == false)
                                    {
                                        //Removes the resources and makes the building built = true
                                        player1Resources.Resources()[0].value -= town1.EnterTown()[1].goldCost;
                                        player1Resources.Resources()[1].value -= town1.EnterTown()[1].woodCost;
                                        player1Resources.Resources()[2].value -= town1.EnterTown()[1].stoneCost;
                                        town1.EnterTown()[1].built = true;
                                    }
                                    return "SlingerBuilding";
                                    break;
                                case "PoisonMasterBuilding":
                                    //Checks if you have enough resources and if it is not already build
                                    if (player1Resources.Resources()[0].value >= town1.EnterTown()[2].goldCost && player1Resources.Resources()[1].value >= town1.EnterTown()[2].woodCost &&
                                        player1Resources.Resources()[2].value >= town1.EnterTown()[2].stoneCost && town1.EnterTown()[2].built == false)
                                    {
                                        //Removes the resources and makes the building built = true
                                        player1Resources.Resources()[0].value -= town1.EnterTown()[2].goldCost;
                                        player1Resources.Resources()[1].value -= town1.EnterTown()[2].woodCost;
                                        player1Resources.Resources()[2].value -= town1.EnterTown()[2].stoneCost;
                                        town1.EnterTown()[2].built = true;
                                    }
                                    return "PoisonMasterBuilding";
                                    break;
                                case "KnightBuilding":
                                    //Checks if you have enough resources and if it is not already build
                                    if (player1Resources.Resources()[0].value >= town1.EnterTown()[3].goldCost && player1Resources.Resources()[1].value >= town1.EnterTown()[3].woodCost &&
                                        player1Resources.Resources()[2].value >= town1.EnterTown()[3].stoneCost && town1.EnterTown()[3].built == false)
                                    {
                                        //Removes the resources and makes the building built = true
                                        player1Resources.Resources()[0].value -= town1.EnterTown()[3].goldCost;
                                        player1Resources.Resources()[1].value -= town1.EnterTown()[3].woodCost;
                                        player1Resources.Resources()[2].value -= town1.EnterTown()[3].stoneCost;
                                        town1.EnterTown()[3].built = true;
                                    }
                                    return "KnightBuilding";
                                    break;
                                case "PyromancerBuilding":
                                    //Checks if you have enough resources and if it is not already build
                                    if (player1Resources.Resources()[0].value >= town1.EnterTown()[4].goldCost && player1Resources.Resources()[1].value >= town1.EnterTown()[4].woodCost &&
                                        player1Resources.Resources()[2].value >= town1.EnterTown()[4].stoneCost && town1.EnterTown()[4].built == false)
                                    {
                                        //Removes the resources and makes the building built = true
                                        player1Resources.Resources()[0].value -= town1.EnterTown()[4].goldCost;
                                        player1Resources.Resources()[1].value -= town1.EnterTown()[4].woodCost;
                                        player1Resources.Resources()[2].value -= town1.EnterTown()[4].stoneCost;
                                        town1.EnterTown()[4].built = true;
                                    }
                                    return "PyromancerBuilding";
                                    break;

                                case "Pugilist":
                                    //If you are in the city
                                    if (state == 10)
                                    {
                                        //Adds troops as long as you have money and troops to buy
                                        while (player1Resources.resources[0].value >= (town1.EnterTown()[0] as MilitaryBuilding).troopCost && (town1.EnterTown()[0] as MilitaryBuilding).AvailableTroops() > 0)
                                        {
                                            //Removes resources
                                            player1Resources.resources[0].Minus((town1.EnterTown()[0] as MilitaryBuilding).troopCost);
                                            //Adds a soldier
                                            hero.AddPugilist();
                                            //Adds a troop to the weeklyBoughtTroops
                                            (town1.EnterTown()[0] as MilitaryBuilding).weeklyBoughtTroops++;
                                        }
                                    }
                                    break;
                                case "Slinger":
                                    //If you are in the city
                                    if (state == 10)
                                    {
                                        //Adds troops as long as you have money and troops to buy
                                        while (player1Resources.resources[0].value >= (town1.EnterTown()[1] as MilitaryBuilding).troopCost && (town1.EnterTown()[1] as MilitaryBuilding).AvailableTroops() > 0)
                                        {
                                            //Removes resources
                                            player1Resources.resources[0].Minus((town1.EnterTown()[1] as MilitaryBuilding).troopCost);
                                            //Adds a soldier
                                            hero.AddSlinger();
                                            //Adds a troop to the weeklyBoughtTroops
                                            (town1.EnterTown()[1] as MilitaryBuilding).weeklyBoughtTroops++;
                                        }
                                    }
                                    break;
                                case "PoisonMaster":
                                    //If you are in the city
                                    if (state == 10)
                                    {
                                        //Adds troops as long as you have money and troops to buy
                                        while (player1Resources.resources[0].value >= (town1.EnterTown()[2] as MilitaryBuilding).troopCost && (town1.EnterTown()[2] as MilitaryBuilding).AvailableTroops() > 0)
                                        {
                                            //Removes resources
                                            player1Resources.resources[0].Minus((town1.EnterTown()[2] as MilitaryBuilding).troopCost);
                                            //Adds a soldier
                                            hero.AddPoisonMaster();
                                            //Adds a troop to the weeklyBoughtTroops
                                            (town1.EnterTown()[2] as MilitaryBuilding).weeklyBoughtTroops++;
                                        }
                                    }
                                    break;
                                case "Knight":
                                    //If you are in the city
                                    if (state == 10)
                                    {
                                        //Adds troops as long as you have money and troops to buy
                                        while (player1Resources.resources[0].value >= (town1.EnterTown()[3] as MilitaryBuilding).troopCost && (town1.EnterTown()[3] as MilitaryBuilding).AvailableTroops() > 0)
                                        {
                                            //Removes resources
                                            player1Resources.resources[0].Minus((town1.EnterTown()[3] as MilitaryBuilding).troopCost);
                                            //Adds a soldier
                                            hero.AddKnight();
                                            //Adds a troop to the weeklyBoughtTroops
                                            (town1.EnterTown()[3] as MilitaryBuilding).weeklyBoughtTroops++;
                                        }
                                    }
                                    break;
                                case "Pyromancer":
                                    //If you are in the city
                                    if (state == 10)
                                    {
                                        //Adds troops as long as you have money and troops to buy
                                        while (player1Resources.resources[0].value >= (town1.EnterTown()[4] as MilitaryBuilding).troopCost && (town1.EnterTown()[4] as MilitaryBuilding).AvailableTroops() > 0)
                                        {
                                            //Removes resources
                                            player1Resources.resources[0].Minus((town1.EnterTown()[4] as MilitaryBuilding).troopCost);
                                            //Adds a soldier
                                            hero.AddPyromancer();
                                            //Adds a troop to the weeklyBoughtTroops
                                            (town1.EnterTown()[4] as MilitaryBuilding).weeklyBoughtTroops++;
                                        }
                                    }
                                    break;
                                case "fightingGrassTexture":
                                    return t.X + "S" + t.Y;
                                    break;
                            }


                        }

                    }
                }

            }

            return "1";
        }
        bool EnemyOnGrass(List<Troops> troops, int x, int y)
        {
            for (int n = 0; n < troops.Count; n++)
            {
                if (troops[n].X == x && troops[n].Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        void Restart()
        {

            screenWidth = 1000;
            screenHeight = 700;

            //Fighting
            fightingScreen1 = new FightingScreen();
            currentEnemy = new Army(0);
            //Wanna be for-loop
            troopTurn = 0;

            //Restart screen
            gameover1 = new GameOverScreen();
            //Menu 1
            mainMenu1 = new MainMenu();
            //Menu 2
            newGameMenu1 = new NewGameMenu();
            //Cityscreen 1
            cityScreen1 = new CityScreen();

            //ESC screen
            escScreen1 = new ESCScreen();

            //Town 1
            town1 = new Town1();

            //Resources screen & Nextturn related
            player1Resources = new ResourceScreen();
            addResources = 0;
            day = 1;
            totalDays = 0;
            //Hero
            hero = new Boris(1);

            //Click related
            state = 0;
            difficulty = -1;
            map = -1;
            //Random
            r = new Random();
            //Create currentEnemy
            currentEnemy.AddPugilist();
            currentEnemy.AddSlinger();
            currentEnemy.AddPoisonMaster();
            currentEnemy.AddKnight();
            currentEnemy.AddPyromancer();
        }

        void Save()
        {
            //Save lists
            List<double> saveNumberData = new List<double>();
            List<bool> saveBoolData = new List<bool>();
            List<string> saveEnemyData = new List<string>();

            //Save info to list
            saveNumberData = hero.Save();

            for (int i = 0; i < 5; i++)
            {
                saveNumberData.Add(town1.SaveDouble()[i]);
                saveBoolData.Add(town1.SaveBool()[i]);
            }
            for (int ii = 0; ii < 3; ii++)
            {
                saveNumberData.Add(player1Resources.Save()[ii]);
            }
            saveNumberData.Add(difficulty);
            saveNumberData.Add(map);
            saveNumberData.Add(day);
            saveNumberData.Add(totalDays);

            //Save enemy info to list
            for (int iii = 0; iii < world1.armies.Count; iii++)
            {
                saveEnemyData.Add((world1.armies[iii] as Enemy).Save());
            }

            //Saves the lists to settings
            Properties.Settings1.Default.NumberList = saveNumberData;
            Properties.Settings1.Default.BoolList = saveBoolData;
            Properties.Settings1.Default.EnemyStringList = saveEnemyData;
            Properties.Settings1.Default.Save();

        }

        void Load()
        {   
            
            List<double> loadNumberData = Properties.Settings1.Default.NumberList;
            List<bool> loadBoolData = Properties.Settings1.Default.BoolList;
            List<string> loadEnemyData = Properties.Settings1.Default.EnemyStringList;

            //Load the hero
            hero.Load(loadNumberData[0], loadNumberData[1], loadNumberData[2], loadNumberData[3], loadNumberData[4], loadNumberData[5], loadNumberData[6], loadNumberData[7], loadNumberData[8], loadNumberData[9], loadNumberData[10], loadNumberData[11]);

            //Load weekly bought troops
            town1.LoadDouble(Convert.ToInt32(loadNumberData[12]), Convert.ToInt32(loadNumberData[13]), Convert.ToInt32(loadNumberData[14]), Convert.ToInt32(loadNumberData[15]), Convert.ToInt32(loadNumberData[16]));

            //Load resources
            player1Resources.resources[0].value = Convert.ToInt32(loadNumberData[17]);
            player1Resources.resources[1].value = Convert.ToInt32(loadNumberData[18]);
            player1Resources.resources[2].value = Convert.ToInt32(loadNumberData[19]);

            //Load general stuff
            difficulty = Convert.ToInt32(loadNumberData[20]);
            map = Convert.ToInt32(loadNumberData[21]);
            day = Convert.ToInt32(loadNumberData[22]);
            totalDays = Convert.ToInt32(loadNumberData[23]);

            //Load built buildings
            town1.LoadBool(loadBoolData[0], loadBoolData[1], loadBoolData[2], loadBoolData[3], loadBoolData[4]);

            if (map == 0)
            {
                //Set starting point for map
                world1 = new World(60, 80, hero.xPos, hero.yPos);
                world1.armies.Clear();
            }
            if (map == 1)
            {
                //Set starting point for map
                world1 = new World(660, 460, hero.xPos, hero.yPos);
                world1.armies.Clear();
            }
            //Load enemies
            for (int i = 0; i < loadEnemyData.Count; i++)
            {
                try
                {
                    
                    //Split the data into usable parts
                    string[] dataDifference = loadEnemyData[i].Split('S');
                    //Start loading the data
                    int x = int.Parse(dataDifference[0]);
                    int y = int.Parse(dataDifference[1]);

                    world1.armies.Add(new Enemy(0, true, x, y));

                    for (int ii = 2; i < 7; i++)
                    {
                        world1.armies[i].troops[ii - 2].troopSize = int.Parse(dataDifference[ii]);
                    }

                    if (dataDifference[7] == "1")
                    {
                        (world1.armies[i] as Enemy).alive = true;
                    }
                    else
                    {
                        (world1.armies[i] as Enemy).alive = false;
                    }
                }
                catch
                {
                }
                
            }
        }

    }

    
}
