using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Room[,] Map =  new Room[15,15];

    public int[,] MinMap = new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,2,2,2,2,2,1,1,1,1,1},
        {1,1,1,1,1,2,2,2,2,2,1,1,1,1,1},
        {1,1,1,1,1,2,2,3,2,2,1,1,1,1,1},
        {1,1,1,1,1,2,2,2,2,2,1,1,1,1,1},
        {1,1,1,1,1,2,2,2,2,2,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
    };
    public int[,] MaxMap = new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {1,2,2,3,3,3,3,3,3,3,3,3,2,2,1},
        {1,2,2,3,3,3,3,3,3,3,3,3,2,2,1},
        {1,2,2,3,3,4,4,4,4,4,3,3,2,2,1},
        {1,2,2,3,3,4,4,4,4,4,3,3,2,2,1},
        {1,2,2,3,3,4,4,4,4,4,3,3,2,2,1},
        {1,2,2,3,3,4,4,4,4,4,3,3,2,2,1},
        {1,2,2,3,3,4,4,4,4,4,3,3,2,2,1},
        {1,2,2,3,3,3,3,3,3,3,3,3,2,2,1},
        {1,2,2,3,3,3,3,3,3,3,3,3,2,2,1},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
    };
    public Room basicRoom;

    public string[,] fakeMap = new string[15, 15];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                Map[i, j] = basicRoom;
                fakeMap[i, j] = "O";
            }
        }
        fakeMap[7, 7] = "W";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateMap();
        }
    }

    public void GenerateMap()
    {
        //print("debug");
        string willPrint = "";

        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (i == 7 && j == 7)
                {
                    fakeMap[i, j] = "W";
                }
                else
                {
                    Map[i,j].myType = Room.RoomType.Empty;
                    fakeMap[1, j] = "O";
                    willPrint += fakeMap[i, j] + " ";
                }
            }
            willPrint += "\n";
        }
        print(willPrint);

        //make every Room empty
        //make the center room waiting
        //go into a for loop
        //Map[7,7].SetToWaiting();
    }

    public bool MapComplete()
    {
        bool done = true;
        foreach (var temp in fakeMap)
        {
            if (temp == "W")
            {
                done = false;
            }
        }
        return done;
    }

    public void IterateMap()
    {
        bool checkAgain = false;
        int baseMin;
        int baseMax;
        int actualMin;
        int actualMax;
        int targetX = 0;
        int targetY = 0;
        int numConnected;
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (!checkAgain && fakeMap[i,j]=="W")
                {
                    checkAgain = true;
                    if (i != 0 && j != 0 && i != 14 && j != 14) //if not on the edges
                    {
                        baseMin = MinMap[i, j];
                        baseMax = MaxMap[i, j];
                        actualMin = 0;
                        actualMax = 4;
                        numConnected = 0;
                        if (fakeMap[i - 1, j] == "X")
                        {
                            actualMax--;
                        }
                        if (fakeMap[i + 1, j] == "X")
                        {
                            actualMax--;
                        }
                        if (fakeMap[i, j - 1] == "X")
                        {
                            actualMax--;
                        }
                        if (fakeMap[i, j + 1] == "X")
                        {
                            actualMax--;
                        }
                        
                        if (fakeMap[i - 1, j] == "W" || fakeMap[i - 1, j] == "D")
                        {
                            actualMin++;
                        }
                        if (fakeMap[i + 1, j] == "W" || fakeMap[i + 1, j] == "D")
                        {
                            actualMin++;
                        }
                        if (fakeMap[i, j - 1] == "W" || fakeMap[i, j - 1] == "D")
                        {
                            actualMin++;
                        }
                        if (fakeMap[i, j + 1] == "X" || fakeMap[i, j + 1] == "D")
                        {
                            actualMin++;
                        }

                        if (actualMax < baseMax)
                        {
                            baseMax = actualMax;
                        }

                        if (actualMin > baseMin)
                        {
                            baseMin = actualMin;
                        }
                    }
                }
            }
        }
    }
}
