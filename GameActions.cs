using Uno_Game.Models;
using Uno_Game.Models.Cards.Uno;
using Uno_Game.Models.Cards.Uno.Expansion_Pack.Reverse_pack;
using Uno_Game.Models.Cards.Uno.Expansion_Pack.Stack_pack;
using GamePacks = Uno_Game.Models.EnumCollections.gamePacks;
using CardColor = Uno_Game.Models.EnumCollections.cardColors;
using static Uno_Game.Models.EnumCollections;

//This class is only for game actions like make decks, draw cards, display cards, etc.

namespace Uno_Game
{
    public class GameActions
    {
        static Random rnd = new Random();

        public static int selectOption(string message, int upperLimit, int lowerLimit)
        {
            int res = 0;
            Console.WriteLine(message);
            do
            {
                try
                {
                    //Choose an option
                    res = int.Parse(Console.ReadLine()!);
                    if (res < upperLimit && res > lowerLimit)
                        break;
                    else
                        throw new Exception();
                }
                catch (Exception e)
                {
                    displayWarningMessage();
                }
            } while (true);

            return res;
        }

        public static GamePacks selectPack()
        {
            GamePacks gamePacks = new GamePacks();
            int opc = 0;
            do
            {
                opc = selectOption("Select an expansion pack\n1) Reverse pack\n" +
    "2) Stack pack \n10) Start Game", 11, 0);

                switch (opc)
                {
                    case 1:
                        gamePacks ^= GamePacks.REVERSE;
                        break;
                    case 2:
                        gamePacks ^= GamePacks.STACK;
                        break;
                    case 3:
                        gamePacks ^= GamePacks.SWAP;
                        break;
                    case 4:
                        gamePacks ^= GamePacks.SPEED;
                        break;
                    case 5:
                        gamePacks ^= GamePacks.FRIENEMIES;
                        break;
                    case 6:
                        gamePacks ^= GamePacks.DEFENSE;
                        break;
                    case 7:
                        gamePacks ^= GamePacks.COMEBACK;
                        break;
                    case 8:
                        gamePacks ^= GamePacks.BILLIE;
                        break;
                    case 9:
                        gamePacks ^= GamePacks.NO_MERCY;
                        break;
                }
            } while (opc != 10);
            
            return gamePacks;
        }

        public static Card getFirstCard(List<Card> deck)
        {
            Card actualCard;
            do
            {
                //Grab the first card
                int n = rnd.Next(0, deck.Count - 1);
                if (deck[n] is NumberCard)
                {
                    actualCard = deck[n];
                    deck.RemoveAt(n);
                    break;
                }
            } while (true);
            return actualCard;
        }

        public static void displayWarningMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSelect a valid option");
            Console.ResetColor();
        }

