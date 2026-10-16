using Uno_Game;
using Uno_Game.Models;
using Uno_Game.Models.Cards.Uno;
using Uno_Game.Models.Cards.Uno.Expansion_Pack.Reverse_pack;
using Uno_Game.Models.Cards.Uno.Expansion_Pack.Stack_pack;
using GameMode = Uno_Game.Models.EnumCollections.gameModes;
using GamePacks = Uno_Game.Models.EnumCollections.gamePacks;
using GameStates = Uno_Game.Models.EnumCollections.gameStates;

static void startGame(int gameMode, GamePacks gamePacks)
{
    /////////VARIABLES
    Random rnd = new Random();
    int gameState = (int) GameStates.INGAME;
    int turn = 0, opcCard = 0;
    int gameFlow = 1; //1: Clockwise  -1: Anticlockwise
    bool isActionAlreadyDone = false;
    Card? actualCard = null;

    //Create the deck
    List<Card> deck = GameActions.makeDeck(gamePacks);

    //Create the list of players
    List<Player> playersList = GameActions.makePlayersList(4, deck, gamePacks);

    //Reverse Pack Variables
    bool hasWildNoUCard = false;
    int nextPlayerPunish = 0;

    ///////////END VARIABLES


    //Grab first card
    actualCard = GameActions.getFirstCard(deck);

    do
    {
        //Players select a card
        opcCard = GameActions.selectCard(turn, gameFlow, playersList, ref actualCard, deck, ref isActionAlreadyDone, gamePacks);

        //UNO! message
        if (playersList[turn].hand.Count == 1)
        {
            Console.WriteLine("Player " + (turn + 1) + " says: UNO!");
        }

        //Win condition
        if (playersList[turn].hand.Count == 0)
        {
            gameState = (int) GameStates.GAMEOVER;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPlayer " + (turn + 1) + " wins!");
            break;
        }


        //To prevent the same action card do its action more than once
        if (!isActionAlreadyDone)
        {
            isActionAlreadyDone = true;

            //ADITIONAL ACTIONS
            Console.ForegroundColor = ConsoleColor.Cyan;

            //Only for WildNoUCard purposes
            if (gamePacks.HasFlag(GamePacks.REVERSE))
            {
                hasWildNoUCard = GameActions.playerHasWildNoUCard(turn, gameFlow, actualCard, 
                    playersList, ref nextPlayerPunish);
            }

            //Wild card
            if (actualCard.color == (int)EnumCollections.cardColors.WILDCARD)
            {
                GameActions.wildCardAction(actualCard, turn, playersList[turn]);

                //Wild no u card
                if (actualCard is WildNoUCard)
                    GameActions.wildNoUCardAction(ref gameFlow);
            }

            //Draw cards
            if (actualCard is DrawCard drawCard)
            {
                //Has a Wild No U Card?
                if (hasWildNoUCard)
                {    
                    int nextPlayer = GameActions.whoIsNextPlayer(turn, gameFlow, playersList.Count);
                    GameActions.displayPunishment(ref turn, ref gameFlow, nextPlayer, string.Concat("Player ",
                        (nextPlayer + 1), " draws ", drawCard.cardCant, " card(s)"), ref hasWildNoUCard);
                }
                
                GameActions.drawCardsAction(turn, gameFlow, playersList, drawCard.cardCant, 
                        deck, gamePacks);
            }

            //Reverse card
            if (actualCard is ReverseCard reverseCard)
            {
                if (playersList.Count != 2)
                    GameActions.reverseCardAction(ref gameFlow);
                else
                    GameActions.skipCardAction(1, ref turn, gameFlow);
            }

            //Skip card
            if (actualCard is SkipCard skipCard)
            {
                //Has a Wild No U Card?
                if (hasWildNoUCard)
                {
                    int nextPlayer = GameActions.whoIsNextPlayer(turn, gameFlow, playersList.Count);
                    GameActions.displayPunishment(ref turn, ref gameFlow, nextPlayer, string.Concat("Skipped ", skipCard.turnsSkipped, " turn(s)"), ref hasWildNoUCard);
                }

                GameActions.skipCardAction(skipCard.turnsSkipped, ref turn, gameFlow);
            }


            //REVERSE PACK ACTIONS
            if (gamePacks.HasFlag(GamePacks.REVERSE))
            {
                //Reverse skip card
                if (actualCard is ReverseSkipCard reverseSkipCard)
                {
                    GameActions.reverseSkipCardAction(reverseSkipCard.skipCard.turnsSkipped, ref turn, 
                        ref gameFlow, playersList.Count, hasWildNoUCard);
                }

                //Reverse draw 2 card
                if (actualCard is ReverseDrawCard reverseDrawCard)
                {
                    GameActions.reverseDrawCardAction(ref turn, ref gameFlow, playersList,
                            reverseDrawCard.drawCard, deck, gamePacks, hasWildNoUCard);
                }

                //Wild power reverse card
                if (actualCard is WildPowerReverseCard wildPowerReverseCard)
                    GameActions.wildPowerReverseCardAction(ref turn, ref gameFlow);
            }


            //STACK PACK ACTIONS
            if (gamePacks.HasFlag(GamePacks.STACK))
            {
                //Stack Card
                if(actualCard is StackCard stackCard)
                {
                    StackCard.stack += stackCard.cardCant;
                    actualCard = GameActions.stackCardAction(StackCard.stack, ref turn, gameFlow, 
                        playersList, deck, gamePacks, actualCard);
                    StackCard.stack = 0;
                }
            }
        }

        Thread.Sleep(2000);
            
        //Next player turn
        turn += gameFlow;
        Console.WriteLine();
        if (turn >= playersList.Count)
        {
            turn -= playersList.Count;
        }   
        else if (turn < 0)
        {
            turn += playersList.Count;
        }
            

        Console.ResetColor();

    }while (gameState == (int) GameStates.INGAME);
}

/*int gameMode = GameActions.selectOption("Select a game mode\n1) Normal mode", 2, 0);*/
GamePacks gamepacks = GameActions.selectPack();

//GamePacks gamePacks = new GamePacks();
Console.Clear();
startGame((int)GameMode.NORMAL, gamepacks);