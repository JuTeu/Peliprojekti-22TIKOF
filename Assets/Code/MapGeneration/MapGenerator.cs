using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public static int[,] GenerateMap(int mapWidth, int mapHeight, int roomCount, int minBottomWidth, int maxBottomWidth)
    {

        bool[,] map = generateMapShape(mapWidth, mapHeight, roomCount,
                                        minBottomWidth, maxBottomWidth);
        while (getDeadEndCount(map) < 4) {
            map = generateMapShape(mapWidth, mapHeight, roomCount,
                                   minBottomWidth, maxBottomWidth);
        }
        int[,] fancyMap = generateMapFromShape(map);
        drawMap(fancyMap);
        return fancyMap;
    }

    // Update is called once per frame
    void Update()
    {

    }
    static bool[,] generateMapShape(int mapWidth, int mapHeight,
                     int roomCount, int minBottomWidth, int maxBottomWidth)
    {
        bool[,] map = new bool[mapHeight, mapWidth];
        bool validBottom = roomCount >= mapHeight
                              && minBottomWidth <= maxBottomWidth 
                              && maxBottomWidth <= mapWidth 
                              && mapHeight + minBottomWidth - 1 <= roomCount 
                              && roomCount < mapWidth * mapHeight -
                              (mapWidth - maxBottomWidth) + 1 ? false : true;
        do {
            int[] carverPos = {0, mapWidth / 2};
            int currentRoomCount = 0;
            int direction = 0;
            int bottomRoomCount = 0;
            for (int i = 0; i < mapHeight; i++) {
                for (int j = 0; j < mapWidth; j++) {
                    map[i, j] = false;
                }
            }
            while (currentRoomCount < roomCount) {
                if (map[carverPos[0], carverPos[1]] == false) {
                    currentRoomCount++;
                    map[carverPos[0], carverPos[1]] = true;
                }
                direction = (int) (Random.Range(0, 2));
                int mapSize = direction == 1 ? mapWidth : mapHeight;
                carverPos[direction] = (Random.Range(0, 2)) < 1
                                       ? carverPos[direction] - 1
                                       : carverPos[direction] + 1;
                
                if (carverPos[direction] < 0 ||
                    carverPos[direction] >= mapSize) {
                    carverPos[direction] = carverPos[direction] < 0
                                         ? 0 : mapSize - 1;
                }
            }
            for (int i = 0; i < mapWidth; i++) {
                if (map[mapHeight - 1, i] == true) {
                    bottomRoomCount++;
                }
            }
            if (bottomRoomCount <= maxBottomWidth 
                && bottomRoomCount >= minBottomWidth) {
                validBottom = true;
            }

        } while (!validBottom);
        
        return map;
    }
    static int getDeadEndCount(bool[,] map)
    {
        int deadEndCount = 0;
        int mapWidth = map.GetLength(1);
        int mapHeight = map.GetLength(0);

        for (int i = 0; i < mapHeight - 1; i++) {
            for (int j = 0; j < mapWidth; j++) {
                int openSide = 0;
                if (map[i, j] == true) {
                    bool left = j - 1 < 0 ? true : false;
                    bool right = j + 1 >= mapWidth ? true : false;
                    bool top = i - 1 < 0 ? true : false;
                    bool bottom = i + 1 >= mapHeight ? true : false;
                    if (!left) {
                        openSide = map[i, j - 1] ? openSide + 1 : openSide;
                    }
                    if (!top) {
                        openSide = map[i - 1, j] ? openSide + 1 : openSide;
                    }
                    if (!right) {
                        openSide = map[i, j + 1] ? openSide + 1 : openSide;
                    }
                    if (!bottom) {
                        openSide = map[i + 1, j] ? openSide + 1 : openSide;
                    }
                    if (openSide <= 1) {
                        deadEndCount++;
                    }
                }
            }
        }
        return deadEndCount;
    }
    static int[,] generateMapFromShape(bool[,] map)
    {
        //0 = SEIN??

        //1 = ALAS AUKI
        //2 = YL??S AUKI
        //3 = VASEMMALLE AUKI
        //4 = OIKEALLE AUKI

        //5 = ALAS JA YL??S AUKI
        //6 = ALAS JA VASEMMALLE AUKI
        //7 = ALAS JA OIKEALLE AUKI
        //8 = YL??S JA VASEMMALLE AUKI
        //9 = YL??S JA OIKEALLE AUKI
        //10 = VASEMMALLE JA OIKEALLE AUKI

        //11 = ALAS, YL??S JA VASEMMALLE AUKI
        //12 = ALAS, YL??S JA OIKEALLE AUKI
        //13 = ALAS, OIKEALLE JA VASEMMALLE AUKI
        //14 = YL??S, OIKEALLE JA VASEMMALLE AUKI

        //15 = ALAS, YL??S, OIKEALLE JA VASEMMALLE AUKI
        //16 = KAIKKIIN SUUNTIIN TYHJ??

        //17 = ALAS, VASEMMALLE JA ALAVASEMMALLE AUKI
        //18 = ALAS, OIKEALLE JA ALAOIKEALLE AUKI
        //19 = YL??S, VASEMMALLE JA YL??VASEMMALLE AUKI
        //20 = YL??S, OIKEALLE JA YL??OIKEALLE AUKI

        //21 = YL??S, VASEMMALLE, OIKEALLE, YL??VASEMMALLE JA YL??OIKEALLE AUKI
        //22 = YL??S, VASEMMALLE, OIKEALLE JA YL??VASEMMALLE AUKI
        //23 = YL??S, VASEMMALLE, OIKEALLE JA YL??OIKEALLE AUKI

        //24 = ALAS, VASEMMALLE, OIKEALLE, ALAVASEMMALLE JA ALAOIKEALLE AUKI
        //25 = ALAS, VASEMMALLE, OIKEALLE JA ALAVASEMMALLE AUKI
        //26 = ALAS, VASEMMALLE, OIKEALLE JA ALAOIKEALLE AUKI

        //27 = ALAS, YL??S, VASEMMALLE, ALAVASEMMALLE JA YL??VASEMMALLE AUKI
        //28 = ALAS, YL??S, VASEMMALLE JA ALAVASEMMALLE AUKI
        //29 = ALAS, YL??S, VASEMMALLE JA YL??VASEMMALLE AUKI

        //30 = ALAS, YL??S, OIKEALLE, ALAVOIKEALLE JA YL??OIKEALLE AUKI
        //31 = ALAS, YL??S, OIKEALLE JA ALAOIKEALLE AUKI
        //32 = ALAS, YL??S, OIKEALLE JA YL??OIKEALLE AUKI


        //33 = ALAS, YL??S, OIKEALLE, VASEMMALLE, ALAVASEMMALLE, ALAOIKEALLE JA YL??VASEMMALLE AUKI
        //34 = ALAS, YL??S, OIKEALLE, VASEMMALLE, ALAVASEMMALLE, ALAOIKEALLE JA YL??OIKEALLE AUKI
        //35 = ALAS, YL??S, OIKEALLE, VASEMMALLE, YL??VASEMMALLE, YL??OIKEALLE JA ALAOIKEALLE AUKI 
        //36 = ALAS, YL??S, OIKEALLE, VASEMMALLE, ALAVASEMMALLE, YL??VASEMMALLE JA YL??OIKEALLE AUKI

        //37 = ALAS, YL??S, OIKEALLE, VASEMMALLE, ALAVASEMMALLE JA ALAOIKEALLE AUKI
        //38 = ALAS, YL??S, OIKEALLE, VASEMMALLE, ALAVASEMMALLE JA YL??VASEMMALLE AUKI
        //39 = ALAS, YL??S, OIKEALLE, VASEMMALLE, ALAVASEMMALLE JA YL??OIKEALLE AUKI
        //40 = ALAS, YL??S, OIKEALLE, VASEMMALLE, YL??VASEMMALLE JA ALAOIKEALLE AUKI
        //41 = ALAS, YL??S, OIKEALLE, VASEMMALLE, YL??OIKEALLE JA ALAOIKEALLE AUKI
        //42 = ALAS, YL??S, OIKEALLE, VASEMMALLE, YL??VASEMMALLE JA YL??OIKEALLE AUKI

        //43 = ALAS, YL??S, OIKEALLE, VASEMMALLE JA ALAVASEMMALLE AUKI
        //44 = ALAS, YL??S, OIKEALLE, VASEMMALLE JA ALAOIKEALLE AUKI
        //45 = ALAS, YL??S, OIKEALLE, VASEMMALLE JA YL??VASEMMALLE AUKI
        //46 = ALAS, YL??S, OIKEALLE, VASEMMALLE JA YL??OIKEALLE AUKI


        int mapWidth = map.GetLength(1);
        int mapHeight = map.GetLength(0);
        int[, ] newMap = new int[mapHeight, mapWidth];

        for (int i = 0; i < mapHeight; i++) {
            for (int j = 0; j < mapWidth; j++) {
                if (map[i, j] == true) {
                    bool left = j - 1 < 0 ? true : false;
                    bool right = j + 1 >= mapWidth ? true : false;
                    bool top = i - 1 < 0 ? true : false;
                    bool bottom = i + 1 >= mapHeight ? true : false;

                    bool leftOpen = false;
                    bool rightOpen = false;
                    bool topOpen = false;
                    bool bottomOpen = false;

                    bool topLeftOpen = false;
                    bool topRightOpen = false;
                    bool bottomLeftOpen = false;
                    bool bottomRightOpen = false;

                    if (!left) {
                        leftOpen = map[i, j - 1] ? true : false;
                        if (!top) {
                            topLeftOpen = map[i - 1, j - 1] ? true : false;
                        }
                        if (!bottom) {
                            bottomLeftOpen = map[i + 1, j - 1] ? true : false;
                        }
                    }
                    if (!top) {
                        topOpen = map[i - 1, j] ? true : false;
                    }
                    if (!right) {
                        rightOpen = map[i, j + 1] ? true : false;
                        if (!top) {
                            topRightOpen = map[i - 1, j + 1] ? true : false;
                        }
                        if (!bottom) {
                            bottomRightOpen = map[i + 1, j + 1] ? true : false;
                        }
                    }
                    if (!bottom) {
                        bottomOpen = map[i + 1, j] ? true : false;
                    }
                    if (bottomOpen && topOpen && leftOpen && rightOpen) {
                        if (topLeftOpen && topRightOpen && bottomLeftOpen && bottomRightOpen) {
                            newMap[i, j] = 16;
                        } else if (bottomLeftOpen && bottomRightOpen && topLeftOpen) {
                            newMap[i, j] = 33;
                        } else if (bottomLeftOpen && bottomRightOpen && topRightOpen) {
                            newMap[i, j] = 34;
                        } else if (topLeftOpen && topRightOpen && bottomRightOpen) {
                            newMap[i, j] = 35;
                        } else if (bottomLeftOpen && topLeftOpen && topRightOpen) {
                            newMap[i, j] = 36;
                        } else if (bottomLeftOpen && bottomRightOpen) {
                            newMap[i, j] = 37;
                        } else if (bottomLeftOpen && topLeftOpen) {
                            newMap[i, j] = 38;
                        } else if (bottomLeftOpen && topRightOpen) {
                            newMap[i, j] = 39;
                        } else if (topLeftOpen && bottomRightOpen) {
                            newMap[i, j] = 40;
                        } else if (topRightOpen && bottomRightOpen) {
                            newMap[i, j] = 41;
                        } else if (topLeftOpen && topRightOpen) {
                            newMap[i, j] = 42;
                        } else if (bottomLeftOpen) {
                            newMap[i, j] = 43;
                        } else if (bottomRightOpen) {
                            newMap[i, j] = 44;
                        } else if (topLeftOpen) {
                            newMap[i, j] = 45;
                        } else if (topRightOpen) {
                            newMap[i, j] = 46;
                        } else {
                            newMap[i, j] = 15;
                        }
                    } else if (topOpen && rightOpen && leftOpen) {
                        if (topLeftOpen && topRightOpen) {
                            newMap[i, j] = 21;
                        } else if (topLeftOpen) {
                            newMap[i, j] = 22;
                        } else if (topRightOpen) {
                            newMap[i, j] = 23;
                        } else {
                            newMap[i, j] = 14;
                        }
                    } else if (bottomOpen && rightOpen && leftOpen) {
                        if (bottomLeftOpen && bottomRightOpen) {
                            newMap[i, j] = 24;
                        } else if (bottomLeftOpen) {
                            newMap[i, j] = 25;
                        } else if (bottomRightOpen) {
                            newMap[i, j] = 26;
                        } else {
                            newMap[i, j] = 13;
                        }
                    } else if (bottomOpen && topOpen && rightOpen) {
                        if (bottomRightOpen && topRightOpen) {
                            newMap[i, j] = 30;
                        } else if (bottomRightOpen) {
                            newMap[i, j] = 31;
                        } else if (topRightOpen) {
                            newMap[i, j] = 32;
                        } else {
                            newMap[i, j] = 12;
                        }
                    } else if (bottomOpen && topOpen && leftOpen) {
                        if (bottomLeftOpen && topLeftOpen) {
                            newMap[i, j] = 27;
                        } else if (bottomLeftOpen) {
                            newMap[i, j] = 28;
                        } else if (topLeftOpen) {
                            newMap[i, j] = 29;
                        } else {
                            newMap[i, j] = 11;
                        }
                    } else if (leftOpen && rightOpen) {
                        newMap[i, j] = 10;
                    } else if (topOpen && rightOpen) {
                        newMap[i, j] = topRightOpen ? 20 : 9;
                    } else if (topOpen && leftOpen) {
                        newMap[i, j] = topLeftOpen ? 19 : 8;
                    } else if (bottomOpen && rightOpen) {
                        newMap[i, j] = bottomRightOpen ? 18 : 7;
                    } else if (bottomOpen && leftOpen) {
                        newMap[i, j] = bottomLeftOpen ? 17 : 6;
                    } else if (bottomOpen && topOpen) {
                        newMap[i, j] = 5;
                    } else if (rightOpen) {
                        newMap[i, j] = 4;
                    } else if (leftOpen) {
                        newMap[i, j] = 3;
                    } else if (topOpen) {
                        newMap[i, j] = 2;
                    } else if (bottomOpen) {
                        newMap[i, j] = 1;
                    }
                } else {
                    newMap[i, j] = 0;
                }
            }
        }
        return newMap;
    }
    static void drawMap(int [,] map)
    {
        string line = "";
        for (int i = 0; i < map.GetLength(0); i++) {
            for (int j = 0; j < map.GetLength(1); j++) {
                char symbol = '?';
                switch (map[i, j]) {
                    case 0:
                        symbol = '/';
                        break;
                    case 1:
                        symbol = '\u25B3';
                        break;
                    case 2:
                        symbol = '\u25BD';
                        break;
                    case 3:
                        symbol = '\u25B7';
                        break;
                    case 4:
                        symbol = '\u25C1';
                        break;
                    case 5:
                        symbol = '\u2551';
                        break;
                    case 6:
                        symbol = '\u2557';
                        break;
                    case 7:
                        symbol = '\u2554';
                        break;
                    case 8:
                        symbol = '\u255D';
                        break;
                    case 9:
                        symbol = '\u255A';
                        break;
                    case 10:
                        symbol = '\u2550';
                        break;
                    case 11:
                        symbol = '\u2563';
                        break;
                    case 12:
                        symbol = '\u2560';
                        break;
                    case 13:
                        symbol = '\u2566';
                        break;
                    case 14:
                        symbol = '\u2569';
                        break;
                    case 15:
                        symbol = '\u256C';
                        break;
                    case 16:
                        symbol = ' ';
                        break;
                    case 17:
                        symbol = '\u2510';
                        break;
                    case 18:
                        symbol = '\u250C';
                        break;
                    case 19:
                        symbol = '\u2518';
                        break;
                    case 20:
                        symbol = '\u2514';
                        break;
                    case 21:
                        symbol = '\u2500';
                        break;
                    case 22:
                        symbol = '\u2534';
                        break;
                    case 23:
                        symbol = '\u2534';
                        break;
                    case 24:
                        symbol = '\u2500';
                        break;
                    case 25:
                        symbol = '\u252C';
                        break;
                    case 26:
                        symbol = '\u252C';
                        break;
                    case 27:
                        symbol = '\u2502';
                        break;
                    case 28:
                        symbol = '\u2524';
                        break;
                    case 29:
                        symbol = '\u2524';
                        break;
                    case 30:
                        symbol = '\u2502';
                        break;
                    case 31:
                        symbol = '\u251C';
                        break;
                    case 32:
                        symbol = '\u251C';
                        break;
                    case 33:
                        symbol = '\u2813';
                        symbol = '\u2808';
                        symbol = '\u2808';
                        break;
                    case 34:
                        symbol = '\u281A';
                        symbol = '\u2801';
                        break;
                    case 35:
                        symbol = '\u2832';
                        symbol = '\u2820';
                        symbol = '\u2840';
                        break;
                    case 36:
                        symbol = '\u2816';
                        symbol = '\u2804';
                        symbol = '\u2880';
                        break;
                    case 37:
                        symbol = '\u00A8';
                        break;
                    case 38:
                        symbol = ':';
                        break;
                    case 39:
                        symbol = '\u2821';
                        break;
                    case 40:
                        symbol = '\u280C';
                        break;
                    case 41:
                        symbol = ':';
                        break;
                    case 42:
                        symbol = '\u2824';
                        break;
                    case 43:
                        symbol = '\u2804';
                        symbol = '\u2832';
                        symbol = '\u2829';
                        break;
                    case 44:
                        symbol = '\u2841';
                        symbol = '\u2816';
                        symbol = '\u280D';
                        break;
                    case 45:
                        symbol = '\u2820';
                        symbol = '\u281A';
                        symbol = '\u282C';
                        break;
                    case 46:
                        symbol = '\u2808';
                        symbol = '\u2813';
                        symbol = '\u2825';
                        break;
                }
                line += symbol;
            }
            line += "\n";
        }
        Debug.Log(line);
    }
}