        public static List<Card> makeDeck(GamePacks gamePacks)
        {
            List<Card> deck = new List<Card>();

            //NUMBER CARDS
            //Colors
            for (int i = 0; i < 4; i++)
            {
                //Number
                for (int j = 0; j < 10; j++)
                {
                    //Cant of cards
                    for (int k = 0; k < 2; k++)
                    {
                        deck.Add(new NumberCard(i, j, j));
                        //Only one 0 card
                        if (j == 0)
                            break;
                    }
                }
            }

            //SKIP CARDS
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    deck.Add(new SkipCard(i));
                }
            }

            //REVERSE CARDS
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    deck.Add(new ReverseCard(i));
                }
            }

            //DRAW 2 CARDS
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    deck.Add(new DrawCard(i));
                }
            }

            //WILDCARD
            for (int i = 0; i < 4; i++)
            {
                deck.Add(new WildCard());
            }

            //DRAW 4 WILDCARD
            for (int i = 0; i < 4; i++)
            {
                deck.Add(new DrawWildCard(1));
            }


            //REVERSE PACK CARDS
            if (gamePacks.HasFlag(GamePacks.REVERSE))
            {
                //REVERSE SKIP CARD
                for (int i = 0; i < 4; i++)
                {
                    deck.Add(new ReverseSkipCard(i));
                }

                //REVERSE DRAW 2 CARDS
                for (int i = 0; i < 4; i++)
                {
                    deck.Add(new ReverseDrawCard(i));
                }

                //WILD POWER REVERSE CARD
                for (int i = 0; i < 4; i++)
                {
                    deck.Add(new WildPowerReverseCard());
                }

                //WILD NO U CARD
                for (int i = 0; i < 4; i++)
                {
                    deck.Add(new WildNoUCard());
                }
            }


            //STACK PACK CARDS
            if (gamePacks.HasFlag(GamePacks.STACK))
            {
                for (int i = 0; i < 4; i++)
                {
                    //STACK 1 CARD
                    deck.Add(new StackCard(i, cardCant: 1));

                    //STACK 2 CARD
                    deck.Add(new StackCard(i, cardCant: 2));

                    //WILD STACK 3 CARD
                    //deck.Add(new WildStackCard());
                }
            }

            return deck;
        }

        public static List<Card> makehand(List<Card> deck, GamePacks gamePacks)
        {
            Random rnd = new Random();
            List<Card> hand = new List<Card>();
            int n = 0;

            for (int i = 0; i < 7; i++)
            {
                //If there's no cards left
                if (deck.Count == 0)
                    deck = makeDeck(gamePacks);

                n = rnd.Next(0, deck.Count - 1);
                hand.Add(deck[n]);
                deck.RemoveAt(n);
            }

            return hand;
        }

        public static List<Player> makePlayersList(int cantPlayers, List<Card> deck, GamePacks gamePacks)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < cantPlayers; i++)
            {
                Player tmpPlayer = new Player(i, makehand(deck, gamePacks));
                players.Add(tmpPlayer);
            }

            return players;
        }

        public static bool isValidCard(Card playerCard, Card actualCard)
        {
            bool isValid = true;
            //Are the same color? Is a wild card?
            if (playerCard.color != actualCard!.color && 
                actualCard.color != (int) EnumCollections.cardColors.WILDCARD)
            {
                //Is a wild card?
                if (playerCard.color == (int)EnumCollections.cardColors.WILDCARD)
                {
                    //nop
                }
                //Are the same kind of card?
                else if (playerCard.GetType() != actualCard.GetType())
                {
                    isValid = false;
                }
                //Are number cards?
                else if (playerCard is NumberCard numberCard && actualCard is NumberCard tmpCard)
                {
                    //They have the same number?
                    if (numberCard.number != tmpCard.number)
                        isValid = false;
                }
            }
            return isValid;
        }

        public static bool isValidHand(List<Card> playerHand, Card actualCard)
        {
            bool isValidHand = false;

            //Check every card
            for (int i = 0; i < playerHand.Count; i++)
            {
                if (isValidCard(playerHand[i], actualCard))
                {
                    isValidHand = true;
                    break;
                }
            }
            return isValidHand;
        }

        public static void displayPlayerHand(List<Card> hand, Card actualCard)
        {
            Console.ResetColor();
            Console.WriteLine("Actual card: " + actualCard.ToString() + "\n");
            Console.WriteLine("Your hand:");
            int n = 1;

            foreach (var card in hand)
            {
                if (isValidCard(card, actualCard))
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine((n++) + ") " + card.ToString());
            }
            Console.ResetColor();
        }

        public static int selectCard(int turn, int gameFlow, List<Player> playersList, ref Card actualCard, List<Card> deck, ref bool isActionAlreadyDone, GamePacks gamePacks)
        {
            int opcCard = 0;
            //Player's turn
            if (turn == 0)
            {
                //Display player hand if it has a card to put on the deck
                if (isValidHand(playersList[turn].hand, actualCard))
                {
                    displayPlayerHand(playersList[turn].hand, actualCard);

                    do
                    {
                        try
                        {
                            //Choose a card
                            opcCard = selectOption("", playersList[turn].hand.Count + 1, 0) - 1;

                            //If the card is valid replace the top card on the pile
                            if (!isValidCard(playersList[turn].hand[opcCard], actualCard))
                                throw new Exception();

                            break;
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Select a valid card");
                            Console.ResetColor();
                        }
                    } while (true);

                    isActionAlreadyDone = false;

                    //The new actual card is the player's card
                    actualCard = playersList[turn].hand[opcCard];
                    playersList[turn].hand.RemoveAt(opcCard);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No cards available for player 1, draw 1 card");
                    drawCards(playersList[turn].hand, deck, 1, gamePacks);
                }
            }
            //NPC turn
            else
            {
                //If there is playable cards
                if (isValidHand(playersList[turn].hand, actualCard))
                {
                    int value = 1000, pos = 0;

                    //Check the valid card with the lowest value in order to use it
                    for (int i = 0; i < playersList[turn].hand.Count; i++)
                    {
                        Card tmpCard = playersList[turn].hand[i];

                        if (isValidCard(tmpCard, actualCard))
                        {
                            int nextPlayer = whoIsNextPlayer(turn, gameFlow, playersList.Count);

                            //Revenge mode: Prevents next player from winning easily
                            if (playersList[nextPlayer].hand.Count == 1)
                            {
                                if (tmpCard is (DrawCard or SkipCard))
                                {
                                    pos = i;
                                    break;
                                }
                            }

                            //The new lowest value card
                            if (value > tmpCard.points)
                            {
                                value = tmpCard.points;
                                pos = i;
                            }
                        }
                    }
                    isActionAlreadyDone = false;

                    //The new actual card is the npc's card
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Player " + (turn + 1) + " use the card: " + playersList[turn].hand[pos].ToString());
                    actualCard = playersList[turn].hand[pos];
                    playersList[turn].hand.RemoveAt(pos);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No cards available for player " + (turn + 1) + ", draw 1 card");
                    drawCards(playersList[turn].hand, deck, 1, gamePacks);
                }
            }
            return opcCard;
        }

        public static void drawCards(List<Card> playerHand, List<Card> deck, int cant, GamePacks gamePacks)
        {
            Random rnd = new Random();
            int n = 0;

            //In case the deck doesn't have enough cards
            if (deck.Count < cant)
                deck = makeDeck(gamePacks);

            //The player draw n cards
            for (int i = 0; i < cant; i++)
            {
                n = rnd.Next(0, deck.Count - 1);
                playerHand.Add(deck[n]);
                deck.RemoveAt(n);
            }
        }

        public static int whoIsNextPlayer(int turn, int gameFlow, int playersListCount)
        {
            int nextPlayer = turn + gameFlow;

            //Checks the next player
            if (nextPlayer >= playersListCount)
                nextPlayer = 0;
            else if (nextPlayer < 0)
                nextPlayer = playersListCount - 1;

            return nextPlayer;
        }


        //REGULAR CARDS ACTIONS
        public static void wildCardAction(Card actualCard, int turn, Player player)
        {
            //Player's turn
            if (turn == 0)
            {
                int color = selectOption("Select a color:\n1) Red\n2) Yellow\n3) Blue\n4) Green", 5, 0);
                actualCard.color = color - 1;
            }
            //NPC turn
            else
            {
                //Select the color basing on its cards
                for(int i = 0; i < player.hand.Count; i++)
                {
                    if (player.hand.Count > 1)
                    {
                        if (player.hand[i].color != (int) CardColor.WILDCARD)
                        {
                            //50% color of its hand
                            if(rnd.Next(0,2) < 1)
                                actualCard.color = player.hand[i].color;
                            //50% random color
                            else
                            {
                                actualCard.color = rnd.Next(0, 4);
                                break;
                            }
                        } 
                    }
                    else
                    {
                        actualCard.color = rnd.Next(0, 4);
                        break;
                    }
                }

                //In case the NPC doesn't have any color in its hand
                if (actualCard.color == (int)EnumCollections.cardColors.WILDCARD)
                    actualCard.color = (int)EnumCollections.cardColors.RED;

                string color;

                if (actualCard.color == 0)
                    color = "red";
                else if (actualCard.color == 1)
                    color = "yellow";
                else if (actualCard.color == 2)
                    color = "blue";
                else
                    color = "green";
                Console.WriteLine("Player " + (turn + 1) + " selected color " + color);
            }
        }

        public static void drawCardsAction(int turn, int gameFlow, List<Player> playersList, int cardCant, List<Card> deck, GamePacks gamePacks)
        {
            //Draw cards under the limits
            int nextPlayer = whoIsNextPlayer(turn, gameFlow, playersList.Count);
            Console.WriteLine("Player " + (nextPlayer + 1) + " draws " + cardCant + " card(s)");
    
            //The next player draws n cards
            drawCards(playersList[nextPlayer].hand, deck, cardCant, gamePacks);
        }

        public static void reverseCardAction(ref int gameFlow)
        {
            gameFlow *= -1;
            string flow = (gameFlow == 1) ? "clockwise" : "anticlockwise";
            Console.WriteLine("Game flow is now " + flow);
        }


        public static void skipCardAction(int turnsSkipped, ref int turn, int gameFlow)
        {
            Console.WriteLine("Skipped " + turnsSkipped + " turn(s)");
            turn = turn + (turnsSkipped * gameFlow);
        }


        //REVERSE PACK ACTIONS
        public static void reverseSkipCardAction(int turnsSkipped, ref int turn, ref int gameFlow, 
            int playersListCount, bool hasWildNoUCard)
        {
            reverseCardAction(ref gameFlow);

            if (hasWildNoUCard)
            {
                int nextPlater = whoIsNextPlayer(turn, gameFlow, playersListCount);
                GameActions.displayPunishment(ref turn, ref gameFlow, nextPlater,
                            string.Concat("Skipped ", turnsSkipped, " turn(s)"), ref hasWildNoUCard);
            }

            skipCardAction(turnsSkipped, ref turn, gameFlow);
        }

        public static void reverseDrawCardAction(ref int turn, ref int gameFlow, List<Player> playersList, DrawCard drawCard, List<Card> deck, GamePacks gamePacks, bool hasWildNoUCard)
        {
            reverseCardAction(ref gameFlow);

            if (hasWildNoUCard)
            {
                int nextPlayer = whoIsNextPlayer(turn, gameFlow, playersList.Count);
                displayPunishment(ref turn, ref gameFlow, nextPlayer, string.Concat("Player ",
                            (nextPlayer + 1), " draws ", drawCard.cardCant, " card(s)"), ref hasWildNoUCard);
            }
            
            drawCardsAction(turn, gameFlow, playersList, drawCard.cardCant, deck, gamePacks);
            skipCardAction(drawCard.turnsSkipped, ref turn, gameFlow);
        }

        public static void wildPowerReverseCardAction(ref int turn, ref int gameFlow)
        {
            reverseCardAction(ref gameFlow);
            turn -= gameFlow;
        }

        public static bool playerHasWildNoUCard(int turn, int gameFlow, Card actualCard, List<Player> playersList, ref int nextPlayerPunish)
        {
            bool hasWildNoUCard = false;

            if (actualCard is ReverseSkipCard or ReverseDrawCard)
                nextPlayerPunish = whoIsNextPlayer(turn, (gameFlow * -1), playersList.Count);
            else
                nextPlayerPunish = whoIsNextPlayer(turn, gameFlow, playersList.Count);

            //Checking if the player has a WildNoUCard
            for (int i = 0; i < playersList[nextPlayerPunish].hand.Count; i++)
            {
                if (playersList[nextPlayerPunish].hand[i] is WildNoUCard)
                {
                    hasWildNoUCard = true;
                    playersList[nextPlayerPunish].hand.RemoveAt(i);
                }
            }

            return hasWildNoUCard;
        }

        public static void displayPunishment(ref int  turn, ref int gameFlow, int nextPlayer, string message, ref bool hasWildNoUCard)
        {
            Console.WriteLine(message);
            Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nBut wait...\n");
            Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Player " + (nextPlayer + 1) + " has a Wild No U Card and reverse the punishment\n");
            Thread.Sleep(2000);
            reverseCardAction(ref gameFlow);
            turn -= gameFlow;
            hasWildNoUCard = false;
        }

        public static void wildNoUCardAction(ref int gameFlow)
        {
            reverseCardAction(ref gameFlow);
        }

        public static bool nextPlayerHasStackCard(Player nextPlayer)
        {
            bool hasStackCard = false;

            for(int i = 0; i < nextPlayer.hand.Count; i++)
            {
                if (nextPlayer.hand[i] is StackCard) 
                {
                    hasStackCard = true;
                    break;
                }
            }

            return hasStackCard;
        }

        public static Card stackCardAction(int stack, ref int turn, int gameFlow, 
            List<Player> playersList, List<Card> deck, GamePacks gamePacks, Card actualCard)
        {
            bool hasStackCard = false;
            Console.WriteLine("Player " + (turn + 1) + " add " + stack + " card(s) to the stack");
            Thread.Sleep(1500);

            do
            {
                int nextPlayer = whoIsNextPlayer(turn, gameFlow, playersList.Count);
                hasStackCard = nextPlayerHasStackCard(playersList[nextPlayer]);

                if (hasStackCard)
                {
                    turn += gameFlow;

                    if (turn >= playersList.Count)
                    {
                        turn -= playersList.Count;
                    }
                    else if (turn < 0)
                    {
                        turn += playersList.Count;
                    }

                    for (int i = 0; i < playersList[turn].hand.Count; i++)
                    {
                        if (playersList[turn].hand[i] is StackCard stackCard)
                        {
                            Console.WriteLine("Player " + (turn + 1) + " add " + stackCard.cardCant + 
                                " card(s) to the stack");
                            actualCard = playersList[turn].hand[i];
                            playersList[turn].hand.RemoveAt(i);
                            Thread.Sleep(1500);

                            stack += stackCard.cardCant;
                            break;
                        }
                    }
                }
                else
                {
                    drawCardsAction(turn, gameFlow, playersList, stack, deck, gamePacks);
                    skipCardAction(1, ref turn, gameFlow);
                }
            } while (hasStackCard);
            return actualCard;
        }
    }
}
